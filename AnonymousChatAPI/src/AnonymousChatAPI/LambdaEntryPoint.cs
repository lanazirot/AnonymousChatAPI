using AnonymousChatAPI.Extensions.AWS;

namespace AnonymousChatAPI {
    public class LambdaEntryPoint : Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction {
        protected override void Init(IWebHostBuilder builder) {
            builder.ConfigureAppConfiguration((_, configurationBuilder) => {
                configurationBuilder.AddAmazonSecretsManager("us-east-1", "StreamIOServiceKeys");
            });
            builder.UseStartup<Startup>();
        }
        protected override void Init(IHostBuilder builder) {}
    }
}