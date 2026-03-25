using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class Review
    {
        public int ReviewId { get; set; }          // Primary Key
        public int ProductId { get; set; }         // FK to Product
        public int CustomerId { get; set; }        // FK to Customer
        public int Rating { get; set; }            // Required
        public string? Comment { get; set; }       // Optional, max 1000 chars
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual Product Product { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
    }
}
