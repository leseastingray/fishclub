using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FishClubWebsite.Data;
using FishClubWebsite.Models;
using FishClubWebsite.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FishClubWebsite
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
            // added for security
            // addresses forgery cookie issues
            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = "AntiforgeryCookie";
                options.Cookie.Domain = "localhost";
                options.Cookie.Path = "/";
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                // This will tell the browser to prevent transmission of a cookie over an unencrypted HTTP request... hopefully
                options.Secure = CookieSecurePolicy.Always;
            });

            // Add DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Dependency Injection for real database
            services.AddTransient<IFishRepository, FishRepository>();
            // Dependency Injection for testing
            //services.AddTransient<IFishRepository, FakeFishRepository>();

            // Add Identity
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc();//.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // added for security
            // addresses X-Frame Options and X-Content-Type-Options
            /*app.Use(async (httpContext, next) =>
            {
                httpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });

            // addesses X-Frame Options
            //app.UseXfo(options => options.Deny());
            // addresses Web Browser XSS Protection not enabled
            //app.UseXXssProtection(options => options.EnabledWithBlockMode());
            // addresses X-Content-Type-Options
            //app.UseXContentTypeOptions();
            */
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Add Authentication
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            // Seed data
            //SeedData.Seed(app);

            // Ensure that the database has been created and the latest migration applied
            context.Database.Migrate();

            // For creating a starting AdminAccount
            //ApplicationDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
