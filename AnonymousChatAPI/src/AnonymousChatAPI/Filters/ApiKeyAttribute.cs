using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace AwesomeApi.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class ApiKeyAttribute : Attribute, IAuthorizationFilter {
    private const string API_KEY_HEADER_NAME = "X-API-Key";
    public void OnAuthorization(AuthorizationFilterContext context) {
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development") return;
        var submittedApiKey = context.HttpContext.Request.Headers[API_KEY_HEADER_NAME].ToString();
        var apiKey = GetSecretValue();
        var secretDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(apiKey);
        var extractedApiKey = secretDictionary!["ApiKey"];
        if (string.IsNullOrEmpty(submittedApiKey) || submittedApiKey != extractedApiKey) 
            context.Result = new UnauthorizedResult();
    }
    public string GetSecretValue() {
        var secretsManagerClient = new AmazonSecretsManagerClient();
        var getRequest = new GetSecretValueRequest {
            SecretId = "ApiSecretCredentialKey",
            VersionStage = "AWSCURRENT"
        };
        var response = secretsManagerClient.GetSecretValueAsync(getRequest).GetAwaiter().GetResult();
        return response.SecretString;
    }
}