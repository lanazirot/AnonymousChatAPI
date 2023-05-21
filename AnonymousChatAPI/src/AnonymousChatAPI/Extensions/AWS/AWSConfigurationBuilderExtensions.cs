using Infrastructure.Services.AWS;

namespace AnonymousChatAPI.Extensions.AWS {
    public static class AWSConfigurationBuilderExtensions {
        public static void AddAmazonSecretsManager(this IConfigurationBuilder configurationBuilder, string region, string secretName) {
            var configurationSource = new AmazonSecretsManagerConfigurationSource(region, secretName);
            configurationBuilder.Add(configurationSource);
        }
    }
}
