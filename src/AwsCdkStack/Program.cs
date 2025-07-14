using Amazon.CDK;

namespace AwsCdkStack
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            var infrastructureStack = new InfrastructureStack(app, "InfrastructureStack", new StackProps()
            {
                Env = new Amazon.CDK.Environment
                {
                    Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                    Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
                }
            });

            // TODO
            // new ApplicationStack(app, "ApplicationStack", infrastructureStack, new StackProps
            // {
            //     Env = new Amazon.CDK.Environment
            //     {
            //         Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
            //         Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION"),
            //     },
            // });
            app.Synth();
        }
    }
}
