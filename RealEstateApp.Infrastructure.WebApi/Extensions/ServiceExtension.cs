using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RealEstateApp.Presentation.WebApi.Extensions
{
    public static class ServiceExtension
    {
        public static void AddSwaggerExtension(this IServiceCollection service)
        {
            service.AddSwaggerGen(opt =>
            {
                List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", searchOption: SearchOption.TopDirectoryOnly).ToList();
                xmlFiles.ForEach(xmlFile => opt.IncludeXmlComments(xmlFile));

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Real Estate Api",
                    Description = "Api for admins and devs of a Real Estate App",
                    Contact = new OpenApiContact
                    {
                        Name = "Joel Grullon",
                        Email = "joelenmanuelgm@gmail.com",
                        Url = new Uri("https://github.com/JoelGrullon12/RealEstateApp")
                    }
                });

                opt.EnableAnnotations();
                opt.DescribeAllParametersInCamelCase();
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Introduce tu token Bearer en este formato: Bearer {tu token aqui}"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            },
                            Scheme="Bearer",
                            Name="Bearer",
                            In=ParameterLocation.Header,

                        }, new List<string>()
                    }
                });
            });
        }

        public static void AddApiVersioningExtension(this IServiceCollection service)
        {
            service.AddApiVersioning(conf =>
            {
                conf.DefaultApiVersion = new ApiVersion(1, 0);
                conf.AssumeDefaultVersionWhenUnspecified = true;
                conf.ReportApiVersions = true;
            });
        }
    }
}