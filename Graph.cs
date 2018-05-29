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
        HashSet<int> visitied;
        Stack<int> stack;

        public Graph(int v)
        {
            this.V = v;
            this.AdjecencyList = new List<List<int>>();
            this.visitied = new HashSet<int>();
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
                if (!this.visitied.Contains(vertex))
                {
                    Console.WriteLine(vertex);
                    this.visitied.Add(vertex);
                }

                foreach (var adjNode in this.AdjecencyList[vertex])
                {
                    if (!this.visitied.Contains(adjNode))
                    {
                        this.stack.Push(adjNode);
                    }
                }
            }
        }

        public void DFS_recursive(int v)
        {
            this.visitied.Add(v);
            Console.WriteLine(v);

            foreach (var vertex in this.AdjecencyList[v])
            {
                if (!this.visitied.Contains(vertex))
                {
                    this.DFS_recursive(vertex);
                }
            }
        }

        public void TopologicalSort()
        {
            for (int i = 0; i < this.V; i++)
            {
                if (!this.visitied.Contains(i))
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
            this.visitied.Add(v);

            foreach (var e in this.AdjecencyList[v])
            {
                if (!this.visitied.Contains(e))
                {
                    TopologicalSortUtil(e);
                }
            }

            this.stack.Push(v);
        }


        //https://leetcode.com/problems/sequence-reconstruction/description/
        /*
         * Check whether the original sequence org can be uniquely reconstructed from the sequences in seqs. 
         * The org sequence is a permutation of the integers from 1 to n, with 1 ≤ n ≤ 104. 
         * Reconstruction means building a shortest common supersequence of the sequences in seqs 
         * (i.e., a shortest sequence so that all sequences in seqs are subsequences of it). 
         * Determine whether there is only one sequence that can be reconstructed from seqs and it is the org sequence.

            Example 1:

            Input:
            org: [1,2,3], seqs: [[1,2],[1,3]]

            Output:
            false

            Explanation:
            [1,2,3] is not the only one sequence that can be reconstructed, because [1,3,2] is also a valid sequence that can be reconstructed.
        */
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
        /*
         * There is a new alien language which uses the latin alphabet. 
         * However, the order among letters are unknown to you. You receive a list of non-empty words from the dictionary,
         * where words are sorted lexicographically by the rules of this new language. Derive the order of letters in this language.

            Example 1:

            Input:
            [
              "wrt",
              "wrf",
              "er",
              "ett",
              "rftt"
            ]

            Output: "wertf"
            Example 2:

            Input:
            [
              "z",
              "x"
            ]

            Output: "zx"
        */
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
            DFSUtil(grid, 0, 0, Direction.S, grid.GetLength(0), grid.GetLength(1));
        }

        private Direction DFSUtil(int[,] grid, int r, int c, Direction currentDirection, int maxRows, int maxCols)
        {
            Direction prevDirection;

            if (r < 0 || c < 0 || r >= maxRows || c >= maxCols || grid[r, c] == 0)
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

        //https://leetcode.com/problems/word-search/description/
		/*
		 * Given a 2D board and a word, find if the word exists in the grid.

            The word can be constructed from letters of sequentially adjacent cell, 
            where "adjacent" cells are those horizontally or vertically neighboring. The same letter cell may not be used more than once.

            Example:

            board =
            [
              ['A','B','C','E'],
              ['S','F','C','S'],
              ['A','D','E','E']
            ]

            Given word = "ABCCED", return true.
            Given word = "SEE", return true.
            Given word = "ABCB", return false.
        */
        public bool Exist(char[,] board, string word)
        {
            HashSet<string> visited = new HashSet<string>();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == word[0])
                    {
                        //visited = new HashSet<string>();

                        if (DFS(board, visited, i, j, word, 0))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool DFS(char[,] board, HashSet<string> visited, int x, int y, string word, int index)
        {
            if (x >= board.GetLength(0) || x < 0 || y >= board.GetLength(1) || y < 0 || board[x, y] != word[index] || visited.Contains(x + "->" + y))
            {
                return false;
            }

            if (word.Length - 1 == index)
            {
                return true;
            }

            visited.Add(x + "->" + y);

            if (DFS(board, visited, x + 1, y, word, index + 1) ||
                DFS(board, visited, x, y + 1, word, index + 1) ||
                DFS(board, visited, x - 1, y, word, index + 1) ||
                DFS(board, visited, x, y - 1, word, index + 1))
            {
                return true;
            }

            visited.Remove(x + "->" + y);

            return false;
        }


		//https://leetcode.com/problems/redundant-connection/description/
        /*
         * In this problem, a tree is an undirected graph that is connected and has no cycles.

            The given input is a graph that started as a tree with N nodes (with distinct values 1, 2, ..., N), with one additional edge added. 
            The added edge has two different vertices chosen from 1 to N, and was not an edge that already existed.

            The resulting graph is given as a 2D-array of edges. Each element of edges is a pair [u, v] with u < v,
            that represents an undirected edge connecting nodes u and v.

            Return an edge that can be removed so that the resulting graph is a tree of N nodes. 
            If there are multiple answers, return the answer that occurs last in the given 2D-array. 
            The answer edge [u, v] should be in the same format, with u < v.

            Example 1:
            Input: [[1,2], [1,3], [2,3]]
            Output: [2,3]
            Explanation: The given undirected graph will be like this:
              1
             / \
            2 - 3
          */

		public int[] FindRedundantConnection(int[,] edges)
        {
			/*this is an application of union find, 
			 * 1. Creat an parent array of size of all vertices,
			 * set array such that every vertice is parent of it self
			 * 2. start going through edges such that u->v is "source" -> "destination"
			 * 3. every time we find both vertices have different parent
			 * set the from(u) vertice`s parent to v(to)
			 * 4. if we find both verices have same parents then we have detected an cycle
			 */

			int[] parent = new int[edges.GetLength(0) + 1];
            
            for (int i = 0; i < parent.Length; i++)
            {
                parent[i] = i;
            }

			for (int i = 0; i < edges.GetLength(0); i++)
			{
				int source = edges[i, 0];
				int destination = edges[i, 1];

				int x = Find(parent, source);
				int y = Find(parent, destination);

                if(x == y)
				{
					return new int[] { source, destination };
				}

				parent[x] = y;
			}

			return new int[]{0,0};
        }

        private int Find(int[] parent, int index)
        {
            if (parent[index] == index)
            {
                return index;
            }

            return Find(parent, parent[index]);
        }
    }
}