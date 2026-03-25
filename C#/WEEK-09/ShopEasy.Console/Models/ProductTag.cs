using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class ProductTag
    {
        public int ProductId { get; set; }

        public int TagId { get; set; }

        // Navigation
        public Product? Product { get; set; }

        public Tag? Tag { get; set; }
    }
}
