using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models.IRepository;
using DataAccess.Models;
using DataAccess.Models.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using BugTriage.Search;

namespace BugTriage
{
    public class Startup
    {

       private IConfiguration configuration;
       public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        
            services.AddDbContextPool<BugTriageContext>(options=> options.UseNpgsql(configuration.GetConnectionString("NpgSql")));
            services.AddIdentity<IdentityUser,IdentityRole>(option=> {
                option.Password.RequiredLength = 6;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireDigit = false;
                option.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<BugTriageContext>();
          
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IBugRepository, BugRepository>();
            services.AddTransient<ISearchManager, SearchManager>();
            services.AddMvc();
            //services.AddMvc(option=>
            //{
            //    AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();

            //    option.Filters.Add(new AuthorizeFilter(policy));
            //});
            services.AddAuthorization(option=>
                option.AddPolicy("Admin",policy=>policy.RequireClaim("Admin"))
            );
            //services.AddDistributedMemoryCache();
            //services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSession();
            app.UseAuthentication();
            //app.UseCookiePolicy();
            //app.UseMvcWithDefaultRoute();
            app.UseMvc(route=>route.MapRoute("default","{controller=Home}/{action=Index}/{id?}"));

            
        }
    }
}
