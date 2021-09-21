using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace FriendsApi.Swagger
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, string apiHost)
        {
            services.AddSwaggerGen(c =>
            {
                //c.OperationFilter<AddAcceptLanguageParameter>();
                c.CustomSchemaIds(obj => obj.FullName);
                c.SwaggerDoc("v1",
                    new OpenApiInfo()
                    {
                        Title = "Friends Api",
                        Version = "v1",
                        Description = "Friends Api Documentation",
                        Contact = new OpenApiContact()
                        {
                            Name = "Friends",
                            Email = "Friendss@gmail.com",
                            Url = new Uri("http://Friends.ge/"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX",
                            Url = new Uri("http://Friends.ge/")
                        }
                    }
                );
                c.OperationFilter<AddOperationId>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{apiHost}connect/token", UriKind.Absolute),
                            Scopes = new Dictionary<string, string>
                        {
                            {"FriendsApi", "FriendsApi"}
                        }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                       new string[] { "FriendsApi" }
                    }
                });
                c.AddServer(new OpenApiServer { Url = apiHost });
            });
            services.ConfigureSwaggerGen(options =>
            {
                var pathDivider = System.Environment.OSVersion.ToString().Contains("Windows", StringComparison.InvariantCultureIgnoreCase) ? @"\" : @"/";
                var xmlDocFile = Path.Combine(AppContext.BaseDirectory, Environment.CurrentDirectory + $@"{pathDivider}FriendsApi.xml");
                if (File.Exists(xmlDocFile))
                {
                    options.IncludeXmlComments(xmlDocFile);
                }
            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Friends API V1");
                c.RoutePrefix = string.Empty;
                c.DocumentTitle = "Friends API";
                c.DocExpansion(DocExpansion.None);

                c.OAuthClientId("FriendsApi");
                c.OAuthClientSecret("5Aue2ks34fj");
                c.OAuthScopeSeparator(" ");
                c.OAuthUseBasicAuthenticationWithAccessCodeGrant();

            });

            return app;
        }
        public class AddOperationId : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                operation.OperationId = context.MethodInfo.Name;
            }
        }
    }
}
