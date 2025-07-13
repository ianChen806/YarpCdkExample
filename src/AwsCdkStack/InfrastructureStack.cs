using System.Linq;
using Amazon.CDK;
using Amazon.CDK.AWS.EC2;
using Constructs;

namespace AwsCdkStack
{
    public class InfrastructureStack : Stack
    {
        internal InfrastructureStack(Construct scope, string id, IStackProps props = null)
            : base(scope, id, props)
        {
            var vpc = CreateVpc();
            CreateS3Endpoint(vpc);

            // 改由AppStack建立
            // CreateVpcEndpoints(vpc);

            CfnOutputs(vpc);
        }

        private void CreateVpcEndpoints(Vpc vpc)
        {
            vpc.AddInterfaceEndpoint("EcrApiEndpoint", new InterfaceVpcEndpointOptions
            {
                Service = InterfaceVpcEndpointAwsService.ECR
            });

            vpc.AddInterfaceEndpoint("EcrDockerEndpoint", new InterfaceVpcEndpointOptions
            {
                Service = InterfaceVpcEndpointAwsService.ECR_DOCKER
            });

            vpc.AddInterfaceEndpoint("CloudWatchLogsEndpoint", new InterfaceVpcEndpointOptions
            {
                Service = InterfaceVpcEndpointAwsService.CLOUDWATCH_LOGS
            });
        }

        private void CfnOutputs(Vpc vpc)
        {
            new CfnOutput(this, "VpcId", new CfnOutputProps
            {
                Value = vpc.VpcId,
                ExportName = "InfrastructureStack-VpcId"
            });
            new CfnOutput(this, "PublicSubnets", new CfnOutputProps
            {
                Value = string.Join(",", vpc.PublicSubnets.Select(r => r.SubnetId)),
                ExportName = "InfrastructureStack-PublicSubnets"
            });
            new CfnOutput(this, "AppSubnets", new CfnOutputProps
            {
                Value = string.Join(",", vpc.SelectSubnets(new SubnetSelection
                {
                    SubnetGroupName = "App"
                }).Subnets.Select(s => s.SubnetId)),
                ExportName = "InfrastructureStack-AppSubnets"
            });
            new CfnOutput(this, "DatabaseSubnets", new CfnOutputProps
            {
                Value = string.Join(",", vpc.SelectSubnets(new SubnetSelection
                {
                    SubnetGroupName = "Database"
                }).Subnets.Select(s => s.SubnetId)),
                ExportName = "InfrastructureStack-DatabaseSubnets"
            });
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
