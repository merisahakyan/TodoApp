using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TodoAppTest.Database;

namespace TodoAppTest
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                                 .AddCookie(options =>
                                 {
                                     options.Cookie.HttpOnly = true;
                                     options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                                     options.Cookie.SameSite = SameSiteMode.Lax;

                                     options.Cookie.Name = "AuthCookieAspNetCore";
                                     options.LoginPath = "/api/Accounts/Login";
                                     options.LogoutPath = "/api/Accounts/Logout";
                                 });
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = CookieSecurePolicy.None;
            });

            services.AddMvc();

            var connection = ConfigurationExtensions.GetConnectionString(this.Configuration, "DefaultConnection");
            var context = new TodoContext(connection);
            var adminExists = context.Users.FirstOrDefault(u => u.Email == "admin@devlix.de");
            if (adminExists == null)
            {
                context.Users.Add(new User
                {
                    Email = "admin@devlix.de",
                    Name = "admin",
                    Password = "admin",
                    Role = Enums.Roles.Admin,
                });
                context.SaveChanges();
            }
            services.AddSingleton<TodoContext>(o => context);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
