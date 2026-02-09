using System;

namespace Small_Library
{
    class LibraryItem
    {
        public string Title;
        public string ItemId;

        public LibraryItem(string title, string itemId)
        {
            Title = title;
            ItemId = itemId;
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine("Library Item: " + Title);
            Console.WriteLine("ID: " + ItemId);
        }
    }
}
