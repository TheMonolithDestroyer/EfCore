using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using System;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;
using TheNomad.EFCore.Data;

namespace TheNomad.EFCore.Api
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
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddMvc();
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(
                @"Server=localhost; port=5432; user id=postgres; password=pdMsWFjZ; database=EF; pooling=true;SearchPath=public",
                b => b.MigrationsAssembly("TheNomad.EFCore.Data")));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ClientIdentity API v1",
                    Version = "v1",
                    Description = "A v1 WebAPI for managing ClientIdentity services"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(options =>
            {
                options.PreSerializeFilters.Add((openApiDocument, httpRequest) =>
                {
                    openApiDocument.Servers = new List<OpenApiServer>();
                    var apiServer = new OpenApiServer { Url = "http://localhost:22387/" };
                    openApiDocument.Servers.Add(apiServer);
                });
            });

            app.UseSwaggerUI(c => 
            { 
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "V1"); 
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
