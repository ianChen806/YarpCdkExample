using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.ElasticLoadBalancingV2;
using Amazon.CDK.AWS.IAM;
using Constructs;
using HealthCheck = Amazon.CDK.AWS.ECS.HealthCheck;

namespace AwsCdkStack
{
    public class AwsCdkStackStack : Stack
    {
        internal AwsCdkStackStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = CreateVpc();
            CreateS3GatewayEndpoint(vpc);

            var albSg = CreateAlbSg(vpc);
            var ecsSg = CreateEcsSg(vpc, albSg);

            var ecsCluster = CreateEcsCluster(vpc);
            var ecsService = CreateEcsService(ecsSg, ecsCluster);

            var targetGroup = CreateTargetGroup(vpc);
            targetGroup.AddTarget(ecsService);

            var alb = CreateAlb(vpc, albSg);
            AddAlbListener(alb, targetGroup);
        }

        private ApplicationTargetGroup CreateTargetGroup(Vpc vpc)
        {
            var targetGroup = new ApplicationTargetGroup(this, "MyTargetGroup", new ApplicationTargetGroupProps()
            {
                Vpc = vpc,
                Port = 80,
                Protocol = ApplicationProtocol.HTTP,
                HealthCheck = new Amazon.CDK.AWS.ElasticLoadBalancingV2.HealthCheck()
                {
                    Path = "/"
                },
            });
            return targetGroup;
        }

        private static void AddAlbListener(ApplicationLoadBalancer alb, ApplicationTargetGroup targetGroup)
        {
            alb.AddListener("MyListener", new BaseApplicationListenerProps()
            {
                Port = 80,
                DefaultTargetGroups = [targetGroup]
            });
        }

        private ApplicationLoadBalancer CreateAlb(Vpc vpc, SecurityGroup albSg)
        {
            var alb = new ApplicationLoadBalancer(this, "MyLoadBalance", new ApplicationLoadBalancerProps()
            {
                Vpc = vpc,
                SecurityGroup = albSg,
                VpcSubnets = new SubnetSelection()
                {
                    SubnetGroupName = "Public",
                },
                InternetFacing = true,
            });
            return alb;
        }

        private Cluster CreateEcsCluster(Vpc vpc)
        {
            var ecsCluster = new Cluster(this, "MyCluster", new ClusterProps
            {
                Vpc = vpc,
            });
            return ecsCluster;
        }

        private FargateService CreateEcsService(SecurityGroup ecsSg, Cluster ecsCluster)
        {
            var ecsService = new FargateService(this, "MyService", new FargateServiceProps()
            {
                VpcSubnets = new SubnetSelection()
                {
                    SubnetGroupName = "App"
                },
                SecurityGroups = [ecsSg],
                DesiredCount = 1,
                TaskDefinition = CreateTaskDef(),
                Cluster = ecsCluster,
            });
            return ecsService;
        }

