using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Console.Models;

namespace ShopEasy.Console.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "shop");

            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.IsActive)
                   .HasDefaultValue(true);

            builder.Property(p => p.Name)
                   .IsRequired();

            builder.Property(p => p.SKU)
                   .IsRequired();

            builder.HasIndex(p => p.SKU)
                   .IsUnique()
                   .HasDatabaseName("IX_Products_SKU");

            builder.Property(p => p.StockQuantity)
                   .IsRequired();

            builder.Property<string>("DisplayName")
                   .HasComputedColumnSql("[Name] + ' (' + [SKU] + ')'", true);

            builder.HasQueryFilter(p => p.IsActive);

            builder.HasOne(p => p.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
