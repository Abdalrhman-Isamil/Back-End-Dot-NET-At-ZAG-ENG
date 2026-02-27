using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    internal class Program2
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

            // 1) Are ALL products priced above 100?
            bool areAllProductsAbove100 = productList.All(prod => prod.Price > 100);
            Console.WriteLine($"1) All products priced > 100? => {areAllProductsAbove100}");

            // 2) Is there ANY product in the 'Gaming' category?
            bool isThereGamingCategory = productList.Any(prod => prod.Category == "Gaming");
            Console.WriteLine($"2) Any product in 'Gaming'? => {isThereGamingCategory}");

            // 3) Does the collection CONTAIN a product named 'Chair'?
            bool containsChairProduct = productList.Any(prod => prod.Name == "Chair");
            Console.WriteLine($"3) Contains product named 'Chair'? => {containsChairProduct}");

            // 4) Are ALL Electronics products priced above 500?
            bool areAllElectronicsAbove500 = productList
                .Where(prod => prod.Category == "Electronics")
                .All(prod => prod.Price > 500);

            Console.WriteLine($"4) All Electronics priced > 500? => {areAllElectronicsAbove500}");

            // 5) Is there ANY product cheaper than 200?
            bool isThereCheapProduct = productList.Any(prod => prod.Price < 200);
            Console.WriteLine($"5) Any product cheaper than 200? => {isThereCheapProduct}");
        }
    }
}
