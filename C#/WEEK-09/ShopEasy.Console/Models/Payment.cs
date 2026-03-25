using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }          // Primary Key
        public int OrderId { get; set; }            // FK to Order
        public string Method { get; set; } = null!; // required
        public PaymentStatus Status { get; set; }   // Enum
        public DateTime PaidAt { get; set; }
        public decimal Amount { get; set; }

        // Navigation property
        public virtual Order Order { get; set; } = null!;
    }

    public enum PaymentStatus
    {
        Pending,
        Completed,
        Refunded
    }
}
