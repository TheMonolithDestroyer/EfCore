using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TheNomad.EFCore.Data;
using TheNomad.EFCore.Api.Services;
using TheNomad.EFCore.Services.DatabaseServices.Concrete;
using TheNomad.EFCore.Services.AdminServices;
using TheNomad.EFCore.Services.AdminServices.Concrete;

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
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddHttpContextAccessor();
            services.AddSingleton(new AppInformation());

            var connection = Configuration.GetConnectionString("DefaultConnection");
            if (Configuration["ENVIRONMENT"] == "Development")
            {
                connection = connection.FormDatabaseConnection();
            }
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection, b => b.MigrationsAssembly("TheNomad.EFCore.Data")));

            services.AddTransient<IChangePubDateService, ChangePubDateService>();

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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "V1"); });
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers();});
        }
    }
}
