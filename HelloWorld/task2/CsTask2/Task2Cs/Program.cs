using System;
using FSharpLibrary;

namespace Task2Cs
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(pipe.double_square(5));
            Console.WriteLine(Area.area(Polygon.NewRectangle(5, 4)));
        }
    }
}