        private TaskDefinition CreateTaskDef()
        {
            var taskDefinition = new TaskDefinition(this, "MyTaskDefinition", new TaskDefinitionProps
            {
                Cpu = "1024",
                MemoryMiB = "2048",
                Compatibility = Compatibility.FARGATE,
                TaskRole = CreateTaskRole(),
                ExecutionRole = CreateExecutionRole(),
                RuntimePlatform = new RuntimePlatform()
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

        private void AddYarpTarget(TaskDefinition taskDefinition, string targetImage)
        {
            taskDefinition.AddContainer("yarp-target", new ContainerDefinitionOptions()
            {
                Image = new RepositoryImage(targetImage!),
                PortMappings = new PortMapping[]
                {
                    new PortMapping
                    {
                        ContainerPort = 8080,
                    }
                },
                HealthCheck = new HealthCheck()
                {
                    Command = ["CMD-SHELL", "curl -f http://localhost:8080/ || exit 1"],
                    Interval = Duration.Seconds(30),
                    Timeout = Duration.Seconds(5),
                    Retries = 3,
                    StartPeriod = Duration.Seconds(60)
                }
            });
        }

        private void AddYarpProxy(TaskDefinition taskDefinition, string proxyImage)
        {
            taskDefinition.AddContainer("yarp-proxy", new ContainerDefinitionOptions()
            {
                Image = new RepositoryImage(proxyImage),
                PortMappings = new PortMapping[]
                {
                    new PortMapping
                    {
                        ContainerPort = 80,
                    }
                },
                HealthCheck = new HealthCheck()
                {
                    Command = ["CMD-SHELL", "curl -f http://localhost:80/ || exit 1"],
                    Interval = Duration.Seconds(30),
                    Timeout = Duration.Seconds(5),
                    Retries = 3,
                    StartPeriod = Duration.Seconds(60)
                }
            });
        }

        private Role CreateExecutionRole()
        {
            var executionRole = new Role(this, "MyExecutionRole", new RoleProps()
            {
                AssumedBy = new ServicePrincipal("ecs-tasks.amazonaws.com"),
                ManagedPolicies =
                [
                    ManagedPolicy.FromAwsManagedPolicyName(
                        "service-role/AmazonECSTaskExecutionRolePolicy")
                ]
            });
            executionRole.AddToPolicy(new PolicyStatement(new PolicyStatementProps()
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

        private Role CreateTaskRole()
        {
            var taskRole = new Role(this, "MyTaskRole", new RoleProps()
            {
                AssumedBy = new ServicePrincipal("ecs-tasks.amazonaws.com"),
                Description = "Role for ECS Tasks"
            });

            taskRole.AddToPolicy(new PolicyStatement(new PolicyStatementProps()
            {
                Effect = Effect.ALLOW,
                Actions = ["s3:GetObject"],
                Resources = ["arn:aws:s3:::my-test-bucket-yarp-sample/*"]
            }));
            return taskRole;
        }

        private Vpc CreateVpc()
        {
            var vpc = new Vpc(this, "MyVpc", new VpcProps
            {
                MaxAzs = 2,
                IpAddresses = IpAddresses.Cidr("10.16.0.0/16"),
                SubnetConfiguration = SubnetConfigurations()
            });
            return vpc;
        }

        private SecurityGroup CreateEcsSg(Vpc vpc, SecurityGroup albSg)
        {
            var ecsSg = new SecurityGroup(this, "ECS-SG", new SecurityGroupProps()
            {
                Vpc = vpc,
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

        private SecurityGroup CreateAlbSg(Vpc vpc)
        {
            var albSg = new SecurityGroup(this, "ALB-SG", new SecurityGroupProps()
            {
                Vpc = vpc,
                Description = "ALB-SG",
                AllowAllOutbound = true,
                SecurityGroupName = "ALB-SG",
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

        private static ISubnetConfiguration[] SubnetConfigurations()
        {
            var subnetConfigurations = new ISubnetConfiguration[]
            {
                new SubnetConfiguration
                {
                    Name = "Public",
                    SubnetType = SubnetType.PUBLIC,
                    CidrMask = 20,
                },
                new SubnetConfiguration
                {
                    Name = "App",
                    SubnetType = SubnetType.PRIVATE_WITH_EGRESS,
                    CidrMask = 20,
                },
                new SubnetConfiguration
                {
                    Name = "Database",
                    SubnetType = SubnetType.PRIVATE_ISOLATED,
                    CidrMask = 20
                }
            };
            return subnetConfigurations;
        }

        private void CreateS3GatewayEndpoint(Vpc vpc)
        {
            var s3Endpoint = new GatewayVpcEndpoint(this, "S3Endpoint", new GatewayVpcEndpointProps
            {
                Vpc = vpc,
                Service = GatewayVpcEndpointAwsService.S3,
                Subnets =
                [
                    new SubnetSelection() { SubnetGroupName = "App" },
                    new SubnetSelection() { SubnetGroupName = "Database" },
                ]
            });
        }
    }
}
