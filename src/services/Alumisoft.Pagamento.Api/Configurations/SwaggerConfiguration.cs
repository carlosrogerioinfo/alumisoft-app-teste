﻿using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Alumisoft.Pagamento.Api.Configurations
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Alumisoft Backend Api",
                    Description = "Api backend of project Alumisoft",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Alumisoft",
                        Email = "alumisoft@alumisoft.com",
                        Url = new Uri("https://alumisoft.com")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licença de uso: Alumisoft | Api backend",
                        Url = new Uri("https://alumisoft.com")
                    }
                });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

                #region Settings Authentication Use

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }});
                
                #endregion

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            if (env.IsDevelopment())
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
            else
            {
                app.UseSwaggerUI(options =>
                {
                    options.RoutePrefix = string.Empty;
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }

            return app;
        }

    }
}
