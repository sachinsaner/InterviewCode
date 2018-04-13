using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice
{
    public enum Direction
    {
        N,
        E,
        S,
        W
    };

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


        //https://leetcode.com/problems/sequence-reconstruction/description/
        public bool SequenceReconstruction(int[] org, IList<IList<int>> seqs)
        {
            if (seqs == null || seqs.Count == 0)
            {
                return false;
            }

            Dictionary<int, int> indegree = new Dictionary<int, int>();
            var map = new Dictionary<int, HashSet<int>>();

            foreach (var o in org)
            {
                indegree.Add(o, 0);
            }

            foreach (var s in seqs)
            {
                if (s.Count == 2)
                {
                    if (map.ContainsKey(s[0]))
                    {
                        map[s[0]].Add(s[1]);
                    }
                    else
                    {
                        map.Add(s[0], new HashSet<int> { s[1] });
                    }
                    indegree[s[1]]++;
                }
            }

            var q = new Queue<int>();

            foreach (var item in indegree)
            {
                if (item.Value == 0)
                {
                    q.Enqueue(item.Key);
                }
            }

            var result = new List<int>();

            while (q.Count > 0)
            {
                int item = q.Dequeue();
                result.Add(item);

                int count = 0;
                if (map.ContainsKey(item))
                {
                    foreach (var v in map[item])
                    {
                        indegree[v]--;

                        if (indegree[v] == 0)
                        {
                            count++;
                            q.Enqueue(v);
                        }

                        if (count > 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return result.Count == org.Length;
        }

        //https://leetcode.com/problems/alien-dictionary/description/
        //https://leetcode.com/problems/alien-dictionary/discuss/70119/Java-AC-solution-using-BFS/72252
        //Topological sort
        public string AlienOrder(string[] words)
        {
            string result = string.Empty;

            Dictionary<char, int> indegree = new Dictionary<char, int>();
            Dictionary<char, HashSet<char>> map = new Dictionary<char, HashSet<char>>();

            foreach (var w in words)
            {
                foreach (var c in w)
                {
                    if (!indegree.ContainsKey(c))
                    {
                        indegree.Add(c, 0);
                    }
                }
            }

            for (int i = 0; i < words.Length - 1; i++)
            {
                var word1 = words[i];
                var word2 = words[i + 1];

                for (int j = 0; j < Math.Min(word1.Length, word2.Length); j++)
                {
                    var c1 = word1[j];
                    var c2 = word2[j];

                    if (c1 != c2)
                    {
                        if (map.ContainsKey(c1))
                        {
                            if (!map[c1].Contains(c2))
                            {
                                //add edge from c1 -> c2, this means c1 comes before c2
                                map[c1].Add(c2);
                                indegree[c2]++;
                            }
                        }
                        else
                        {
                            map.Add(c1, new HashSet<char>() { c2 });
                            indegree[c2]++;
                        }
                        break;
                    }
                }
            }

            //Topological sort
            var q = new Queue<char>();

            foreach (var item in indegree)
            {
                if (item.Value == 0)
                {
                    q.Enqueue(item.Key);
                }
            }

            while (q.Count > 0)
            {
                var c = q.Dequeue();
                result += c;

                if (map.ContainsKey(c))
                {
                    foreach (var t in map[c])
                    {
                        indegree[t]--;
                        if (indegree[t] == 0)
                        {
                            q.Enqueue(t);
                        }
                    }
                }
            }

            if (result.Length != indegree.Count)
            {
                result = "";
            }
            return result;
        }

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
            DFSUtil(grid, 0, 0, Direction.S,grid.GetLength(0),grid.GetLength(1) );
        }
      
        private Direction DFSUtil(int[,] grid, int r, int c, Direction currentDirection, int maxRows, int maxCols)
        {
            Direction prevDirection;

            if(r < 0 || c < 0 || r >= maxRows || c >= maxCols || grid[r,c] == 0)
            {
                return currentDirection;
            }
            else
            {
                grid[r, c] = 0;
               
                prevDirection = DFSUtil(grid, r + 1, c, Direction.S, maxRows, maxCols);

                //turnleft(1) // TurnRobot(currDir, prevDir)
                prevDirection = DFSUtil(grid, r, c + 1, Direction.E, maxRows, maxCols);

                //turnleft(1) // TurnRobot(currDir, prevDir)
                prevDirection = DFSUtil(grid, r - 1, c, Direction.N, maxRows, maxCols);

                //turnleft(1) // TurnRobot(currDir, prevDir)
                prevDirection = DFSUtil(grid, r, c - 1, Direction.W, maxRows, maxCols);

                //turnright(1) and move, now you returning back
                return Direction.N;
            }
        }
    }
}