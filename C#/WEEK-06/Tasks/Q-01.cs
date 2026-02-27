using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> myNumbers = new List<int> { 3, 18, 7, 42, 10, 5, 29, 14, 6, 100 };

            var filteredNumbersMethod = myNumbers
                .Where(number => number % 2 == 0 && number > 10)
                .Select(number => number)
                .OrderByDescending(number => number)
                .ToList();

            var filteredNumbersQuery =
                from number in myNumbers
                where number % 2 == 0 && number > 10
                orderby number descending
                select number;

            foreach (var item in filteredNumbersMethod)
                Console.Write($"{item} ");

            Console.WriteLine();

            foreach (var item in filteredNumbersQuery)
                Console.Write($"{item} ");
        }
    }
}
