using Common.Helpers;
using Domain;
using Domain.Extensions;
using FriendsApi.Injection.Auth;
using FriendsApi.Injection.DB;
using FriendsApi.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FriendsApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string apiHost = Configuration.GetValue<string>("APIHost").TrimEnd('/') + "/";
            services.AddDataBase(Configuration);
            services.AddConfiguration(apiHost);
            services.AddControllers();
            #region Swagger Docs
            services.AddSwaggerDocumentation(apiHost);
            #endregion
            services.AddConfigure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FriendsAppDbContext dbContext,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment() || env.EnvironmentName == "Beta")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHttpsRedirection();
            }
            app.UseCors(builder =>
            {
                builder.WithOrigins(new string[] { "https://localhost:44354" })
                .AllowAnyMethod().AllowCredentials().AllowAnyHeader();
            });
            app.UseHttpsRedirection();
            app.UseIdentityServer();
            app.UseStaticFiles();
            var options = app.ApplicationServices.GetService<Microsoft.Extensions.Options.IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            app.UseSwaggerDocumentation();
            RequestContextManager.SetRequestContextManager(app.ApplicationServices.GetService<IHttpContextAccessor>());
            DbContextExtensions.Seed(dbContext, serviceProvider);
        }
    }
}
