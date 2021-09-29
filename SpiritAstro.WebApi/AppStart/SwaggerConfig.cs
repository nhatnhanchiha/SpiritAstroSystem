﻿using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

namespace SpiritAstro.WebApi.AppStart
{
    public static class SwaggerConfig
    {
        public static void InitSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SpiritAstro.WebApi", Version = "v1" });
                c.AddSecurityDefinition("x-token", new OpenApiSecurityScheme
                {
                    Name = "x-token",
                    In = ParameterLocation.Header
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    { 
                        new OpenApiSecurityScheme 
                        { 
                            Reference = new OpenApiReference 
                            { 
                                Type = ReferenceType.SecurityScheme,
                                Id = "x-token" 
                            } 
                        },
                        System.Array.Empty<string>()
                    } 
                });
            });
            services.AddSwaggerGenNewtonsoftSupport();
            services.TryAddEnumerable(ServiceDescriptor.Transient<IApiDescriptionProvider, SnakeCaseQueryParametersApiDescriptionProvider>());
        }
    }
}