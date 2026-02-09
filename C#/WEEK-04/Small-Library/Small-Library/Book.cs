using System;

namespace Small_Library
{
    class Book : LibraryItem
    {
        public string Author;
        public int Pages;

        public Book(string title, string itemId, string author, int pages)
            : base(title, itemId)
        {
            Author = author;
            Pages = pages;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("Library Item: " + Title);
            Console.WriteLine("ID: " + ItemId);
            Console.WriteLine("Author: " + Author);
            Console.WriteLine("Pages: " + Pages);
            Console.WriteLine();
        }
    }
}
