using Microsoft.EntityFrameworkCore;

namespace WebApp.Data {

  public class DBInitializer {

    public static void SeedData (ApplicationDbContext context) {
      MigrateDB (context);

    }

    private static void MigrateDB (ApplicationDbContext context) {
      context.Database.Migrate ();
    }

  }
}