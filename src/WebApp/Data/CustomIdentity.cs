using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Data {
    public static class CustomIdentity {
        public static ModelBuilder LoadIdentity (this ModelBuilder model) {
            model.Entity<IdentityUser> ().ToTable ("User");
            model.Entity<IdentityRole> ().ToTable ("Role");
            model.Entity<IdentityUserClaim<string>> ().ToTable ("UserClaim");
            model.Entity<IdentityUserClaim<string>> ().HasKey (a => new { a.UserId, a.Id });

            model.Entity<IdentityUserRole<string>> ().ToTable ("UserRole");
            model.Entity<IdentityUserRole<string>> ().HasKey (a => new { a.UserId, a.RoleId });

            model.Entity<IdentityUserLogin<string>> ().ToTable ("UserLogin");
            model.Entity<IdentityUserLogin<string>> ().HasKey (a => new { a.UserId, a.ProviderKey });

            model.Entity<IdentityRoleClaim<string>> ().ToTable ("RoleClaim");
            model.Entity<IdentityRoleClaim<string>> ().HasKey (a => new { a.RoleId, a.Id });
            return model;
        }

    }
}