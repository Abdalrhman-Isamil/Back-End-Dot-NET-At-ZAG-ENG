using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    internal class Program3
    {
        static void Main(string[] args)
        {
            var productList = new List<Product>
            {
                new(1, "Laptop", 1200m, "Electronics"),
                new(2, "Phone", 800m, "Electronics"),
                new(3, "Desk", 350m, "Furniture"),
                new(4, "Chair", 150m, "Furniture"),
                new(5, "Headphones", 200m, "Electronics"),
            };

            // 1) Convert to Array
            var productsAsArray = productList.ToArray();

            // 2) Convert to Dictionary keyed by Id
            var productsByIdDictionary = productList.ToDictionary(prod => prod.Id);

            // 3) Convert to HashSet of product Names
            var productNamesSet = productList
                .Select(prod => prod.Name)
                .ToHashSet();

            // 4) Convert to Lookup keyed by Category
            var productsByCategoryLookup = productList.ToLookup(prod => prod.Category);

            // → lookup["Electronics"] should return all electronics products
            var electronicsCategoryProducts = productsByCategoryLookup["Electronics"];

            foreach (var item in electronicsCategoryProducts)
            {
                Console.WriteLine(item.Name);
            }
        }
    }

    public record Product(int Id, string Name, decimal Price, string Category); 
    //? هنا استخدمت ريكورد عشان اختصر على نفسي بدل ما اكتب الكلاس تاني كلها من اول فكده اختصرتها في سطر (من المعلومات الجديدة اللي عرفتها وفرقت معايا جامد)
}
