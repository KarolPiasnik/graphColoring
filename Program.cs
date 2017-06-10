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
            g1.addEdge(1, 2);
            g1.addEdge(1, 3);
            g1.addEdge(2, 3);
            g1.addEdge(3, 4);
            g1.greedyColoring();
            Graph g2 = new Graph(@"C:\Users\Karol\Desktop\cpp kolo\6\ConsoleApplication1\ConsoleApplication1\graf.txt");
            g2.greedyColoring();


        }
    }
}
