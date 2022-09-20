using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    private int[][] graph;

    public Graph(int number_of_points)
    {
        graph = new int[number_of_points][];
        for (int i = 0; i < number_of_points; i++)
        {
            graph[i] = new int[number_of_points];
        }

        for (int i = 0; i < number_of_points; i++)
        {
            for (int j = 0; j < number_of_points; j++)
            {
                graph[i][j] = 0;
            }
        }
    }

    public void addEdge(int index_1, int index_2)
    {
        graph[index_1][index_2] = 1;
    }
}
