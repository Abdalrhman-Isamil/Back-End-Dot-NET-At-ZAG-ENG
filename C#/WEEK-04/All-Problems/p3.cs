using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int> { 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        List<int> evens = new List<int>();
        List<int> odds = new List<int>();
        List<int> primes = new List<int>();


        foreach (int num in numbers)
        {
            if (IsEven(num))
                evens.Add(num);
            else
                odds.Add(num);

            if (IsPrime(num))
                primes.Add(num);
        }


        Console.WriteLine("Even Numbers: " + string.Join(", ", evens));
        Console.WriteLine("Odd Numbers: " + string.Join(", ", odds));
        Console.WriteLine("Prime Numbers: " + string.Join(", ", primes));

        Console.ReadLine();
    }


    static bool IsEven(int n)
    {
        return n % 2 == 0;
    }


    static bool IsPrime(int n)
    {
        if (n < 2) return false;

        for (int i = 2; i <= Math.Sqrt(n); i++)
        {
            if (n % i == 0)
                return false;
        }
        return true;
    }
}
