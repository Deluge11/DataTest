using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataTest
{
    public class Graph
    {

        List<GraphNode> graph = new List<GraphNode>();

        public void Add(GraphNode node)
        {
            graph.Add(node);
        }

        public void BFS(GraphNode startNode)
        {
            bool[] Visiting_State = new bool[graph.Count];

            var path = Enumerable.Repeat(double.PositiveInfinity, graph.Count).ToArray();

            Queue<GraphNode> queue = new Queue<GraphNode>();

            int CurrentIndex = graph.IndexOf(startNode);
            int NextIndex;

            queue.Enqueue(graph[CurrentIndex]);
            Visiting_State[CurrentIndex] = true;
            path[CurrentIndex] = 0;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                CurrentIndex = graph.IndexOf(current);

                foreach (var next in current.Connected)
                {
                    NextIndex = graph.IndexOf(next);

                    if (!Visiting_State[NextIndex])
                    {
                        queue.Enqueue(next);
                        path[NextIndex] = path[CurrentIndex] + 1;
                        Visiting_State[NextIndex] = true;
                    }

                }
            }

            for (int i = 0; i < graph.Count; i++)
            {
                Console.WriteLine($"The Path From {startNode.Value} To {graph[i].Value} : {path[i]}");
            }


        }
        public void DFS(GraphNode startNode)
        {

        }
    }
    public class GraphNode
    {
        public char Value { get; set; }

        public LinkedList<GraphNode> Connected = new LinkedList<GraphNode>();
        public GraphNode(char _value)
        {
            Value = _value;
        }

        public void Con(params GraphNode[] nodes)
        {
            foreach (var node in nodes)
            {
                Connected.AddLast(node);
            }
        }
    }

}
