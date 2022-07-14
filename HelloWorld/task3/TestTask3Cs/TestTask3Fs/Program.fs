// For more information see https://aka.ms/fsharp-console-apps
open GraphBfsDfs
let g = new Graph(4)

g.AddEdge(0, 1);
g.AddEdge(0, 2);
g.AddEdge(1, 2);
g.AddEdge(2, 0);
g.AddEdge(2, 3);
g.AddEdge(3, 3);
     
g.BFS(2);
printf("\n");
g.DFS(2);