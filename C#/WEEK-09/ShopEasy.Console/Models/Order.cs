using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class Order
    {
        public int OrderId { get; set; }             // Primary Key
        public int CustomerId { get; set; }          // FK to Customer
        public OrderStatus Status { get; set; }      // Enum
        public decimal TotalAmount { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime? ShippedAt { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual Payment Payment { get; set; } = null!;
    }

    public enum OrderStatus
    {
        Pending,
        Cancelled,
        Delivered
    }
}
