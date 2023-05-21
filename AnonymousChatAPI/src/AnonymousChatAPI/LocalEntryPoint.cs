using AnonymousChatAPI.Extensions.AWS;

namespace AnonymousChatAPI {
    public class LocalEntryPoint {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((_, configurationBuilder) => {
                configurationBuilder.AddAmazonSecretsManager("us-east-1", "StreamIOServiceKeys");
            }).ConfigureWebHostDefaults(webBuilder => {
               webBuilder.UseStartup<Startup>();
            });
    }
}