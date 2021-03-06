﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Configs;
using WebApp.Data;
using WebApp.Hubs;

namespace WebApp {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices (IServiceCollection services) {
            services.AddContextConfig (Configuration);
            services.AddIdentityConfig (Configuration);
            services.AddInjectionDependency ();
            services.AddMVCConfig ();

        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            app.AddEnvConfig (env);
            app.UseHttpsRedirection ();
            app.UseStaticFiles ();
            app.UseCookiePolicy ();
            // todo: MigrateDB with two context....
            //DBInitializer.SeedData (context);
            app.UseAuthentication ();
            app.AddRouteConfig ();

        }
    }
}