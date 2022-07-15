using BoxOffice.Core.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BoxOffice.Core.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Spectacle> Spectacles { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<Admin>().HasIndex(x => x.Email).IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
