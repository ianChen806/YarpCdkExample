using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
using Amazon.CDK.AWS.IAM;
using Constructs;

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
                Cpu = "256",
                MemoryMiB = "512",
                Compatibility = Compatibility.FARGATE,
                TaskRole = CreateTaskRole(),
                ExecutionRole = CreateExecutionRole()
            });
            taskDefinition.AddContainer("nginx", new ContainerDefinitionOptions()
            {
                Image = new RepositoryImage("nginx:latest"),
                PortMappings = new PortMapping[]
                {
                    new PortMapping
                    {
                        ContainerPort = 80,
                    }
                },
                HealthCheck = new HealthCheck()
                {
                    Command = ["CMD-SHELL", "nginx -t || exit 1"],
                    Interval = Duration.Seconds(30),
                    Timeout = Duration.Seconds(5),
                    Retries = 3,
                    StartPeriod = Duration.Seconds(60)
                }
            });
            return taskDefinition;
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
                MaxAzs = 1,
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
