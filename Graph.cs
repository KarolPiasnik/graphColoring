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

            colors = new int[numberOfVertices];
            // Assign the first color to first vertex
            

            // Initialize remaining V-1 vertices as unassigned
            for (int u = 0; u < numberOfVertices; u++)
                colors[u] = -1;  // no color is assigned to u

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
                    if (colors[i] != -1)
                        unAvailable[colors[i]] = true;

                // Find the first available color
                int color;
                for (color = 0; color < numberOfVertices; color++)
                    if (unAvailable[color] == false)
                        break;

                colors[adjacencyList[u].Item2] = color; // Assign the found color

                // Reset the values back to false for the next iteration
                foreach (int i in adjacencyList[u].Item1)
                    if (colors[i] != -1)
                        unAvailable[colors[i]] = false;
            }

            // print the result
            for (int u = 0; u < numberOfVertices; u++)
            {
                Console.WriteLine("Vertex " + u + "---> Color" + colors[u]);
            }
           
        }

        public void interchangeColoring()
        {
            colors = new int[numberOfVertices];
            for (int u = 0; u < numberOfVertices; u++)
                colors[u] = -1;
            colors[0] = 0;
            bool[] unAvailable = new bool[numberOfVertices];
            for (int color = 0; color < numberOfVertices; ++color)
                unAvailable[color] = false;
            int highestColorNumber = 0;
            for (int u = 0; u < numberOfVertices; u++)
            {
                foreach (int i in adjacencyList[u].Item1)
                    if (colors[i] != -1)
                        unAvailable[colors[i]] = true;

                int color;
                for (color = 0; color < numberOfVertices; color++)
                    if (unAvailable[color] == false)
                    {
                        break;
                    }


                if (color > highestColorNumber) // only difference to greedy coloring
                {

                    bool[] tmpAvailable = new bool[highestColorNumber + 2];

                    foreach (int vertex in adjacencyList[u].Item1)
                    {
                        for (int n = 0; n < highestColorNumber + 2; ++n)
                            tmpAvailable[n] = true;

                        foreach (int vertex2 in adjacencyList[searchForVertex(vertex)].Item1)
                        {
                            if (colors[searchForVertex(vertex2)] != -1)
                                tmpAvailable[colors[searchForVertex(vertex2)]] = false;
                        }

                        for (int n = 0; n <= highestColorNumber + 1; ++n)
                            if (tmpAvailable[n] && n < colors[searchForVertex(vertex)] || colors[searchForVertex(vertex)] == -1)
                            {
                                colors[searchForVertex(vertex)] = n;
                                break;
                            }
                    }

                    for (int n = 0; n < highestColorNumber + 2; ++n)
                        tmpAvailable[n] = true;

                    foreach (int vertex in adjacencyList[u].Item1)
                    {
                        if (colors[searchForVertex(vertex)] != -1)
                            tmpAvailable[colors[searchForVertex(vertex)]] = false;
                    }
                    for (int n = 0; n < highestColorNumber + 2; ++n)
                        if (tmpAvailable[n] && n < highestColorNumber + 1)
                        {
                            colors[u] = n;
                            if (n > highestColorNumber)
                                highestColorNumber = n;
                            break;
                        }
                }
                else
                {
                    colors[adjacencyList[u].Item2] = color; // Assign the found color
                }
                // Reset the values back to false for the next iteration
                foreach (int i in adjacencyList[u].Item1)
                    if (colors[i] != -1)
                        unAvailable[colors[i]] = false;
            }

            // print the result
            for (int u = 0; u < numberOfVertices; u++)
            {
                Console.WriteLine("Vertex " + u + "---> Color" + colors[u]);
            }

        }

        public bool checkColoring()
        {

            for (int i = 0; i < numberOfVertices; ++i)
            {
                foreach (int j in adjacencyList[i].Item1)
                    if (colors[i] == colors[j] && j != i)
                        return false;
            }
            return true;

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

        public void SFColoring() 
        {

            Tuple<List<int>, int>[] tmp = adjacencyList;
            Tuple<List<int>, int>[] tmp2 = new Tuple<List<int>, int>[numberOfVertices];
            bool[] available = new bool[numberOfVertices];
            for (int j = 0; j < numberOfVertices; ++j)
            {
                available[j] = true;
            }

            for (int j = 0; j < numberOfVertices; ++j)
            {
                int smallest =1000000;
                int index =1000000;
                for (int i = 0; i < numberOfVertices; ++i)
                {
                    if(adjacencyList[i].Item1.Count < smallest && available[i] )
                    {
                        smallest = adjacencyList[i].Item1.Count;
                        index = i;
                    }
                }
                tmp2[j] = new Tuple<List<int>, int>( adjacencyList[index].Item1, adjacencyList[index].Item2);
                available[index] = false;
            }
            adjacencyList = tmp2;
            this.greedyColoring2();
            adjacencyList = tmp;
        }

        public void SLColoring() 
        {

            Tuple<List<int>, int>[] tmp = adjacencyList;
            Tuple<List<int>, int>[] tmp2 = new Tuple<List<int>, int>[numberOfVertices];
            bool[] available = new bool[numberOfVertices];
            for (int j = 0; j < numberOfVertices; ++j)
            {
                available[j] = true;
            }

            for (int j = 0; j < numberOfVertices; ++j)
            {
                int smallest = 1000000;
                int index = 1000000;
                for (int i = 0; i < numberOfVertices; ++i)
                {
                    if (adjacencyList[i].Item1.Count < smallest && available[i])
                    {
                        smallest = adjacencyList[i].Item1.Count;
                        index = i;
                    }
                }
                tmp2[j] = new Tuple<List<int>, int>(adjacencyList[index].Item1, adjacencyList[index].Item2);
                available[index] = false;
            }
            adjacencyList = tmp2;
            Array.Reverse(adjacencyList);

            this.greedyColoring2();
            adjacencyList = tmp;
        }
    }
}
