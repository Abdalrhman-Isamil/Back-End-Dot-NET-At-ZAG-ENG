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
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            // Table mapping
            builder.ToTable("ProductImages", "shop");

            // Primary Key (assumed by convention)
            builder.HasKey(pi => pi.ProductImageId);

            // Columns
            builder.Property(pi => pi.Url)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(pi => pi.AltText)
                .HasMaxLength(200);

            builder.Property(pi => pi.IsPrimary)
                .HasDefaultValue(false);

            // One-to-one with Product
            builder.HasOne(pi => pi.Product)
                .WithOne(p => p.ProductImage)
                .HasForeignKey<ProductImage>(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}