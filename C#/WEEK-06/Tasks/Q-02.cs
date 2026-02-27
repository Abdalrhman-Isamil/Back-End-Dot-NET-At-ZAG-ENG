using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var productList = new List<Product>
            {
                new (1, "Laptop", 1200m, "Electronics"),
                new (2, "Phone", 800m, "Electronics"),
                new (3, "Desk", 350m, "Furniture"),
                new (4, "Chair", 150m, "Furniture"),
                new (5, "Headphones", 200m, "Electronics"),
            };

            // 1
            var firstElectronicsProduct = productList
                .Where(prod => prod.Category == "Electronics")
                .FirstOrDefault();

            if (firstElectronicsProduct != null)
                Console.WriteLine(firstElectronicsProduct.Name);
            else
                Console.WriteLine("No Electronics item found.");

            // 2
            var lastExpensiveProduct = productList
                .Where(prod => prod.Price > 1000)
                .LastOrDefault();

            if (lastExpensiveProduct != null)
                Console.WriteLine(lastExpensiveProduct.Name);
            else
                Console.WriteLine("No product with Price > 1000 found.");

            // 3
            var singleFurnitureProduct = productList
                .Where(prod => prod.Category == "Furniture" && prod.Price > 300)
                .SingleOrDefault();

            if (singleFurnitureProduct != null)
                Console.WriteLine(singleFurnitureProduct.Name);
            else
                Console.WriteLine("No single Furniture item with Price > 300 found (or multiple matches).");

            // 4
            var productWithId3 = productList.Find(prod => prod.Id == 3);

            if (productWithId3 != null)
                Console.WriteLine(productWithId3.Name);
            else
                Console.WriteLine("Element in index 3 not exist");
        }
    }

    public class Product
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public string Category { set; get; }

        public Product(int id, string name, decimal price, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }
}
