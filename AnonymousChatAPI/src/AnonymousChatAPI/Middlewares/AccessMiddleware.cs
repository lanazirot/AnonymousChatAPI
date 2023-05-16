namespace AnonymousChatAPI.Middlewares {
    public class AccessMiddleware {
        private readonly RequestDelegate _next;

        public AccessMiddleware(RequestDelegate next) {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext) {

            return _next(httpContext);
        }
    }

    public static class MiddlewareExtensions {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<AccessMiddleware>();
        }
    }
}
