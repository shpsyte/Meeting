using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data.Context {
    public static class AppDbContextConfig {
        public static ModelBuilder AddConfigContext (this ModelBuilder modelBuilder) {
            foreach (var item in modelBuilder.Model.GetEntityTypes ()
                    .SelectMany (a => a.GetProperties ()
                        .Where (p => p.ClrType == typeof (string)))) {
                item.Relational ().ColumnType = "varchar(255)";

            }
            modelBuilder.ApplyConfigurationsFromAssembly (typeof (AppDbContext).Assembly);

            return modelBuilder;
        }

    }
}