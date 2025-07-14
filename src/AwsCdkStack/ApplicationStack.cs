using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.IAM;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.S3;
using Constructs;
using HealthCheck = Amazon.CDK.AWS.ECS.HealthCheck;

namespace AwsCdkStack;

public class ApplicationStack : Stack
{
    private readonly Bucket _configBucket;
    private readonly Vpc _vpc;

    internal ApplicationStack(
        Construct scope,
        string id,
        InfrastructureStack infrastructureStack,
        IStackProps props = null)
        : base(scope, id, props)
    {
        _configBucket = infrastructureStack.Bucket;
        _vpc = infrastructureStack.Vpc;
        CreateVpcEndpoints();

        var albSg = CreateAlbSg();
        var ecsSg = CreateEcsSg(albSg);

        var ecsCluster = CreateEcsCluster();
        var ecsService = CreateEcsService(ecsSg, ecsCluster);

        var targetGroup = CreateTargetGroup();
        targetGroup.AddTarget(ecsService);

        var alb = CreateAlb(albSg);
        AddAlbListener(alb, targetGroup);
    }

    private static void AddAlbListener(ApplicationLoadBalancer alb, ApplicationTargetGroup targetGroup)
    {
        alb.AddListener("MyListener", new BaseApplicationListenerProps
        {
            Port = 80,
            DefaultTargetGroups = [targetGroup]
        });
    }

    private void AddYarpProxy(TaskDefinition taskDefinition, string proxyImage)
    {
        taskDefinition.AddContainer("yarp-proxy", new ContainerDefinitionOptions
        {
            Image = new RepositoryImage(proxyImage),
            PortMappings = new[]
            {
                new PortMapping
                {
                    ContainerPort = 80
                }
            },
            HealthCheck = new HealthCheck
            {
                Command = ["CMD-SHELL", "curl -f http://localhost:80 || exit 1"],
                Interval = Duration.Seconds(30),
                Timeout = Duration.Seconds(5),
                Retries = 3,
                StartPeriod = Duration.Seconds(60)
            },
            Logging = LogDriver.AwsLogs(new AwsLogDriverProps
            {
                LogGroup = new LogGroup(this, "YarpProxyLogGroup", new LogGroupProps
                {
                    LogGroupName = "/ecs/yarp-proxy",
                    Retention = RetentionDays.ONE_DAY,
                    RemovalPolicy = RemovalPolicy.DESTROY
                }),
                StreamPrefix = "yarp-proxy"
            })
        });
    }

    private void AddYarpTarget(TaskDefinition taskDefinition, string targetImage)
    {
        taskDefinition.AddContainer("yarp-target", new ContainerDefinitionOptions
        {
            Image = new RepositoryImage(targetImage!),
            PortMappings = new[]
            {
                new PortMapping
                {
                    ContainerPort = 8080
                }
            },
            HealthCheck = new HealthCheck
            {
                Command = ["CMD-SHELL", "curl -f http://localhost:8080 || exit 1"],
                Interval = Duration.Seconds(30),
                Timeout = Duration.Seconds(5),
                Retries = 3,
                StartPeriod = Duration.Seconds(60)
            },
            Logging = LogDriver.AwsLogs(new AwsLogDriverProps
            {
                LogGroup = new LogGroup(this, "YarpTargetLogGroup", new LogGroupProps
                {
                    LogGroupName = "/ecs/yarp-target",
                    Retention = RetentionDays.ONE_DAY,
                    RemovalPolicy = RemovalPolicy.DESTROY
                }),
                StreamPrefix = "yarp-target"
            })
        });
    }

    private ApplicationLoadBalancer CreateAlb(SecurityGroup albSg)
    {
        var alb = new ApplicationLoadBalancer(this, "MyLoadBalance", new ApplicationLoadBalancerProps
        {
            Vpc = _vpc,
            SecurityGroup = albSg,
            VpcSubnets = new SubnetSelection
            {
                SubnetGroupName = "Public"
            },
            InternetFacing = true
        });
        return alb;
    }

    private SecurityGroup CreateAlbSg()
    {
        var albSg = new SecurityGroup(this, "ALB-SG", new SecurityGroupProps
        {
            Vpc = _vpc,
            Description = "ALB-SG",
            AllowAllOutbound = true,
            SecurityGroupName = "ALB-SG"
        });
        albSg.AddIngressRule(
            Peer.AnyIpv4(),
            Port.HTTP, "allow http from anywhere");
        albSg.AddIngressRule(
            Peer.AnyIpv4(),
            Port.HTTPS,
            "allow https from anywhere");
        return albSg;
    }

