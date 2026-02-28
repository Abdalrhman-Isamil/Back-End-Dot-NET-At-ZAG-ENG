using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    record Employee(string Name, string Department, decimal Salary);
    record Course(string Title, List<string> Students);

    internal class Program11
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new()
            {
                new("Ali",  "Engineering", 9000m),
                new("Sara", "Engineering", 8500m),
                new("Nada", "Engineering", 9500m),
                new("Omar", "HR", 6000m),
                new("Mona", "HR", 6200m),
                new("Yara", "Marketing", 7000m),
                new("Karim","Marketing", 7500m),
            };

            List<Course> courses = new()
            {
                new("C# Basics",    new List<string> { "Ali", "Sara", "Omar" }),
                new("LINQ Mastery", new List<string> { "Sara", "Mona", "Ali" }),
                new("ASP.NET Core", new List<string> { "Yara", "Omar", "Karim" }),
            };

            //! 1) Top 2 highest-paid employees per dept
            //? GroupBy/OrderByDescending/Take/SelectMany are Deferred (query building)
            //? ToList() at the end is Immediate (executes now)
            var top2PerDept = employees
                .GroupBy(e => e.Department)
                .SelectMany(g => g.OrderByDescending(e => e.Salary).Take(2))
                .ToList();

            Console.WriteLine("Top 2 per dept:");
            foreach (var e in top2PerDept)
                Console.WriteLine($"{e.Department} - {e.Name} - {e.Salary}");

            //! 2)  Dictionary<CourseName, StudentCount> only Courses with > 2 students
            //? Where/Select are Deferred
            //? ToDictionary is Immediate (executes now) + requires unique keys
            var courseCounts = courses
                .Where(c => c.Students.Count > 2)
                .ToDictionary(c => c.Title, c => c.Students.Count);

            Console.WriteLine("\nCourses dict:");
            foreach (var kv in courseCounts)
                Console.WriteLine($"{kv.Key} -> {kv.Value}");

            //! 3) Checks (Any / All)
            //? Any is Immediate (returns bool now)
            bool anyEngLess8000 = employees.Any(e => e.Department == "Engineering" && e.Salary < 8000);
            //? Where is Deferred, but All is Immediate (returns bool now)
            bool allHrAbove5500 = employees.Where(e => e.Department == "HR").All(e => e.Salary > 5500);

            Console.WriteLine($"\nAny Engineering <8000? {anyEngLess8000}");
            Console.WriteLine($"All HR >5500? {allHrAbove5500}");

            //! 4) Project top-2 per dept with Rank + Seniority
            string GetLevel(decimal s) => s >= 9000 ? "Senior" : s >= 7000 ? "Mid" : "Junior";

            //? Up to SelectMany is Deferred; ToList is Immediate.
            //? Inside each group:
            //?  - OrderByDescending Deferred
            //?  - Take(2) Deferred
            //?  - Select((e, index) => ...) Deferred

            var top2Projected = employees
                .GroupBy(e => e.Department)
                .SelectMany(g => g.OrderByDescending(e => e.Salary)
                                .Take(2)
                                .Select((e, i) => new
                                {
                                    Rank = i + 1, //? ده اللي هيعمل ريسيت لكل جروب لان كل اندكس موجود جوه الجروب 
                                    e.Name,
                                    e.Department,
                                    e.Salary,
                                    Level = GetLevel(e.Salary)
                                }))
                .ToList();

            Console.WriteLine("\nTop-2 per dept projected:");
            foreach (var x in top2Projected.OrderBy(x => x.Department).ThenBy(x => x.Rank))
                Console.WriteLine($"{x.Department} | Rank {x.Rank} | {x.Name} | {x.Salary} | {x.Level}");

            //! 5) Deferred vs Immediate summary
            //? - Deferred: Where, Select, SelectMany, GroupBy, OrderByDescending, Take
            //? - Immediate: ToList, ToDictionary, Any, All (also Count/First/Single...)
        }
    }
}
