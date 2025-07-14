using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Constructs;

namespace AwsCdkStack
{
    public class InfrastructureStack : Stack
    {
        public Vpc Vpc { get; set; }

        internal InfrastructureStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = CreateVpc();

            CreateS3Endpoint(vpc);

            // 改由AppStack建立
            // CreateVpcEndpoints(vpc);

            Vpc = vpc;
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

        private void CreateS3Endpoint(Vpc vpc)
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
