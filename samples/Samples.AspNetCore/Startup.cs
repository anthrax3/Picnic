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
using Picnic.SimpleAuth.Extensions;
using Picnic.Extensions;
using Picnic.Stores.EF;
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

            // Step 1: Add Picnic, Use Json Store
            services.AddPicnic(options =>
            {
                options.Manage.EditorOptions.EditorBaseUrl = "/js/tinymce";
                options.Manage.EditorOptions.SetStylesheets("/lib/bootstrap/dist/css/bootstrap.min.css", "/css/site.css");
            })
                .UseJsonStore("App_Data")                
                .UseSimpleAuth(opts => opts.LoginPath = "/cms/login");

            services.AddMvc()
                .UsePicnic("cms")
                .UsePicnicSimpleAuth("cms");
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

            app.UsePicnicSimpleAuth();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                
                // Step 4: Add the Picnic Catch-All Route to enable dynamic pages
                routes.AddPicnicDynamicPageRoute();
            });
        }
    }
}
