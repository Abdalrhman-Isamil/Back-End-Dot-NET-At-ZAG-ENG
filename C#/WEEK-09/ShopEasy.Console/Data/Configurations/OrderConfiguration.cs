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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Table mapping
            builder.ToTable("Orders", "shop");

            // Primary Key
            builder.HasKey(o => o.OrderId);

            // Columns
            builder.Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .HasDefaultValue(OrderStatus.Pending);

            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(o => o.PlacedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            // Filtered index on pending orders
            builder.HasIndex(o => o.Status)
                .HasDatabaseName("IX_Orders_PendingStatus")
                .HasFilter("[Status] = 'Pending'");

            // One-to-many with Customer
            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
