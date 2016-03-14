using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Data.Entity;
using Sharebook.Models;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Authentication.Cookies;
using System.Net;
using AutoMapper;
using Sharebook.ViewModels;

namespace Sharebook
{
    public class Startup
    {
        public static IConfigurationRoot Configuration { get; set; }
        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            services.AddEntityFramework()
            .AddSqlite()
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]);
            });

            services.AddScoped<ISharebookRepository, SharebookRepository>();
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                config.Cookies.ApplicationCookie.LoginPath = "/auth/Login";
                 config.Cookies.ApplicationCookie.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx => {
                        if (ctx.Request.Path.StartsWithSegments("/api/users") &&
                        ctx.Response.StatusCode == (int)HttpStatusCode.OK) {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                };
            }
            )
            .AddEntityFrameworkStores<ApplicationDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseIdentity();
            Mapper.Initialize(config=> {
                config.CreateMap<RegisterViewModel,ApplicationUser>()
                        .ForMember(dest =>dest.City,
                                    options => options.Ignore())
                       .ReverseMap();
                config.CreateMap<City, CityViewModel>().ReverseMap();
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
            template: "{Controller=Home}/{Action=Index}/{id?}");
            });

        }

        // Entry point for the application.
        public static void Main(string[] args) => Microsoft.AspNet.Hosting.WebApplication.Run<Startup>(args);
    }
}
