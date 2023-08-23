using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RealEstateApp.Core.Application;
using RealEstateApp.Infrastructure.Identity;
using RealEstateApp.Infrastructure.Persistence;
using RealEstateApp.Presentation.WebApi.Extensions;

namespace RealEstateApp.Presentation.WebApi
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
            services.AddPersistenceLayer(Configuration);
            services.AddIdentityLayer(Configuration);
            services.AddApplicationLayer(Configuration);
            services.AddSwaggerExtension();
            services.AddApiVersioningExtension();

            services.AddControllers(opt =>
            {
                opt.Filters.Add(new ProducesAttribute("application/json"));
            }).ConfigureApiBehaviorOptions(opt =>
            {
                opt.SuppressInferBindingSourcesForParameters = true;
                opt.SuppressMapClientErrors = true;
            }).AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHttpMethodOverride();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHealthChecks("/health");

            app.UseSwaggerExtension();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}