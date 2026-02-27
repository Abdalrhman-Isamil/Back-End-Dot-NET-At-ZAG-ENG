using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    internal class Program4
    {
        static void Main(string[] args)
        {
            List<string> orderCodes = new List<string>
            {
                "ORD-001", "ORD-002", "ORD-003",
                "ORD-004", "ORD-005", "ORD-006", "ORD-007"
            };

            int itemsPerPage = 3;

            // 1) Get Page 1 (items 1-3)
            var firstPageOrders = orderCodes
                .Take(itemsPerPage)
                .ToList();

            Console.WriteLine("Page 1: " + string.Join(", ", firstPageOrders));

            // 2) Get Page 2 (items 4-6)
            var secondPageOrders = orderCodes
                .Skip(itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            Console.WriteLine("Page 2: " + string.Join(", ", secondPageOrders));

            // 3) Get the last 2 orders
            var lastTwoOrders = orderCodes
                .TakeLast(2)
                .ToList();

            Console.WriteLine("Last 2: " + string.Join(", ", lastTwoOrders));

            // 4) Drop the first and last order
            var withoutFirstAndLast = orderCodes
                .Skip(1)
                .SkipLast(1)
                .ToList();

            Console.WriteLine("Drop first & last: " + string.Join(", ", withoutFirstAndLast));

            // BONUS
            var thirdPageOrders = Paginate(orderCodes, 3, itemsPerPage);
            Console.WriteLine("Page 3 (BONUS): " + string.Join(", ", thirdPageOrders));
        }

        static List<T> Paginate<T>(IEnumerable<T> sourceCollection, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "pageNumber must be >= 1");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize must be >= 1");

            return sourceCollection
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        /*
        ?  (Deferred Execution عشان تبقا) List بدل IEnumerable نسخة بترجع 
        
        static IEnumerable<T> PaginateDeferred<T>(IEnumerable<T> source, int pageNumber, int pageSize)
{
    if (pageNumber < 1) throw new ArgumentOutOfRangeException(nameof(pageNumber));
    if (pageSize < 1) throw new ArgumentOutOfRangeException(nameof(pageSize));

    return source
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize);
        ! ToList التنفيذ لسه متأجل مفيش
}
         */
    }
}
