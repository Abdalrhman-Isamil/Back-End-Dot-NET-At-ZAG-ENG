using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }         // Primary Key (with sequence)
        public string Code { get; set; } = null!;   // required, max 30 chars
        public decimal Percentage { get; set; }     // decimal(5,2)
        public DateTime ExpiresAt { get; set; }
        public bool IsActive { get; set; } = true;  // default true
        public int MaxUses { get; set; } = 100;     // default 100
        public int CurrentUses { get; set; } = 0;   // default 0
    }
}
