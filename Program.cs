using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            Graph g1 = new Graph(5);

            g1.addEdge(0, 1);
            g1.addEdge(0, 2);

            g1.addEdge(0, 4);
            g1.addEdge(1, 2);
            g1.addEdge(2, 3);
            g1.addEdge(2, 4);
            g1.addEdge(3, 4);
            g1.greedyColoring2();

            Graph g2 = new Graph(5);
            g2.addEdge(0, 1);
            g2.addEdge(0, 2);
            g2.addEdge(1, 2);
            g2.addEdge(1, 4);
            g2.addEdge(2, 4);
            g2.addEdge(4, 3);
            g2.greedyColoring2();

            Graph g3 = new Graph(@"C:\Users\Karol\Desktop\cpp kolo\6\ConsoleApplication1\ConsoleApplication1\graf.txt");
            g3.greedyColoring2();

        }
    }
}
