using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    record Employee(string Name, decimal Salary, string Department);

    internal class Program5
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = new List<Employee>
            {
                new("Nadia", 9500m, "Engineering"),
                new("Ali", 7200m, "Engineering"),
                new("Sara", 6800m, "HR"),
                new("Omar", 8800m, "Engineering"),
                new("Mona", 6000m, "Finance")
            };

            // 1) Anonymous Projection
            var upperNameWithSalary = employeeList
                .Select(emp => new
                {
                    FullName = emp.Name.ToUpper(),
                    emp.Salary
                }).ToList();

            Console.WriteLine("1) Anonymous Projection:");
            foreach (var emp in upperNameWithSalary)
            {
                Console.WriteLine($"{emp.FullName} - {emp.Salary}");
            }

            // 2) Formatted Projection
            var formattedEmployeeInfo = employeeList
                .Select(emp => $"{emp.Name} works in {emp.Department} - EGP {emp.Salary:N0}").ToList();

            Console.WriteLine("\n2) Formatted Projection:");
            foreach (var emp in formattedEmployeeInfo)
            {
                Console.WriteLine(emp);
            }

            // 3) Ranked Employees
            var rankedEmployeeList = employeeList
                .OrderByDescending(emp => emp.Salary)
                .Select((emp, index) => new
                {
                    Rank = index + 1,
                    emp.Name,
                    emp.Salary
                }).ToList();

            Console.WriteLine("\n3) Ranked Employees:");
            foreach (var emp in rankedEmployeeList)
            {
                Console.WriteLine($"Rank {emp.Rank}: {emp.Name} - {emp.Salary}");
            }

            //? Conditional Operator استخدمت BONUS فكرة الــ 
            var employeesWithSeniorityLevel = employeeList
                .Select(emp => new
                {
                    emp.Name,
                    emp.Salary,
                    emp.Department,
                    SeniorityLevel =
                        emp.Salary >= 9000 ? "Senior" :
                        emp.Salary >= 7000 ? "Mid" :
                        "Junior"
                }).ToList();

            Console.WriteLine("\nBONUS - Seniority Level:");
            foreach (var emp in employeesWithSeniorityLevel)
            {
                Console.WriteLine($"{emp.Name} - {emp.SeniorityLevel}");
            }
        }
    }
}
