﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    class Graph
    {
        int numberOfVertices;
        bool[,] adjacency;
        List<int>[] adjacencyList;

        public Graph(int numberOfVertices)
        {
            this.numberOfVertices = numberOfVertices;
            adjacency = new bool[numberOfVertices, numberOfVertices];
            for (int i = 0; i < numberOfVertices; ++i)
                for (int j = 0; j < numberOfVertices; ++j)
                    adjacency[i, j] = false;
            adjacencyList = new List<int>[numberOfVertices];
            for (int i = 0; i < numberOfVertices; ++i)
                adjacencyList[i] = new List<int>();
        }

        public Graph(string path)
        {
            Console.WriteLine(path);

            string[] lines = System.IO.File.ReadAllLines(path);
            Console.WriteLine(lines.Length);
            numberOfVertices = int.Parse(lines[0]);

            if (lines.Length == numberOfVertices + 1)
            {
                adjacencyList = new List<int>[numberOfVertices];
                for (int i = 0; i < numberOfVertices; ++i)
                    adjacencyList[i] = new List<int>();
                adjacency = new bool[numberOfVertices, numberOfVertices];
                for (int i = 0; i < numberOfVertices; ++i)
                    for (int j = 0; j < numberOfVertices; ++j)
                        adjacency[i, j] = false;

                for (int i = 1; i < lines.Length; ++i)
                {
                    for (int j = 0; j < lines[i].Length; ++j)
                    {
                        int counter = 0;
                        if (lines[i][j] == '1')
                        {
                            adjacency[i - 1, counter] = true;
                            adjacencyList[i - 1].Add(counter);
                            ++counter;
                        }
                        else if (lines[i][j] == '0')
                        {
                            adjacency[i - 1, counter] = false;

                            ++counter;
                        }
                        
                    }
                }
            }
            else
            {
                Console.WriteLine("xxx");
            }
        }

        public void printAdjacencyList()
        {
            foreach (List<int> list in adjacencyList)
            {
                foreach (int i in list)
                {
                    Console.Write(i + " ");
                }
                Console.WriteLine("");
            }
        }

        //public Graph(string path)
        //{
        //    Console.WriteLine(path);

        //    string[] lines = System.IO.File.ReadAllLines(path);
        //    Console.WriteLine(lines.Length);
        //    numberOfVertices = int.Parse(lines[0]);

        //    if (lines.Length == numberOfVertices + 1)
        //    {
        //        adjacency = new bool[numberOfVertices, numberOfVertices];
        //        for (int i = 0; i < numberOfVertices; ++i)
        //            for (int j = 0; j < numberOfVertices; ++j)
        //                adjacency[i, j] = false;

        //        for (int i = 1; i < lines.Length; ++i)
        //        {
        //            Console.WriteLine(" ");
        //            for (int j = 0; j < lines[i].Length; ++j)
        //            {
        //                int counter = 0;
        //                if (lines[i][j] == '1')
        //                {
        //                    adjacency[i - 1, counter] = true;
        //                    Console.Write("1");
        //                    ++counter;
        //                }
        //                else if (lines[i][j] == '0')
        //                {
        //                    adjacency[i - 1, counter] = false;
        //                    Console.Write("0");

        //                    ++counter;
        //                }
        //                else
        //                {
        //                    Console.Write(" ");
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("xxx");
        //    }
        //}

        public void addEdge(int v1, int v2)
        {
            adjacency[v1, v2] = true;
            adjacency[v2, v1] = true;
            adjacencyList[v1].Add(v2);
            adjacencyList[v2].Add(v1);

        }

        // Prints greedy coloring of the vertices
        public void greedyColoring()
        {
            int[] result = new int[numberOfVertices];
            // Assign the first color to first vertex
            result[0] = 0;

            // Initialize remaining V-1 vertices as unassigned
            for (int u = 1; u < numberOfVertices; u++)
                result[u] = -1;  // no color is assigned to u

            // A temporary array to store the available color. True
            // value of available[color] would mean that the color color is
            // assigned to one of its adjacent vertices
            bool[] available = new bool[numberOfVertices];
            for (int color = 0; color < numberOfVertices; ++color)
                available[color] = false;
            // Assign color to remaining V-1 vertices
            for (int u = 1; u < numberOfVertices; u++)
            {
                // Process all adjacent vertices and flag their color
                // as unavailable
                for (int i = 0; i < numberOfVertices; ++i)
                    if (adjacency[u, i] == true)
                        if (result[i] != -1)
                        available[result[i]] = true;

                // Find the first available color
                int cr;
                for (cr = 0; cr < numberOfVertices; cr++)
                    if (available[cr] == false)
                        break;

                result[u] = cr; // Assign the found color

                // Reset the values back to false for the next iteration
                for (int i = 0; i < numberOfVertices; ++i)
                    if(adjacency[u,i] == true)
                        if (result[i] != -1)
                            available[result[i]] = false;
            }

            // print the result
            for (int u = 0; u < numberOfVertices; u++)
                Console.WriteLine("Vertex " + u + "---> Color" + result[u] ); 
        }

        public void greedyColoring2()
        {
            int[] result = new int[numberOfVertices];
            // Assign the first color to first vertex
            result[0] = 0;

            // Initialize remaining V-1 vertices as unassigned
            for (int u = 1; u < numberOfVertices; u++)
                result[u] = -1;  // no color is assigned to u

            // A temporary array to store the available color. True
            // value of available[color] would mean that the color color is
            // assigned to one of its adjacent vertices
            bool[] available = new bool[numberOfVertices];
            for (int color = 0; color < numberOfVertices; ++color)
                available[color] = false;
            // Assign color to remaining V-1 vertices
            for (int u = 1; u < numberOfVertices; u++)
            {
                // Process all adjacent vertices and flag their color
                // as unavailable
                
                foreach (int i in adjacencyList[u])
                    if (result[i] != -1)
                        available[result[i]] = true;

                // Find the first available color
                int cr;
                for (cr = 0; cr < numberOfVertices; cr++)
                    if (available[cr] == false)
                        break;

                result[u] = cr; // Assign the found color

                // Reset the values back to false for the next iteration
                foreach (int i in adjacencyList[u])
                    if (result[i] != -1)
                        available[result[i]] = false;
            }

            // print the result
            for (int u = 0; u < numberOfVertices; u++)
                Console.WriteLine("Vertex " + u + "---> Color" + result[u]);
        }
    }
}
