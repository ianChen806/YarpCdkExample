using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Amazon.CDK.AWS.ECS;
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
            CreateEcsSg(vpc, albSg);

            CreateEcsCluster(vpc);
        }

        private Cluster CreateEcsCluster(Vpc vpc)
        {
            var ecsCluster = new Cluster(this, "MyCluster", new ClusterProps
            {
                Vpc = vpc,
            });
            return ecsCluster;
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
                Port.Tcp(8080),
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
                    SubnetType = SubnetType.PRIVATE_ISOLATED,
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
