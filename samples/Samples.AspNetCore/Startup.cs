using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Picnic.Controllers;
using Picnic.Extensions;
using Samples.AspNetCore.Data;
using Samples.AspNetCore.Models;
using Samples.AspNetCore.Services;

namespace Samples.AspNetCore
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
            
            // Step 1: Add Picnic, Use Json Store
            services.AddPicnic(options =>
            {
                options.Manage.RoutePrefix = "cms";
                options.Manage.EditorOptions.EditorBaseUrl = "/js/tinymce";
                options.Manage.EditorOptions.SetStylesheets("/lib/bootstrap/dist/css/bootstrap.min.css", "/css/site.css");
            }).UseJsonStore();

            // Step 2: Setup Authorization for Picnic
            services.AddAuthorization(options => options.AddPolicy("PicnicAuthPolicy", policyOptions =>
            {
                // NOTE: This example allows anonymous access to the Picnic interface
                // Add your application's policy specifics to control access to the Picnic interface
                policyOptions.RequireAssertion(x => true);
                policyOptions.Build();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
                // Step 3: Add the Picnic Catch-All Route to enable dynamic pages
                routes.AddPicnicDynamicPageRoute();
            });
        }
    }
}
