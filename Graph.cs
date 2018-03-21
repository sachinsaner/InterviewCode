using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice
{
    public class Graph
    {
        public int V;
        public List<List<int>> AdjecencyList;
        HashSet<int> visitiedList;
        Stack<int> stack;

        public Graph(int v)
        {
            this.V = v;
            this.AdjecencyList = new List<List<int>>();
            this.visitiedList = new HashSet<int>();
            this.stack = new Stack<int>();

            for (int i = 0; i < v; i++)
            {
                this.AdjecencyList.Add(new List<int>());
            }
        }

        public void AddEdge(int u, int v)
        {
            this.AdjecencyList[u].Add(v);
        }

        public void DFS(int v)
        {
            this.stack.Push(v);

            while (this.stack.Count > 0)
            {
                var vertex = this.stack.Pop();
                if (!this.visitiedList.Contains(vertex))
                {
                    Console.WriteLine(vertex);
                    this.visitiedList.Add(vertex);
                }

                foreach (var adjNode in this.AdjecencyList[vertex])
                {
                    if (!this.visitiedList.Contains(adjNode))
                    {
                        this.stack.Push(adjNode);
                    }
                }
            }
        }

        public void DFS_recursive(int v)
        {
            this.visitiedList.Add(v);
            Console.WriteLine(v);

            foreach (var vertex in this.AdjecencyList[v])
            {
                if (!this.visitiedList.Contains(vertex))
                {
                    this.DFS_recursive(vertex);
                }
            }
        }

        public void TopologicalSort()
        {
            for (int i = 0; i < this.V; i++)
            {
                if (!this.visitiedList.Contains(i))
                {
                    this.TopologicalSortUtil(i);
                }
            }

            foreach (var item in this.stack)
            {
                Console.WriteLine(item);
            }
        }

        private void TopologicalSortUtil(int v)
        {
            this.visitiedList.Add(v);

            foreach (var e in this.AdjecencyList[v])
            {
                if (!this.visitiedList.Contains(e))
                {
                    TopologicalSortUtil(e);
                }
            }

            this.stack.Push(v);
        }
    }
}