using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Configs {
  public static class MVCConfig {
    public static IServiceCollection AddMVCConfig (this IServiceCollection services) {

      services.AddMvc (a => {
        a.Filters.Add (new AutoValidateAntiforgeryTokenAttribute ());
      }).SetCompatibilityVersion (CompatibilityVersion.Version_2_2);

      return services;
    }
  }
}