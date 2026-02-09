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

        if (LoginEnabled && string.Compare(AppVersion, LoginMinVersion) >= 0)
            Console.WriteLine("Login Feature: Enabled");
        else
            Console.WriteLine("Login Feature: Disabled");


        Console.WriteLine("Export Feature: " +
            (ExportEnabled && string.Compare(AppVersion, ExportMinVersion) >= 0 ? "Enabled" : "Disabled"));

        if (AdminPanelEnabled)
            Console.WriteLine("AdminPanel Feature: " +
                (string.Compare(AppVersion, AdminPanelMinVersion) >= 0 ? "Enabled" : "Disabled"));
        else
            Console.WriteLine("AdminPanel Feature: Disabled");

        Console.ReadLine();
    }
}