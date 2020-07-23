using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using IMedXUtilities;
using IMedXUtilities.Data.Repository;
using IMedXUtilities.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore;
//using Swashbuckle.Swagger;
//using Swashbuckle.SwaggerUi;

namespace IMedXHealthService
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
            services.AddControllers();
            services.AddDbContext<IMedXDBContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:IMedXDBConnection"]));
            DBConnectify.DbConnectionString = Configuration["ConnectionString:IMedXDBConnection"];
            services.AddScoped<IPatientData, PatientICDManager>();

            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IMEDX API ", Version = "v1" });
            //    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
            //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //    c.IncludeXmlComments(xmlPath);
            //});
            //services.AddSwaggerGen(options =>
            //{
            //    options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
            //    {
            //        Title = "IMedX Health Service API",
            //        Version = "v2",
            //        Description = "Sample service for Test",
            //    });
            //});
            //services.AddSwaggerGenNewtonsoftSupport();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            //app.UseSwagger();
            //app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "IMedX Health Services"));
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            IMedXDBContext dbContext = new IMedXDBContext();
            //if (!dbContext.Database.EnsureCreated())
            dbContext.Database.Migrate();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
    
}
