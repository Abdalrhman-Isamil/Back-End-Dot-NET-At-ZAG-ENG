using System;
using System.Collections.Generic;
using System.Linq;

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
