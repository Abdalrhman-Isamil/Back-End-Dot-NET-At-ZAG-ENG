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

//? ======================================================================================================================

// using System;
// using System.Collections.Generic;
// using System.Linq;

namespace LINQ_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var productList = new List<Product>
            {
                new (1, "Laptop", 1200m, "Electronics"),
                new (2, "Phone", 800m, "Electronics"),
                new (3, "Desk", 350m, "Furniture"),
                new (4, "Chair", 150m, "Furniture"),
                new (5, "Headphones", 200m, "Electronics"),
            };

            // 1
            var firstElectronicsProduct = productList
                .Where(prod => prod.Category == "Electronics")
                .FirstOrDefault();

            if (firstElectronicsProduct != null)
                Console.WriteLine(firstElectronicsProduct.Name);
            else
                Console.WriteLine("No Electronics item found.");

            // 2
            var lastExpensiveProduct = productList
                .Where(prod => prod.Price > 1000)
                .LastOrDefault();

            if (lastExpensiveProduct != null)
                Console.WriteLine(lastExpensiveProduct.Name);
            else
                Console.WriteLine("No product with Price > 1000 found.");

            // 3
            var singleFurnitureProduct = productList
                .Where(prod => prod.Category == "Furniture" && prod.Price > 300)
                .SingleOrDefault();

            if (singleFurnitureProduct != null)
                Console.WriteLine(singleFurnitureProduct.Name);
            else
                Console.WriteLine("No single Furniture item with Price > 300 found (or multiple matches).");

            // 4
            var productWithId3 = productList.Find(prod => prod.Id == 3);

            if (productWithId3 != null)
                Console.WriteLine(productWithId3.Name);
            else
                Console.WriteLine("Element in index 3 not exist");
        }
    }

    public class Product
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public decimal Price { set; get; }
        public string Category { set; get; }

        public Product(int id, string name, decimal price, string category)
        {
            Id = id;
            Name = name;
            Price = price;
            Category = category;
        }
    }
}


//? ======================================================================================================================

// using System;
// using System.Collections.Generic;
// using System.Linq;

namespace LINQ_Task
{
    internal class Program2
    {
        static void Main(string[] args)
        {
            var productList = new List<Product>
            {
                new (1, "Laptop", 1200m, "Electronics"),
                new (2, "Phone", 800m, "Electronics"),
                new (3, "Desk", 350m, "Furniture"),
                new (4, "Chair", 150m, "Furniture"),
                new (5, "Headphones", 200m, "Electronics"),
            };

            // 1) Are ALL products priced above 100?
            bool areAllProductsAbove100 = productList.All(prod => prod.Price > 100);
            Console.WriteLine($"1) All products priced > 100? => {areAllProductsAbove100}");

            // 2) Is there ANY product in the 'Gaming' category?
            bool isThereGamingCategory = productList.Any(prod => prod.Category == "Gaming");
            Console.WriteLine($"2) Any product in 'Gaming'? => {isThereGamingCategory}");

            // 3) Does the collection CONTAIN a product named 'Chair'?
            bool containsChairProduct = productList.Any(prod => prod.Name == "Chair");
            Console.WriteLine($"3) Contains product named 'Chair'? => {containsChairProduct}");

            // 4) Are ALL Electronics products priced above 500?
            bool areAllElectronicsAbove500 = productList
                .Where(prod => prod.Category == "Electronics")
                .All(prod => prod.Price > 500);

            Console.WriteLine($"4) All Electronics priced > 500? => {areAllElectronicsAbove500}");

            // 5) Is there ANY product cheaper than 200?
            bool isThereCheapProduct = productList.Any(prod => prod.Price < 200);
            Console.WriteLine($"5) Any product cheaper than 200? => {isThereCheapProduct}");
        }
    }
}


//? ======================================================================================================================


// using System;
// using System.Collections.Generic;
// using System.Linq;

namespace LINQ_Task
{
    internal class Program3
    {
        static void Main(string[] args)
        {
            var productList = new List<Product>
            {
                new(1, "Laptop", 1200m, "Electronics"),
                new(2, "Phone", 800m, "Electronics"),
                new(3, "Desk", 350m, "Furniture"),
                new(4, "Chair", 150m, "Furniture"),
                new(5, "Headphones", 200m, "Electronics"),
            };

            // 1) Convert to Array
            var productsAsArray = productList.ToArray();

            // 2) Convert to Dictionary keyed by Id
            var productsByIdDictionary = productList.ToDictionary(prod => prod.Id);

            // 3) Convert to HashSet of product Names
            var productNamesSet = productList
                .Select(prod => prod.Name)
                .ToHashSet();

            // 4) Convert to Lookup keyed by Category
            var productsByCategoryLookup = productList.ToLookup(prod => prod.Category);

            // → lookup["Electronics"] should return all electronics products
            var electronicsCategoryProducts = productsByCategoryLookup["Electronics"];

            foreach (var item in electronicsCategoryProducts)
            {
                Console.WriteLine(item.Name);
            }
        }
    }

