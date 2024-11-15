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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealProduct> DealProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Deal>()
                .HasOne(d => d.Customer)
                .WithMany(c => c.Deals)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DealProduct>()
                .HasKey(dp => dp.DealItemId);

            builder.Entity<DealProduct>()
                .HasOne(dp => dp.Deal)
                .WithMany(d => d.DealProducts)
                .HasForeignKey(dp => dp.DealId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DealProduct>()
                .HasOne(dp => dp.Product)
                .WithMany(p => p.DealProducts)
                .HasForeignKey(dp => dp.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
