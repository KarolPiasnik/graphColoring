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
        int[] colors;
        Tuple<List<int>, int>[] adjacencyList;

        public Graph(int numberOfVertices)
        {
            this.numberOfVertices = numberOfVertices;
            colors = new int[numberOfVertices];
            adjacency = new bool[numberOfVertices, numberOfVertices];
            for (int i = 0; i < numberOfVertices; ++i)
                for (int j = 0; j < numberOfVertices; ++j)
                    adjacency[i, j] = false;
            adjacencyList = new Tuple< List<int>, int>[numberOfVertices];
            for (int i = 0; i < numberOfVertices; ++i)
                adjacencyList[i] = new Tuple<List<int>, int>(new List<int>(), i);
        }

        public Graph(string path)
        {

            string[] lines = System.IO.File.ReadAllLines(path);
            numberOfVertices = int.Parse(lines[0]);

            if (lines.Length == numberOfVertices + 1)
            {
                colors = new int[numberOfVertices];
                adjacencyList = new Tuple<List<int>, int>[numberOfVertices];
                for (int i = 0; i < numberOfVertices; ++i)
                    adjacencyList[i] = new Tuple<List<int>, int>(new List<int>(), i);
                adjacency = new bool[numberOfVertices, numberOfVertices];
                for (int i = 0; i < numberOfVertices; ++i)
                    for (int j = 0; j < numberOfVertices; ++j)
                        adjacency[i, j] = false;

                for (int i = 1; i < lines.Length; ++i)
                {
                    int counter = 0;
                    for (int j = 0; j < lines[i].Length; ++j)
                    {
                        if (lines[i][j] == '1')
                        {
                            adjacency[i - 1, counter] = true;
                            adjacencyList[i - 1].Item1.Add(counter);
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
            for (int i = 0; i < numberOfVertices; ++i)
            {
                foreach (int j in adjacencyList[i].Item1)
                {
                    Console.Write(j + " ");
                }
                Console.WriteLine("");
            }
        }

        public int searchForVertex(int orginalNumber)
        {
            for (int i = 0; i < numberOfVertices; ++i)
            {
                if (adjacencyList[i].Item2 == orginalNumber)
                    return i;
            }
            return -1;
        }

        public void addEdge(int v1, int v2)
        {
            adjacency[v1, v2] = true;
            adjacency[v2, v1] = true;
            adjacencyList[v1].Item1.Add(v2);
            adjacencyList[v2].Item1.Add(v1);

        }

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
            

            // Initialize remaining V-1 vertices as unassigned
            for (int u = 0; u < numberOfVertices; u++)
                result[u] = -1;  // no color is assigned to u

            // A temporary array to store the available color. True
            // value of available[color] would mean that the color is
            // assigned to one of its adjacent vertices
            bool[] unAvailable = new bool[numberOfVertices];
            for (int color = 0; color < numberOfVertices; ++color)
                unAvailable[color] = false;
            // Assign color to remaining V-1 vertices
            for (int u = 0; u < numberOfVertices; u++)
            {
                // Process all adjacent vertices and flag their color
                // as unavailable
                
                foreach (int i in adjacencyList[u].Item1)
                    if (result[i] != -1)
                        unAvailable[result[i]] = true;

                // Find the first available color
                int color;
                for (color = 0; color < numberOfVertices; color++)
                    if (unAvailable[color] == false)
                        break;

                result[adjacencyList[u].Item2] = color; // Assign the found color

                // Reset the values back to false for the next iteration
                foreach (int i in adjacencyList[u].Item1)
                    if (result[i] != -1)
                        unAvailable[result[i]] = false;
            }

            // print the result
            for (int u = 0; u < numberOfVertices; u++)
            {
                Console.WriteLine("Vertex " + u + "---> Color" + result[u]);
            }
            colors = result;
           
        }

        public void checkColoring()
        {

            for (int i = 0; i < numberOfVertices; ++i)
            {
                foreach (int j in adjacencyList[i].Item1)
                    if (colors[i] == colors[j] && j != i)
                        Console.WriteLine("Nie dziala");
            }
        }

        public void RSColoring()
        {

            Tuple<List<int>, int>[] tmp = adjacencyList;
            Random rnd = new Random();
            Tuple<List<int>, int>[] sortedAdjacencyList = adjacencyList.OrderBy(s => rnd.Next()).ToArray();
            adjacencyList = sortedAdjacencyList;
            this.greedyColoring2();
            adjacencyList = tmp;
        }

        public void LFColoring()
        {
           
            Tuple<List<int>, int>[] tmp = adjacencyList;
            Tuple<List<int>, int>[] sortedAdjacencyList = adjacencyList.OrderBy(s => -s.Item1.Count).ToArray();
            adjacencyList = sortedAdjacencyList;
            this.greedyColoring2();
            adjacencyList = tmp;
        }

        public void SLColoring() //TODO
        {

            Tuple<List<int>, int>[] tmp = adjacencyList;
            Tuple<List<int>, int>[] tmp2 = adjacencyList;
            for (int j = 0; j < numberOfVertices; ++j)
            {
                int smallest =1000000;
                int index =1000000;
                for (int i = 1; i < numberOfVertices; ++i)
                {
                    if(adjacencyList[i].Item1.Count < smallest && adjacencyList[i].Item1[0] != -10)
                    {
                        smallest = adjacencyList[i].Item1.Count;
                        index = i;
                    }
                }
                tmp2[j] = adjacencyList[index];
                adjacencyList[j].Item1[0] = -10;
            }

            adjacencyList = tmp2;
            this.greedyColoring2();
            adjacencyList = tmp;
        }
    }
}
