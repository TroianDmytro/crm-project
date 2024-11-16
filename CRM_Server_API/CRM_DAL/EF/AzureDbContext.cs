using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CRM_DAL.Entitys;
using CRM_DAL.Entitys.Auth;
using System.Reflection.Emit;

namespace CRM_DAL.EF
{
    public class AzureDbContext : IdentityDbContext<EmployeeRegisterModel>
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

            builder.Entity<Deal>()
               .Property(d => d.Amount)
               .HasPrecision(8, 2); // 8 - точність, 2 - кількість знаків після коми

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(8, 2); // точність і масштаб
            base.OnModelCreating(builder);
        }
    }
}
