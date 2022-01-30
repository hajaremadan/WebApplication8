using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication8.Models;

namespace WebApplication8
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
            services.AddDbContext<Models.AppDbContext>(o => o.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase")));
            services.AddControllers(mvcOptions =>
                  mvcOptions.EnableEndpointRouting = false);
            services.AddOData();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
                       if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }
        IEdmModel GetEdmModel()
        {
            /*ODataConventionModelBuilder odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<Student>("Students");

            return odataBuilder.GetEdmModel();
            */
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Models.Employee>("Employees");
            builder.EntitySet<Models.Student>("Students");
            return builder.GetEdmModel();

        }
    }
}
