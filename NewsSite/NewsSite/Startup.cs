using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsSite.Data;
using System;
using System.Linq;

namespace NewsSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdministratorRights", policy => policy.RequireClaim("NewsAppRole", "Administrator"));
                options.AddPolicy("PublisherRights", policy => policy.RequireClaim("NewsAppRole", "Publisher"));
                options.AddPolicy("SubscriberRights", policy => policy.RequireClaim("NewsAppRole", "Subscriber"));
                options.AddPolicy("AnonymousRights", policy => policy.RequireClaim("NewsAppRole", "Anonymous"));
                options.AddPolicy("ViewRights", policy => policy.RequireAssertion(context =>
                {
                    var user = context.User;
                    int realAge;
                    var adminRights = user.Claims.Where(a => a.Type == "NewsAppRole").Select(a => a.Value).FirstOrDefault();
                    var ageValue = user.Claims.Where(c => c.Type == "Age").Select(c => c.Value).SingleOrDefault();
                    Int32.TryParse(ageValue, out realAge);

                    if (adminRights == "Administrator")
                    {
                        return true;
                    }
                    else if (adminRights == "Publisher")
                    {
                        return true;
                    }
                    else if (adminRights == "Subscriber" && realAge > 20)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }));
               
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}

