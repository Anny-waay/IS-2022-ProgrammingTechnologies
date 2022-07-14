using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphBfsDfs
{
    public class Graph
    {
        private int _V; 
        private LinkedList<int>[] _adj;
 
        public Graph(int v)
        {
            _adj = new LinkedList<int>[v];
            for(int i = 0; i < _adj.Length; i++)
            {
                _adj[i] = new LinkedList<int>();
            }
            _V = v;
        }
 
        public void AddEdge(int v, int w)
        {
            _adj[v].AddLast(w);
        }
        
        public void BFS(int s)
        {
            bool[] visited = new bool[_V];
            for (int i = 0; i < _V; i++)
                visited[i] = false;

            LinkedList<int> queue = new LinkedList<int>();

            visited[s] = true;
            queue.AddLast(s);

            while (queue.Any())
            {
                s = queue.First();
                Console.Write(s + " ");
                queue.RemoveFirst();

                LinkedList<int> list = _adj[s];

                foreach (var val in list)
                {
                    if (!visited[val])
                    {
                        visited[val] = true;
                        queue.AddLast(val);
                    }
                }
            }
        }
        private void DFSUtil(int v, bool[] visited)
        {
            visited[v] = true;
            Console.Write(v + " ");
 
            LinkedList<int> vList = _adj[v];
            foreach(var n in vList)
            {
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }
 
        public void DFS(int v)
        {
            bool[] visited = new bool[_V];
            DFSUtil(v, visited);
        }
    }
}