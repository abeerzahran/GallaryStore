using GallaryStore.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GallaryStore.dbContext
{
    public class GallaryContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> appUsers { get; set; }
        public DbSet<Favourite> favourites { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderProducts> orderProducts { get; set; }
        public DbSet<Product> products { get; set; }

        public GallaryContext():base()
        {
            
        }

        public GallaryContext(DbContextOptions<GallaryContext>options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Favourite>().HasKey("productId", "userId");
            builder.Entity<OrderProducts>().HasKey("productId", "orderId");

            base.OnModelCreating(builder);
            //builder.Entity<VwUsers>().HasNoKey().ToView("VwUsers");
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server =.; Database = GallaryDb; Integrated Security = True; TrustServerCertificate = True;");

        //}

    }
}
