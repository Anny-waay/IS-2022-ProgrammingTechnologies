using System;
using System.Collections.Generic;

namespace JavaParser
{
    public class MethodDeclaration
    {
        public string MethodName { get; set; }
        public string ReturnType { get; set; }
        public List<ArgDeclaration> ArgList { get; set; } = new List<ArgDeclaration>();
        public string Url { get; set; }
        public string HttpMethodName { get; set; }

        public void Print()
        {
            Console.WriteLine($"MethodName {MethodName}");
            Console.WriteLine($"ReturnType {ReturnType}");
            Console.WriteLine("Args: ");
            foreach (var arg in ArgList)
            {
                Console.WriteLine($"{arg.Type} {arg.Name}");
            }

            Console.WriteLine($"Url {Url}");
            Console.WriteLine($"HttpMethodName {HttpMethodName}");
        }
    }
}