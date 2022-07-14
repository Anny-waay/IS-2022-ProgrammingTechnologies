package com.graph;

import java.io.*;
import java.util.*;

public class Graph {
    private int _V;
    private LinkedList<Integer> _adj[];

    public Graph(int v) {
        _V = v;
        _adj = new LinkedList[v];
        for (int i = 0; i < v; ++i)
            _adj[i] = new LinkedList();
    }

    public void addEdge(int v, int w) {
        _adj[v].add(w);
    }

    public void BFS(int s) {
        boolean visited[] = new boolean[_V];

        LinkedList<Integer> queue = new LinkedList<Integer>();

        visited[s] = true;
        queue.add(s);

        while (queue.size() != 0) {
            s = queue.poll();
            System.out.print(s + " ");

            Iterator<Integer> i = _adj[s].listIterator();
            while (i.hasNext()) {
                int n = i.next();
                if (!visited[n]) {
                    visited[n] = true;
                    queue.add(n);
                }
            }
        }
    }

    private void DFSUtil(int v, boolean visited[]) {
        visited[v] = true;
        System.out.print(v + " ");

        Iterator<Integer> i = _adj[v].listIterator();
        while (i.hasNext()) {
            int n = i.next();
            if (!visited[n])
                DFSUtil(n, visited);
        }
    }

    public void DFS(int v) {
        boolean visited[] = new boolean[_V];
        DFSUtil(v, visited);
    }
}