    private Cluster CreateEcsCluster()
    {
        var ecsCluster = new Cluster(this, "MyCluster", new ClusterProps
        {
            Vpc = _vpc
        });
        return ecsCluster;
    }

    private FargateService CreateEcsService(SecurityGroup ecsSg, Cluster ecsCluster)
    {
        var ecsService = new FargateService(this, "YarpService", new FargateServiceProps
        {
            VpcSubnets = new SubnetSelection
            {
                SubnetGroupName = "App"
            },
            SecurityGroups = [ecsSg],
            DesiredCount = 1,
            TaskDefinition = CreateTaskDef(),
            Cluster = ecsCluster
        });
        return ecsService;
    }

    private SecurityGroup CreateEcsSg(SecurityGroup albSg)
    {
        var ecsSg = new SecurityGroup(this, "ECS-SG", new SecurityGroupProps
        {
            Vpc = _vpc,
            Description = "ECS-SG",
            AllowAllOutbound = true,
            SecurityGroupName = "ECS-SG"
        });
        ecsSg.AddIngressRule(
            Peer.SecurityGroupId(albSg.SecurityGroupId),
            Port.Tcp(80),
            "allow traffic from ALB");
        return ecsSg;
    }

    private Role CreateExecutionRole()
    {
        var executionRole = new Role(this, "MyExecutionRole", new RoleProps
        {
            AssumedBy = new ServicePrincipal("ecs-tasks.amazonaws.com"),
            ManagedPolicies =
            [
                ManagedPolicy.FromAwsManagedPolicyName(
                    "service-role/AmazonECSTaskExecutionRolePolicy")
            ]
        });
        executionRole.AddToPolicy(new PolicyStatement(new PolicyStatementProps
        {
            Effect = Effect.ALLOW,
            Actions =
            [
                "ecr:GetAuthorizationToken",
                "ecr:BatchCheckLayerAvailability",
                "ecr:GetDownloadUrlForLayer",
                "ecr:BatchGetImage"
            ],
            Resources = ["*"]
        }));
        return executionRole;
    }

    private ApplicationTargetGroup CreateTargetGroup()
    {
        var targetGroup = new ApplicationTargetGroup(this, "MyTargetGroup", new ApplicationTargetGroupProps
        {
            Vpc = _vpc,
            Port = 80,
            Protocol = ApplicationProtocol.HTTP,
            HealthCheck = new Amazon.CDK.AWS.ElasticLoadBalancingV2.HealthCheck
            {
                Path = "/"
            }
        });
        return targetGroup;
    }

    private TaskDefinition CreateTaskDef()
    {
        var taskDefinition = new TaskDefinition(this, "YarpExample", new TaskDefinitionProps
        {
            Cpu = "1024",
            MemoryMiB = "2048",
            Compatibility = Compatibility.FARGATE,
            TaskRole = CreateTaskRole(),
            ExecutionRole = CreateExecutionRole(),
            RuntimePlatform = new RuntimePlatform
            {
                CpuArchitecture = CpuArchitecture.X86_64,
                OperatingSystemFamily = OperatingSystemFamily.LINUX
            }
        });

        var proxyImage = Node.TryGetContext("yarp-proxy-image") as string;
        var targetImage = Node.TryGetContext("yarp-target-image") as string;
        AddYarpProxy(taskDefinition, proxyImage);
        AddYarpTarget(taskDefinition, targetImage);
        return taskDefinition;
    }

    private Role CreateTaskRole()
    {
        var taskRole = new Role(this, "MyTaskRole", new RoleProps
        {
            AssumedBy = new ServicePrincipal("ecs-tasks.amazonaws.com"),
            Description = "Role for ECS Tasks"
        });

        taskRole.AddToPolicy(new PolicyStatement(new PolicyStatementProps
        {
            Effect = Effect.ALLOW,
            Actions = ["s3:GetObject"],
            Resources = [$"arn:aws:s3:::{_configBucket.BucketName}/*"]
        }));
        return taskRole;
    }

    private void CreateVpcEndpoints()
    {
        _vpc.AddInterfaceEndpoint("EcrApiEndpoint", new InterfaceVpcEndpointOptions
        {
            Service = InterfaceVpcEndpointAwsService.ECR
        });

        _vpc.AddInterfaceEndpoint("EcrDockerEndpoint", new InterfaceVpcEndpointOptions
        {
            Service = InterfaceVpcEndpointAwsService.ECR_DOCKER
        });

        _vpc.AddInterfaceEndpoint("CloudWatchLogsEndpoint", new InterfaceVpcEndpointOptions
        {
            Service = InterfaceVpcEndpointAwsService.CLOUDWATCH_LOGS
        });
    }
}
