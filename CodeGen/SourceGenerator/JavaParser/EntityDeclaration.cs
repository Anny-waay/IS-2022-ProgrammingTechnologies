using System;
using System.Collections.Generic;

namespace JavaParser
{
    public class EntityDeclaration
    {
        public string EntityName { get; set; }
        public List<ArgDeclaration> ArgList { get; set; } = new List<ArgDeclaration>();
        
        public void Print()
        {
            Console.WriteLine($"EntityName {EntityName}");
            Console.WriteLine("Args: ");
            foreach (var arg in ArgList)
            {
                Console.WriteLine($"{arg.Type} {arg.Name}");
            }
        }
    }
}