package Main;

import com.graph.Graph;

public class TestJava {
    public static void main(String[] args) {
        Graph g = new Graph(4);

        g.addEdge(0, 1);
        g.addEdge(0, 2);
        g.addEdge(1, 2);
        g.addEdge(2, 0);
        g.addEdge(2, 3);
        g.addEdge(3, 3);


        g.BFS(2);
        System.out.println("");
        g.DFS(2);
    }
}
