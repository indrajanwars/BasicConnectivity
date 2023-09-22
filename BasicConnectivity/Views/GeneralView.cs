﻿namespace BasicConnectivity.Views;

public class GeneralView
{
    public static void List<T>(List<T> items, string title)
    {
        Console.WriteLine($"\nList of {title}");
        Console.WriteLine("---------------");
        foreach (var item in items)
        {
            Console.WriteLine(item.ToString());
        }
    }

    public static void Single<T>(T item, string title)
    {
        Console.WriteLine($"\nList of {title}");
        Console.WriteLine("---------------");
        Console.WriteLine(item.ToString());
    }

    public static void Transaction(string result)
    {
        int.TryParse(result, out int res);
        if (res > 0)
        {
            Console.WriteLine("Transaction completed successfully");
        }
        else
        {
            Console.WriteLine("Transaction failed");
            Console.WriteLine(result);
        }
    }
}