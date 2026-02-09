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

//? اللي حصل ان الكلاس اتغير اسمه بعد الفانكشن لانه ريفرانس تايب بالتالي التعديل اللي بيحصل بيتم على الاوبجكت الاساسي

//?  متغيرش لانه فاليو تايب فبتالي الدلة بتشتغل على نسخة منه ref والستراكت من غير الــ 

//? اتغير لأنه اتعدل على النسخة الأصلية  ref على عكس الستراكت مع الــ

