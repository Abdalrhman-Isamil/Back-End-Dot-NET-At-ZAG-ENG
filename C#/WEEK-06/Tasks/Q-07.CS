using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    internal class Program6
    {
        static void Main(string[] args)
        {
            List<int> studentScores = new List<int> { 88, 92, 75, 60, 55, 80, 91, 45 };

            // 1) TakeWhile score >= 70
            var scoresAbove70FromStart = studentScores
                .TakeWhile(score => score >= 70)
                .ToList();

            Console.WriteLine("1) TakeWhile >= 70:");
            Console.WriteLine(string.Join(", ", scoresAbove70FromStart));

            // 2) SkipWhile score >= 70
            var scoresAfterFirstDrop = studentScores
                .SkipWhile(score => score >= 70)
                .ToList();

            Console.WriteLine("\n2) SkipWhile >= 70:");
            Console.WriteLine(string.Join(", ", scoresAfterFirstDrop));

            // ! 3) Difference between this and using Where
            /*
            TakeWhile:
            - ياخد العناصر طالما الشرط صح
            - يقف عند أول عنصر يخالف الشرط

            SkipWhile:
            - يسكيب العناصر طالما الشرط صحيح
            - عند أول عنصر يخالف الشرط يرجع الباقي كله

            Where:
            - يتشيك كل العناصر
            - يرجع كل العناصر اللي تحقق الشرط بغض النظر عن الترتيب
            */
        }
    }
}
