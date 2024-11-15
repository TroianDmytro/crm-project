using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CRM_DAL.Entitys;

namespace CRM_DAL.EF
{
    public class AzureDbContext : IdentityDbContext<IdentityUser>
    {
        public AzureDbContext(DbContextOptions<AzureDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealProduct> DealProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DealProduct>()
                .HasKey(dp => new { dp.DealId, dp.ProductId });

            builder.Entity<DealProduct>()
                .HasOne(dp => dp.Deal)
                .WithMany(dp => dp.DealProducts)
                .HasForeignKey(dp => dp.DealId);

            builder.Entity<DealProduct>()
                .HasOne(dp => dp.Product)
                .WithMany(dp => dp.DealProducts)
                .HasForeignKey(dp => dp.ProductId);

            base.OnModelCreating(builder);
        }
    }
}
