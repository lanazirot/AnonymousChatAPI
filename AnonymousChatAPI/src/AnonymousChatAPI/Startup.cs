using Application.Interfaces;
using Application.Interfaces.Services;
using Application.Models.AWS;
using Infrastructure.Services.RandomChannel;
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
            services.AddTransient<IRandomChannelService, RandomChannelService>();
            services.AddControllers();
            services.Configure<StreamIOServiceKeys>(Configuration);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder => builder
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
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