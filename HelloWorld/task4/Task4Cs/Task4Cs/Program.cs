// See https://aka.ms/new-console-template for more information
using System;
using Task4Cs;

public class Program
{
    static void Main(string[] args)
    {
        Console.Write("N = ");
        var len = Convert.ToInt32(Console.ReadLine());
        var a = new int[len];
        for (var i = 0; i < a.Length; ++i)
        {
            Console.Write("a[{0}] = ", i);
            a[i] = Convert.ToInt32(Console.ReadLine());
        }

        Console.WriteLine("Упорядоченный массив: {0}", string.Join(", ", MergeSort.Run(a)));

        Console.ReadLine();
    }
}