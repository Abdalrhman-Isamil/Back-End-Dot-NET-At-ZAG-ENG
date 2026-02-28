using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    internal class Program8
    {
        static void Main(string[] args)
        {
            //! Q1) Predict output BEFORE running -> Deferred Execution
            List<int> numbers = new List<int> { 1, 2, 3, 4, 5 };

            var deferredQuery = numbers.Where(n => n > 2); //? لسه متنفذش

            numbers.Add(10); //? الاضافة بتتم بعد مانعرف الكويري مش اميديت يعني 

            Console.WriteLine("Q1) Deferred Execution result:");
            foreach (var n in deferredQuery)
            {
                Console.Write(n + " "); //? 3 4 5 10
            }

            Console.WriteLine();

            /*
            todo ( Explanation Q1 )
            ? foreach فكده هتتنفذ لما نيجي نلوب عليها بالــ  Deferred Execution بتتنفذ Where() لان 
            ? لكن التعديل بيكون بعد numbers = [1,2,3,4,5] وعرفنا الكويري لما الـ 
            ? n > 2 بعد كده الكونديشن 
            ? [1,2,3,4,5,10] و بعد ما عملنا ادد 10 هتكون الليست 
            ! ويبقا الاوت بوت اللي المفروض يطلع 3 4 5 10
            */

            //! Q2) What is printed? Why? 
            /*
            todo ( Explanation Q2 نفس تفسير اول سؤال) 
            ? LINQ query (Where) is deferred :
            ? - It doesn't execute at definition time .
            ? - It executes at enumeration time (foreach).
            ? - So it sees the updated numbers list .
            */

            //! Q3) How would using .ToList() right after .Where(...) change the result? -> Immediate Execution with ToList()
            List<int> numbers2 = new List<int> { 1, 2, 3, 4, 5 };

            var immediateQuery = numbers2
                .Where(n => n > 2)
                .ToList(); //? snapshot <- دي بتتنفذ اميديت هنا 

            numbers2.Add(10); //? snapshot التعديل هنا بعد الــ 

            Console.WriteLine("Q3) Immediate Execution (ToList) result:");
            foreach (var n in immediateQuery)
            {
                Console.Write(n + " "); //? 3 4 5
            }

            Console.WriteLine();

            /* 
            todo ( Explanation Q3 )
            ? snapshot <- هينفذ اميديت  
            ? بمعنى هيعمل العملية والنمبرز لحالها بنفس قيمها اللي هو قبل ما يعمل الادد للـ 10فكده الناتج هيكون 3 4 5 
            */

            //! Q4) Name 3 LINQ operators that trigger immediate execution 
            /*
            todo ( هقسمهم بناء على تجويد من عندي مش فالسؤال بما اني مذاكرهم يعني )
            ! (تحول الداتا وتخزنها في الميموري)
            1) ToList()
            2) ToArray()
            3) ToDictionary()
            4) ToHashSet()
            5) ToLookup()
            ! (ترجع عنصر واحد)
            6) First()
            7) FirstOrDefault()
            8) Single()
            9) SingleOrDefault()
            10) Last()
            11) LastOrDefault()
            12) ElementAt()
            13) ElementAtOrDefault()              
            ! (تحسب قيمة)
            14) Count()
            15) LongCount()
            16) Sum()
            17) Average()
            18) Min()
            19) Max()              
            ! (bool ترجع )
            20) Any()
            21) All()
            22) Contains()
            */
        }
    }
}
