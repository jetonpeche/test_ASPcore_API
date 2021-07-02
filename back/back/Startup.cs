using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace back
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
            // CORS
            services.AddCors(c =>
            {
                c.AddPolicy("CORS", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // JSON s�rialis�
            services.AddControllersWithViews();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // mySQL
           //services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:bdd"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CORS");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // fichier
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(

                    // chemin courant + dossier
                    Path.Combine(Directory.GetCurrentDirectory(), "image")),
                    RequestPath = "/image"
            });
        }
    }
}