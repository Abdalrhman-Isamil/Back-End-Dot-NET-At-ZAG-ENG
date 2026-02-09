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
