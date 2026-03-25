using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopEasy.Console.Models;

//namespace ShopEasy.Console.Data.Configurations
//{
//    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
//    {
//        public void Configure(EntityTypeBuilder<Category> builder)
//        {
//            builder.ToTable("Categories", "shop");

//            builder.HasKey(c => c.CategoryId);

//            builder.Property(c => c.Name)
//                   .IsRequired()
//                   .HasMaxLength(100);

//            builder.Property(c => c.Slug)
//                   .IsRequired()
//                   .HasMaxLength(120);

//            builder.HasIndex(c => c.Slug)
//                   .IsUnique()
//                   .HasDatabaseName("IX_Categories_Slug");

//            builder.Ignore(c => c.InternalNotes);

//            builder.HasOne(c => c.ParentCategory)
//                   .WithMany(c => c.SubCategories)
//                   .HasForeignKey(c => c.ParentCategoryId)
//                   .OnDelete(DeleteBehavior.Restrict);
//        }
//    }
//}

namespace ShopEasy.Console.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "shop");

            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Slug)
                   .IsRequired()
                   .HasMaxLength(120);

            builder.HasIndex(c => c.Slug)
                   .IsUnique()
                   .HasDatabaseName("IX_Categories_Slug");

            builder.Ignore(c => c.InternalNotes);

            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.SubCategories)
                   .HasForeignKey(c => c.ParentCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
