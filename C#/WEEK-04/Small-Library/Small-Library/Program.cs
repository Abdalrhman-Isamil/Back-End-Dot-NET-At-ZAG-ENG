using System;

namespace Small_Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Book book1 = new Book("C# Programming", "B001", "John Doe", 450);
            Magazine mag1 = new Magazine("Tech Monthly", "M001", 15, "Tech Media");

            book1.DisplayInfo();
            mag1.DisplayInfo();

            Console.ReadKey();
        }
    }
}
