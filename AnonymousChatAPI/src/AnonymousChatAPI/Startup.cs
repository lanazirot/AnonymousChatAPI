using AnonymousChatAPI.Middlewares;
using Application.Interfaces;
using Application.Interfaces.Services;
using Infrastructure.Services.RandomUsername;
using Infrastructure.Services.StreamIO;
using Infrastructure.Services.User;

namespace AnonymousChatAPI {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services) {
            services.AddTransient<IRandomUserService, RandomUserService>();
            services.AddTransient<IStreamIOService, StreamIOService>();
            services.AddTransient<IUserService, UserService>();
            services.AddControllers();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<AccessMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context => {
                    await context.Response.WriteAsync("AnonymousChatAPI");
                });
            });
        }
    }
}