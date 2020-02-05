using Business.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Context {
    public class AppDbContext : DbContext {

        public DbSet<Meeting> Meeting { get; set; }
        public DbSet<MeetingSetup> MeetingSetup { get; set; }
        public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);

            modelBuilder.AddConfigContext ();
        }
    }
}