using Amazon.CDK;

namespace AwsCdkStack;

internal sealed class Program
{
    public static void Main(string[] args)
    {
        var app = new App();
        var infrastructureStack = CreateInfrastructureStack(app);
        CreateApplicationStack(app, infrastructureStack);

        app.Synth();
    }

    private static void CreateApplicationStack(App app, InfrastructureStack infrastructureStack)
    {
        new ApplicationStack(app, "ApplicationStack", infrastructureStack, new StackProps
        {
            Env = new Environment
            {
                Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION")
            }
        });
    }

    private static InfrastructureStack CreateInfrastructureStack(App app)
    {
        var infrastructureStack = new InfrastructureStack(app, "InfrastructureStack", new StackProps
        {
            Env = new Environment
            {
                Account = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_ACCOUNT"),
                Region = System.Environment.GetEnvironmentVariable("CDK_DEFAULT_REGION")
            }
        });
        return infrastructureStack;
    }
}
