using Newtonsoft.Json;

namespace AnonymousChatAPI.Middlewares {
    public class SystemExceptionMiddleware {
        private readonly RequestDelegate _next;

        public SystemExceptionMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception) {
            int statusCode = StatusCodes.Status400BadRequest;
            string errorMessage = "AnonymousChatAPI unexpected error. Please contact the sysadmin! sysadmin@anonymouschat.com";
            var errorResponse = new { Code = statusCode, Message = errorMessage, Exception = exception.Message };
            var jsonResponse = JsonConvert.SerializeObject(errorResponse);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