    public record Product(int Id, string Name, decimal Price, string Category);
    //? هنا استخدمت ريكورد عشان اختصر على نفسي بدل ما اكتب الكلاس تاني كلها من اول فكده اختصرتها في سطر (من المعلومات الجديدة اللي عرفتها وفرقت معايا جامد)
}


//? ======================================================================================================================


// using System;
// using System.Collections.Generic;
// using System.Linq;

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


//? ======================================================================================================================


// using System;
// using System.Collections.Generic;
// using System.Linq;

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


//? ======================================================================================================================


// using System;
// using System.Collections.Generic;
// using System.Linq;

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


//? ======================================================================================================================


// using System;
// using System.Collections.Generic;
// using System.Linq;

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

//? ======================================================================================================================


// using System;
// using System.Collections.Generic;
// using System.Linq;

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

//? ======================================================================================================================


// using System;
// using System.Collections.Generic;
// using System.Linq;

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


//? ======================================================================================================================


// using System;
// using System.Collections.Generic;
// using System.Linq;

namespace LINQ_Task
{
    record Course(string Title, List<string> Students);

    internal class Program10
    {
        static void Main(string[] args)
        {
            List<Course> courseList = new List<Course>
            {
                new("C# Basics", new List<string> { "Ali", "Sara", "Omar" }),
                new("LINQ Mastery", new List<string> { "Sara", "Mona", "Ali" }),
                new("ASP.NET Core", new List<string> { "Yara", "Omar", "Karim" }),
            };

            //! 1) Flatten to a single list of all student names (with duplicates)
            var allStudentNames = courseList
                .SelectMany(c => c.Students)
                .ToList();

            Console.WriteLine("1) All enrollments (with duplicates):");
            Console.WriteLine(string.Join(", ", allStudentNames));

            //! 2) Distinct list of all student names
            var distinctStudentNames = courseList
                .SelectMany(c => c.Students)
                .Distinct()
                .ToList();

            Console.WriteLine("\n2) Distinct students:");
            Console.WriteLine(string.Join(", ", distinctStudentNames));

            //! 3) Students who appear in more than one course
            var studentsMultipleCourses = courseList
                .SelectMany(c => c.Students)
                .GroupBy(name => name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            Console.WriteLine("\n3) Students in more than one course:");
            Console.WriteLine(string.Join(", ", studentsMultipleCourses));

            //! 4) SelectMany with result selector to get (CourseName , StudentName) pairs
            var courseStudentPairs = courseList
                .SelectMany(
                    course => course.Students,
                    (course, student) => new { CourseName = course.Title, StudentName = student }
                )
                .ToList();

            Console.WriteLine("\n4) Course-Student pairs:");
            foreach (var pair in courseStudentPairs)
            {
                Console.WriteLine($"({pair.CourseName} , {pair.StudentName})");
            }

            /* 
            ! ( اني اجيب اسم كل طالب و جنبه عدد كل الكورسات اللي مشترك فيها ) نقطة زيادة ناخد عليها بونص دي 
            ? خليت كلاود يساعدني فيها الصراحة لان الفكرة جتلي 
            ? و حاولت فيها مكنتش راضية تظبط بالظبط يعني يمكن اكون عملت منها 60 او 70 في الميه منها 
            ? بس تظبيطها ككل كده الشكل النهائي مكنش راضي يتكتب ككود هو كان في دماغي كـ لوجيك كله تمام 
            ?  بس بقا هو كده الكود مبيجيش عافية وبدراعك يعني فمقدرتش معملهاش فخليته يكملهالي
            */
            var studentCourseCounts = courseList
                .SelectMany(c => c.Students)
                .GroupBy(name => name)
                .Select(g => new
                {
                    StudentName = g.Key,
                    CourseCount = g.Count()
                })
                .OrderByDescending(x => x.CourseCount)
                .ThenBy(x => x.StudentName)
                .ToList();

            Console.WriteLine("\nBONUS) Student course counts:");
            foreach (var student in studentCourseCounts)
            {
                Console.WriteLine($"{student.StudentName} -> {student.CourseCount}");
            }
        }
    }
}


//? ======================================================================================================================


// using System;
// using System.Collections.Generic;
// using System.Linq;

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
