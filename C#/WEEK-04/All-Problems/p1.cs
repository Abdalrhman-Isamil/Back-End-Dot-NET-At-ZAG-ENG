using System;
using System.Runtime.InteropServices;

class Program
{
    static void Main()
    {

        string runtime = RuntimeInformation.FrameworkDescription;
        string os = RuntimeInformation.OSDescription;
        string arch = RuntimeInformation.OSArchitecture.ToString();


        Console.WriteLine("Runtime: " + runtime);
        Console.WriteLine("Operating System: " + os);
        Console.WriteLine("CPU Architecture: " + arch);


        switch (runtime)
        {
            case string r when r.Contains(".NET"):
                Console.WriteLine("Modern .NET Runtime");
                break;
            default:
                Console.WriteLine("Legacy Runtime");
                break;
        }

        Console.ReadLine();
    }
}