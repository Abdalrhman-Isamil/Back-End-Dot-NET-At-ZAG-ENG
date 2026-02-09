using System;

namespace Small___Shop
{
    enum Category
    {
        Food,
        Books,
        Clothing
    }

    struct Location
    {
        public int X;
        public int Y;

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    class Product
    {
        private string name;
        private double price;
        private Category _category;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Invalid Price");
                }
                else
                {
                    price = value;
                }
            }
        }

        public Category Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }

        public Location Location
        {
            get;
            set;
        }

        public Product(string name, double price, Category category)
        {
            this.name = name;
            this.Category = category;
            this.Price = price;
            this.Location = new Location(0, 0);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Product p = new Product("Design Patterns GoF", 1500, Category.Books);
            Console.WriteLine("\n============ << My - Store >> ==============\n");
            Console.WriteLine($"Name : {p.Name}");
            Console.WriteLine($"Price : {p.Price}");
            Console.WriteLine($"Category : {p.Category}");
            Console.WriteLine($"Location : X = {p.Location.X} , Y = {p.Location.Y}");

            p.Price = -20; //? To Test Validation-)

            Console.WriteLine("\n============================================\n");
        }
    }
}
