using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Panta2.Core.Entities;

namespace Panta2.Core
{
    public class Panta2DbContext : IdentityDbContext
    {
        //public DbSet<Company> Companies { get; set; }
        //public DbSet<Service> Services { get; set; }
        //public DbSet<CompanyService> CompanyServices { get; set; }
        //public DbSet<ServiceRole> ServiceRoles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<Favorite> Favorites { get; set; }


        //public Panta2DbContext(DbContextOptions<Panta2DbContext> options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Panta2Database");
        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    builder.Entity<Company>()
        //        .HasMany(c => c.Services)
        //        .WithMany(s => s.Companies)
        //        .UsingEntity<CompanyService>(
        //            cs => cs.HasOne(cs => cs.Service).WithMany().HasForeignKey(cs => cs.ServiceId),
        //            cs => cs.HasOne(cs => cs.Company).WithMany().HasForeignKey(cs => cs.CompanyId),
        //            cs =>
        //            {
        //                cs.HasKey(cs => new { cs.CompanyId, cs.ServiceId });
        //                cs.Property(cs => cs.IsEnabled).HasDefaultValue(true);
        //                cs.Property(cs => cs.Order).HasDefaultValue(0);
        //            }
        //        );
        //}
    }
}
