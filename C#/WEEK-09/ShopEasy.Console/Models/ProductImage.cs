using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public string Url { get; set; } = null!;  // required
        public string? AltText { get; set; }      // optional
        public bool IsPrimary { get; set; } = false;

        // Navigation property
        public virtual Product Product { get; set; } = null!;
    }
}