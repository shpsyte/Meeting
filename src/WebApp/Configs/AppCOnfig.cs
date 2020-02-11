using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using WebApp.Hubs;

namespace WebApp.Configs {
  public static class RouteConfig {
    public static IApplicationBuilder AddRouteConfig (this IApplicationBuilder app) {

      app.UseSignalR (routes => {
        routes.MapHub<newParticipantHub> ("/newParticipantHub");
        routes.MapHub<newLinkHub> ("/newLinkHub");
      });

      app.UseMvc (routes => {
        routes.MapRoute (
          name: "default",
          template: "{controller=Home}/{action=Index}/{id?}");
      });

      return app;
    }

    public static IApplicationBuilder AddEnvConfig (this IApplicationBuilder app, IHostingEnvironment env) {

      if (env.IsDevelopment ()) {
        app.UseDeveloperExceptionPage ();
        app.UseDatabaseErrorPage ();
      } else {
        app.UseExceptionHandler ("/Home/500");
        app.UseStatusCodePagesWithReExecute ("/Home/{0}");
        app.UseHsts ();
      }

      return app;
    }
  }
}