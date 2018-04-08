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

        enum Direction
        {
            N,
            E,
            S,
            W
        };


        int Rows = 0;
        int Colms = 0;

        /*
         * Design a robot cleaner algorithm with 4 given APIs and a starting position in an unknown space (with obstacles in random locations) 
           The 4 APIs are: 
           clean(): clean the current location.
           turnleft(k): turn left k*90 degrees.
           turnrigt(k): turn right k*90 degrees.
           move(): move forward for 1 position, return False if that’s not possible.
         */

        public void CleanSpace(int[,] grid, int x, int y)
        {
            this.Rows = grid.GetLength(0);
            this.Colms = grid.GetLength(1);
            DFSUtil(grid, 0, 0, Direction.S);
        }
      
        private Direction DFSUtil(int[,] grid, int r, int c, Direction currentDirection)
        {
            Direction prevDirection;

            if(r < 0 || c < 0 || r >= Rows || c >= Colms || grid[r,c] == 0)
            {
                return currentDirection;
            }
            else
            {
                grid[r, c] = 0;
               
                prevDirection = DFSUtil(grid, r + 1, c, Direction.S);

                //turnleft(1) // TurnRobot(currDir, prevDir)
                prevDirection = DFSUtil(grid, r, c + 1, Direction.E);

                //turnleft(1) // TurnRobot(currDir, prevDir)
                prevDirection = DFSUtil(grid, r - 1, c, Direction.N);

                //turnleft(1) // TurnRobot(currDir, prevDir)
                prevDirection = DFSUtil(grid, r, c - 1, Direction.W);

                //turnright(1) and move, now you returning back
                return Direction.N;
            }
        }
    }
}