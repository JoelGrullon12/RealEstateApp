using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RealEstateApp.Presentation.WebApi.Extensions
{
    public static class AppExtension
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "Real Estate Api");
                opt.DefaultModelRendering(ModelRendering.Model);
            });
        }
    }
}