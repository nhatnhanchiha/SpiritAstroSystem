using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SpiritAstro.BusinessTier.Generations.DependencyInjection;
using SpiritAstro.BusinessTier.Services;
using SpiritAstro.DataTier.Models;
using SpiritAstro.WebApi.AppStart;
using SpiritAstro.WebApi.Middlewares;

namespace SpiritAstro.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy(MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            
            services.InitSwagger();

            services.ConfigureJsonFormatServices();

            services.InitFirebase();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.InitializerDI();

            services.ConfigureAutoMapperServices();

            services.ConfigureVersioningServices();

            services.AddScoped<IAccountService, AccountService>();

            services.ConfigureCasbinServices();

            services.ConfigureFilterServices();

            services.ConfigureAstroChartServices();

            services.ConfigureRedisServices();

            services.AddSingleton<IAstroOnlineService, AstroOnlineService>();

            services.AddDbContext<SpiritAstroContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DbContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IApiVersionDescriptionProvider provider)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            //     app.UseSwagger();
            //     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpiritAstro.WebApi v1"));
            // }
            app.UseDeveloperExceptionPage();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "/swagger/{documentName}/swagger.json";
            });
            
            app.UseSwaggerUI(c =>
            {
                foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            
            });
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpiritAstro.WebApi v1"));

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseMiddleware<JwtMiddleware>();

            app.UseRouting();
            
            app.UseCors(MyAllowSpecificOrigins);

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}