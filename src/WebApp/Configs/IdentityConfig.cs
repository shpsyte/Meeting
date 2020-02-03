using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Data;

namespace WebApp.Configs
{
  public static class IdentityConfig {
        public static IServiceCollection AddIdentityConfig (this IServiceCollection services, IConfiguration configuration) {

            services.Configure<CookiePolicyOptions> (options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext> (options =>
                options.UseSqlServer (
                    configuration.GetConnectionString ("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser> ()
                .AddDefaultUI (UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext> ();

            return services;
        }
    }
}