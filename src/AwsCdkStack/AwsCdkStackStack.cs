using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Constructs;

namespace AwsCdkStack
{
    public class AwsCdkStackStack : Stack
    {
        internal AwsCdkStackStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = new Vpc(this, "MyVpc", new VpcProps
            {
                MaxAzs = 2,
                IpAddresses = IpAddresses.Cidr("10.16.0.0/16"),
                SubnetConfiguration = new ISubnetConfiguration[]
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
                }
            });
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
