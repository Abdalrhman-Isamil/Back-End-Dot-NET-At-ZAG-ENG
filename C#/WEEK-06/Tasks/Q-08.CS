using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ_Task
{
    record Employee(string Name, string Department, decimal Salary);

    internal class Program7
    {
        static void Main(string[] args)
        {
            List<Employee> employeeList = new List<Employee>
            {
                new("Ali", "Engineering", 9000m),
                new("Sara", "Engineering", 8500m),
                new("Omar", "HR", 6000m),
                new("Mona", "HR", 6200m),
                new("Yara", "Marketing", 7000m),
                new("Karim", "Marketing", 7500m),
                new("Nada", "Engineering", 9500m),
            };

            // 1) Group by Department + print count + average salary
            var groupedDepartments = employeeList
                .GroupBy(emp => emp.Department)
                .Select(group => new
                {
                    DepartmentName = group.Key,
                    EmployeeCount = group.Count(),
                    AverageSalary = group.Average(emp => emp.Salary),
                    TotalDepartmentSalary = group.Sum(emp => emp.Salary),
                    EmployeesInDepartment = group
                })
                .ToList();

            Console.WriteLine("1) Grouped Result:");
            foreach (var dept in groupedDepartments)
            {
                Console.WriteLine($"{dept.DepartmentName} + Count: {dept.EmployeeCount}, Avg: {dept.AverageSalary}");
            }

            // 2) Find the department with the highest total salary budget
            var departmentWithHighestBudget = groupedDepartments
                .OrderByDescending(dept => dept.TotalDepartmentSalary)
                .First();

            Console.WriteLine($"\n2) Department with highest total salary: {departmentWithHighestBudget.DepartmentName}");

            // 3) List employees in each group ordered by Salary descending
            Console.WriteLine("\n3) Employees in each department (Salary Desc):");

            foreach (var dept in groupedDepartments)
            {
                Console.WriteLine($"\nDepartment: {dept.DepartmentName}");

                var employeesOrderedBySalary = dept.EmployeesInDepartment
                    .OrderByDescending(emp => emp.Salary);

                foreach (var emp in employeesOrderedBySalary)
                {
                    Console.WriteLine($"{emp.Name} - {emp.Salary}");
                }
            }
        }
    }
}
