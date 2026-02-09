//Runtime Environment Analyzer

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



// ==================================================


using System;

class Program
{

    const string AppVersion = "1.5";


    static readonly bool LoginEnabled = true;
    static readonly bool ExportEnabled = false;
    static readonly bool AdminPanelEnabled = true;


    const string LoginMinVersion = "1.0";
    const string ExportMinVersion = "2.0";
    const string AdminPanelMinVersion = "1.5";

    static void Main()
    {
        Console.WriteLine("App Version: " + AppVersion);

        // Login
        if (LoginEnabled && string.Compare(AppVersion, LoginMinVersion) >= 0)
            Console.WriteLine("Login Feature: Enabled");
        else
            Console.WriteLine("Login Feature: Disabled");

        // Export
        Console.WriteLine("Export Feature: " +
            (ExportEnabled && string.Compare(AppVersion, ExportMinVersion) >= 0 ? "Enabled" : "Disabled"));

        // AdminPanel
        if (AdminPanelEnabled)
            Console.WriteLine("AdminPanel Feature: " +
                (string.Compare(AppVersion, AdminPanelMinVersion) >= 0 ? "Enabled" : "Disabled"));
        else
            Console.WriteLine("AdminPanel Feature: Disabled");

        Console.ReadLine();
    }
}


// ==================================================


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


// ==================================================


using System;

class Program
{
    static void Main()
    {

        User user = new User();
        user.Name = "Ahmed";

        Console.WriteLine("Before ModifyClass: " + user.Name);
        ModifyClass(user); // call normally
        Console.WriteLine("After ModifyClass: " + user.Name);


        UserSnapshot snapshot = new UserSnapshot();
        snapshot.Name = "Mohamed";

        Console.WriteLine("\nBefore ModifyStruct: " + snapshot.Name);
        ModifyStruct(snapshot); // call normally
        Console.WriteLine("After ModifyStruct: " + snapshot.Name);


        ModifyStructRef(ref snapshot);
        Console.WriteLine("After ModifyStruct with ref: " + snapshot.Name);

        Console.ReadLine();
    }


    static void ModifyClass(User u)
    {
        u.Name = "Ali";
    }


    static void ModifyStruct(UserSnapshot s)
    {
        s.Name = "Khaled";
    }


    static void ModifyStructRef(ref UserSnapshot s)
    {
        s.Name = "Youssef";
    }
}


class User
{
    public string Name;
}


struct UserSnapshot
{
    public string Name;
}

// اللي حصل ان الكلاس اتغير اسمه بعد الفانكشن لانه ريفرانس تايب بالتالي التعديل اللي بيحصل بيتم على الاوبجكت الاساسي

//  متغيرش لانه فاليو تايب فبتالي الدلة بتشتغل على نسخة منه ref والستراكت من غير الــ 

//  اتغير لأنه اتعدل على النسخة الأصلية  ref على عكس الستراكت مع الــ



// ==================================================


using System;

class Program
{
    static void Main()
    {
        decimal balance = 100;

        try
        {
            ProcessPayment(150);
        }
        catch (InsufficientBalanceException ex)
        {
            Console.WriteLine("Caught: " + ex.Message);
        }
        catch (PaymentTimeoutException ex)
        {
            Console.WriteLine("Caught: " + ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("General Exception: " + ex.Message);
        }
        finally
        {
            Console.WriteLine("Payment process finished.");
        }

        Console.ReadLine();
    }

    static void ProcessPayment(decimal amount)
    {
        decimal currentBalance = 100;

        if (amount > currentBalance)
            throw new InsufficientBalanceException("Not enough balance!");


        if (amount == 0)
            throw new PaymentTimeoutException("Payment timeout occurred!");

        Console.WriteLine("Payment of " + amount + " completed successfully.");
    }
}

class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string message) : base(message) { }
}

class PaymentTimeoutException : Exception
{
    public PaymentTimeoutException(string message) : base(message) { }
}


// ==================================================


//( Longest Common Prefix )

public class Solution
{
    public string LongestCommonPrefix(string[] words)
    {
        Array.Sort(words);
        int total = words.Length;
        string firstWord = words[0];
        string lastWord = words[total - 1];
        string prefix = "";

        for (int index = 0; index < firstWord.Length; index++)
        {
            if (firstWord[index] == lastWord[index])
            {
                prefix += firstWord[index];
            }
            else
            {
                break;
            }
        }

        return prefix;
    }
}


// ==================================================

// Contains Duplicat

public class Solution
{
    public bool ContainsDuplicate(int[] numbers)
    {
        Array.Sort(numbers);
        int length = numbers.Length;

        for (int i = 1; i < length; i++)
        {
            if (numbers[i] == numbers[i - 1])
                return true;
        }

        return false;
    }
}

// ==================================================

// Valid Anagram


using System;

public class Solution
{
    public bool IsAnagram(string str1, string str2)
    {

        if (str1.Length != str2.Length)
            return false;


        char[] arr1 = str1.ToCharArray();
        char[] arr2 = str2.ToCharArray();

        Array.Sort(arr1);
        Array.Sort(arr2);


        for (int i = 0; i < arr1.Length; i++)
        {
            if (arr1[i] != arr2[i])
                return false;
        }

        return true;
    }
}


