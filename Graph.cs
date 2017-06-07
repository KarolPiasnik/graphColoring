using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Graph
    {
        int numberOfVertices;
        bool[,] adjacency;

        Graph(int numberOfVertices)
        {
            this.numberOfVertices = numberOfVertices;
            adjacency = new bool[numberOfVertices, numberOfVertices];
        }

    }
}
