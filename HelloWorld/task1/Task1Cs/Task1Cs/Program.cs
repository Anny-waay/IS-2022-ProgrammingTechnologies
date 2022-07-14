using System;
using System.Runtime.InteropServices;

namespace Task1Cs
{
    internal class Program
    {
        [DllImport("/Users/annakomova/Desktop/TechProg/lab-1/sum/cmake-build-debug/libsum.dylib", CallingConvention = CallingConvention.Cdecl)]
        private static extern int sum(int a, int b);

        public static void Main(string[] args)
        {
            Console.WriteLine(sum(1,4));
        }
    }
}