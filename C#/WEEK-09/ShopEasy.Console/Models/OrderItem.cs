using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }      // Primary Key
        public int OrderId { get; set; }          // FK to Order
        public int ProductId { get; set; }        // FK to Product
        public int Quantity { get; set; }         // Required
        public decimal UnitPrice { get; set; }    // decimal(18,2)

        // Navigation properties
        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
