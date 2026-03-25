using ShopEasy.Console.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopEasy.Console.Models
{
    public class CustomerProfile
    {
        public int CustomerProfileId { get; set; }

        public int CustomerId { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        public string? NationalId { get; set; }

        // Navigation Property (Relations) (  Entity بــ  Entity يعني y) بتربط كلاس بكلاس تاني Property : ببساطة دي
        public Customer? Customer { get; set; }

    }
}

