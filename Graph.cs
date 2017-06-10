using System;
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

        public Graph(int numberOfVertices)
        {
            this.numberOfVertices = numberOfVertices;
            adjacency = new bool[numberOfVertices, numberOfVertices];
            for (int i = 0; i < numberOfVertices; ++i)
                for (int j = 0; j < numberOfVertices; ++j)
                    adjacency[i, j] = false;
        }

        public Graph(string path)
        {
            string[] lines = System.IO.File.ReadAllLines(path);

            if (lines.Length == numberOfVertices + 1)
            {
                numberOfVertices = int.Parse(lines[0]);
                adjacency = new bool[numberOfVertices, numberOfVertices];
                for (int i = 0; i < numberOfVertices; ++i)
                    for (int j = 0; j < numberOfVertices; ++j)
                        adjacency[i, j] = false;

                for (int i = 0; i < numberOfVertices; ++i)
                {
                    for (int j = 0; j < lines[i + 1].Length; ++j)
                    {
                        int counter = 0;
                        if (lines[i][j] == '1')
                        {
                            adjacency[i, counter] = true;
                            ++counter;
                        }
                        else if (lines[i][j] == '0')
                        {
                            adjacency[i, counter] = false;
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

        public void addEdge(int v1, int v2)
        {
            adjacency[v1, v2] = true;
            adjacency[v2, v1] = true;
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
    }
}
