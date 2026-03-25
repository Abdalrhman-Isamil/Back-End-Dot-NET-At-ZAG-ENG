using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        public string? Name { get; set; }

        // Navigation
        public ICollection<ProductTag>?  ProductTags { get; set; }
    }
}
