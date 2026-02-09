using System;

namespace Small_Library
{
    class Magazine : LibraryItem
    {
        public int IssueNumber;
        public string Publisher;

        public Magazine(string title, string itemId, int issueNumber, string publisher)
            : base(title, itemId)
        {
            IssueNumber = issueNumber;
            Publisher = publisher;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine("Library Item: " + Title);
            Console.WriteLine("ID: " + ItemId);
            Console.WriteLine("Issue Number: " + IssueNumber);
            Console.WriteLine("Publisher: " + Publisher);
            Console.WriteLine();
        }
    }
}
