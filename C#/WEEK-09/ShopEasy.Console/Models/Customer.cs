using ShopEasy.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; } // XML جوه الـ .csproj من الـ ( Nullable Reference Types هرجعها بعد ما افعل الـ ) PhoneNumber وده مناسب جدًا لـلـ null  في علامة استفهام هنا انا شيلتها موقتا بعد كلمة "سترينج" تدل على أن القيمة ممكن تكون 

        public DateTime CreatedAt { get; set; }

        // Navigation Property (Relations) (  Entity بــ  Entity يعني y) بتربط كلاس بكلاس تاني Property : ببساطة دي
        public virtual CustomerProfile? CustomerProfile { get; set; }

        public virtual ICollection<Order>? Orders { get; set; }
        //Add this for Review relationship
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}