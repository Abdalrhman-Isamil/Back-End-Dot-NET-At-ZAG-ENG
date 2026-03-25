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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder) {
            builder.ToTable("Customers", "shop");
            builder.HasKey(C => C.CustomerId);
            builder.Property(C => C.CustomerId).HasColumnName("customer_id");
            builder.Property(C => C.FullName).IsRequired().HasMaxLength(150).HasColumnName("full_name").HasComment("Customer full legal name");
            builder.Property(C => C.Email).IsRequired().HasMaxLength(250);
            builder.HasIndex(C => C.Email).IsUnique().HasDatabaseName("IX_Customers_Email");
            builder.Property(C => C.PhoneNumber).HasMaxLength(20);
            builder.Property(C => C.CreatedAt).HasDefaultValueSql("GETUTCDATE()");


        }
    }
}

 