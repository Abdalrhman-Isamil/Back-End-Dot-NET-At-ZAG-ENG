using ShopEasy.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public string? SKU { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }

        // Navigation
        public virtual Category? Category { get; set; }

        public virtual ICollection<OrderItem>? OrderItems { get; set; }

        public virtual ICollection<ProductTag>? ProductTags { get; set; }

        public virtual ProductImage? ProductImage { get; set; }
          // for Review relationship
         public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
