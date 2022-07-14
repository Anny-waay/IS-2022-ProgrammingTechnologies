﻿using System;
using GraphBfsDfs;

namespace TestTask3Cs
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Graph g = new Graph(4);
     
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(1, 2);
            g.AddEdge(2, 0);
            g.AddEdge(2, 3);
            g.AddEdge(3, 3);
     
            g.BFS(2);
            Console.WriteLine("");
            g.DFS(2);
        }
    }
}