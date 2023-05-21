using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services.AWS {
    /// <summary>
    /// https://aws.amazon.com/blogs/modernizing-with-aws/how-to-load-net-configuration-from-aws-secrets-manager/
    /// </summary>
    public class AmazonSecretsManagerConfigurationSource : IConfigurationSource {
        private readonly string _region;
        private readonly string _secretName;

        public AmazonSecretsManagerConfigurationSource(string region, string secretName) {
            _region = region;
            _secretName = secretName;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) {
            return new AmazonSecretsManagerConfigurationProvider(_region, _secretName);
        }
    }
}
