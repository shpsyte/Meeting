using AutoMapper;
using Business.Interfaces;
using Business.Notifications;
using Business.Services;
using Data.Context;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Configs {
    public static class InjectionDependencyConfig {
        public static IServiceCollection AddInjectionDependency (this IServiceCollection services) {

            services.AddScoped<AppDbContext> ();
            services.AddScoped<IMeetingRepository, MeetingRepository> ();
            services.AddScoped<IMeetingSetupRepository, MeetingSetupRepository> ();
            services.AddScoped<IMeetingServices, MeetingServices> ();

            services.AddScoped<INotificador, Notificador> ();
            services.AddAutoMapper (typeof (Startup));

            return services;
        }
    }

    public static class ContextConfig {
        public static IServiceCollection AddContextConfig (this IServiceCollection services, IConfiguration configuration) {

            services.AddDbContext<AppDbContext> (options =>
                options.UseSqlServer (
                    configuration.GetConnectionString ("DefaultConnection")));

            return services;
        }
    }
}