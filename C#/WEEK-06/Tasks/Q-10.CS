using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    internal class Program9
    {
        static void Main(string[] args)
        {
            List<string> wordList = new List<string>
            {
                "apple", "fig", "banana", "kiwi",
                "grape", "mango", "pear", "plum"
            };

            //! 1) Filter words longer than 4 characters
            var wordsLongerThan4 = wordList
                .Where(w => w.Length > 4)
                .ToList();

            Console.WriteLine("1) Longer than 4 chars:");
            Console.WriteLine(string.Join(", ", wordsLongerThan4)); //? apple, banana, grape, mango

            //! 2) Filter words at even indexes 0,2,4,6 using indexed overload
            var wordsAtEvenIndexes = wordList
                .Where((word, index) => index % 2 == 0)
                .ToList();

            Console.WriteLine("\n2) Even index words:");
            Console.WriteLine(string.Join(", ", wordsAtEvenIndexes)); //? apple, banana, grape, pear

            //! 3) Combine both conditions: longer than 4 chars AND even index
            var combinedFilter = wordList
                .Where((word, index) => word.Length > 4 && index % 2 == 0)
                .ToList();

            Console.WriteLine("\n3) Combined filter:");
            Console.WriteLine(string.Join(", ", combinedFilter)); //? apple, banana, grape

            //! 4) Index of "mango" in step 1 result
            int indexOfMango = wordsLongerThan4.IndexOf("mango");

            Console.WriteLine($"\n4) Index of mango in step 1 result: {indexOfMango}"); //? 3
        }
    }
}
