using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingPractice
{
    public class Practice
    {
        TrieNode root;
        int ROWS = 4;
        int COLS = 4;

        //1 0 0 0 
        //1 1 0 1 
        //0 1 0 0 
        //0 1 1 1



        public Practice()
        {
            root = new TrieNode();
        }

        //public void ReadGrid()
        //{
        //    HashSet<string> visited = new HashSet<string>();
        //    int count = 1;
        //    var res = Util(0, 0, grid, visited, ref count);

        //    Console.WriteLine(res);

        //    Console.WriteLine("Total count: " + count);
        //}

        private bool Util(int i, int j, int[,] grid, HashSet<string> visited, ref int count)
        {

            if (i == ROWS - 1 && j == COLS - 1 && grid[i, j] == 1)
            {
                Console.WriteLine(" [{0},{1}] value : {2}", i, j, grid[i, j]);
                return true;
            }

            if (i >= ROWS || i < 0 || j >= COLS || j < 0 || visited.Contains(i + "->" + j) || grid[i, j] == 0)
            {
                return false;
            }

            count++;
            Console.WriteLine(" [{0},{1}] value : {2}", i, j, grid[i, j]);

            visited.Add(i + "->" + j);

            if (Util(i + 1, j, grid, visited, ref count) ||
               Util(i, j + 1, grid, visited, ref count) ||
               Util(i - 1, j, grid, visited, ref count) ||
               Util(i, j - 1, grid, visited, ref count))
            {
                return true;
            }

            visited.Remove(i + "->" + j);

            return false;
        }


        //s-a-c-h-i-n
        public void BuildTrie(string[] words)
        {
            TrieNode curr = root;

            foreach (var word in words)
            {
                foreach (var ch in word)
                {
                    if (curr.Children.ContainsKey(ch))
                    {
                        curr = curr.Children[ch];
                    }
                    else
                    {
                        curr.Children.Add(ch, new TrieNode() { Chr = ch });
                        curr = curr.Children[ch];
                    }
                }

                curr.IsWord = true;
                curr = root;
            }
        }

        public bool IsWord(string word)
        {
            TrieNode curr = root;

            foreach (var ch in word)
            {
                if (curr.Children.ContainsKey(ch))
                {
                    curr = curr.Children[ch];
                    if (curr.IsWord)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public List<string> GetSuggestions(string word)
        {
            List<string> sol = new List<string>();
            var curr = GetLastNode(word);

            foreach (var child in curr.Children)
            {
                Util(word, child.Value, ref sol);
            }
            return sol;
        }

        private void Util(string word, TrieNode curr, ref List<string> sol)
        {
            if (curr == null)
            {
                return;
            }

            var newWord = word + curr.Chr;
            if (curr.IsWord)
            {
                sol.Add(newWord);
            }

            if (curr.Children == null || curr.Children.Count == 0)
            {
                return;
            }

            foreach (var child in curr.Children)
            {
                Util(newWord, child.Value, ref sol);
            }
        }

        private TrieNode GetLastNode(string word)
        {
            TrieNode curr = root;

            foreach (var ch in word)
            {
                if (curr.Children.ContainsKey(ch))
                {
                    curr = curr.Children[ch];
                }
                else
                {
                    return null;
                }
            }

            return curr;
        }

        public bool Exist(char[][] board, string word)
        {
            board = this.board;
            word = this.searchWord;
            HashSet<string> visited = new HashSet<string>();
            ROWS = board.GetLength(0);
            COLS = board[0].Length;
            int index = 0;

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    if (board[i][j] == word[0])
                    {
                        visited = new HashSet<string>();
                        if (DFS(i, j, board, word, index, visited))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        //[["A","B","C","E"],["S","F","E","S"],["A","D","E","E"]]
        //"ABCESEEEFS"
        //[["A","B","C","E"],["S","F","E","S"],["A","D","E","E"]]
        //"ABCEFSADEESE"

        char[][] board = new char[][]
        {
            new char[] {'A', 'B', 'C', 'E'},
            new char[] {'S', 'F', 'E', 'S'},
            new char[] {'A', 'D', 'E', 'E'}
        };
        string searchWord = "ABCEFSADEESE";
        bool DFS(int i, int j, char[][] grid, string word, int index, HashSet<string> visited)
        {
            if (i >= ROWS || i < 0 || j >= COLS || j < 0)
            {
                return false;
            }
            if (index > (word.Length - 1))
            {
                return false;
            }
            if (visited.Contains(i + "->" + j))
            {
                return false;
            }
            if (word[index] != grid[i][j])
            {
                return false;
            }
            if (index == word.Length - 1)
            {
                return true;
            }
            visited.Add(i + "->" + j);

            if (DFS(i, j + 1, grid, word, index + 1, visited))   // ->
            {
                return true;
            }
            if (DFS(i + 1, j, grid, word, index + 1, visited)) // down
            {
                return true;
            }
            if (DFS(i, j - 1, grid, word, index + 1, visited))    // <-
            {
                return true;
            }
            if (DFS(i - 1, j, grid, word, index + 1, visited))  // up
            {
                return true;
            }
            visited.Remove(i + "->" + j);
            return false;
        }

        //[[1,1,0,0,0],[1,1,0,0,0],[0,0,0,1,1],[0,0,0,1,1]]
        int[][] grid = new int[][]
       {
          new int[] { 1, 1, 0, 0, 0}, // 0
          new int[] { 1, 1, 0, 0, 0}, // 1
          new int[] { 0, 0, 0, 1, 1}, // 2
          new int[] { 0, 0, 0, 1, 1}, // 3
       };

        public int NumDistinctIslands(int[][] grid)
        {
            grid = this.grid;

            ROWS = grid.GetLength(0);
            COLS = grid[0].Length;

            HashSet<string> islands = new HashSet<string>();

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        var island = new List<string>();
                        DFS(grid, i, j, i, j, ref island);

                        var sb = new StringBuilder();
                        foreach (var item in island)
                        {
                            sb.Append(item);
                            sb.Append("|");
                        }

                        islands.Add(sb.ToString());
                    }
                }
            }

            return islands.Count;
        }

        void DFS(int[][] grid, int x0, int y0, int x, int y, ref List<string> island)
        {
            if (x >= ROWS || x < 0 || y >= COLS || y < 0 || grid[x][y] < 1)
            {
                return;
            }

            island.Add($"{x - x0}:{y - y0}");
            grid[x][y] = -1;

            DFS(grid, x0, y0, x + 1, y, ref island);
            DFS(grid, x0, y0, x, y + 1, ref island);
            DFS(grid, x0, y0, x - 1, y, ref island);
            DFS(grid, x0, y0, x, y - 1, ref island);

        }

        public class Point
        {
            public int r, c, Distance;

            public Point(int r, int c, int distance)
            {
                this.r = r;
                this.c = c;
                this.Distance = distance;
            }
        }

        //public int ROWS, COLS;

        public int ShortestPathBinaryMatrix(int[][] grid)
        {
            int distance = 0;
            int smallest = int.MaxValue;

            ROWS = grid.GetLength(0);
            COLS = grid[0].Length;

            if (grid == null || grid[0][0] == 1) return -1;

            Queue<Point> q = new Queue<Point>();
            HashSet<string> visited = new HashSet<string>();

            q.Enqueue(new Point(0, 0, 1));
            visited.Add(0 + ":" + 0);

            Point curr = null;
            while (q.Count > 0)
            {
                curr = q.Dequeue();
                int r = curr.r;
                int c = curr.c;

                if (r == ROWS - 1 && c == COLS - 1)
                {
                    smallest = Math.Min(smallest, curr.Distance);
                }

                if (IsSafe(grid, r + 1, c, visited))
                {
                    q.Enqueue(new Point(r + 1, c, curr.Distance + 1));
                }
                if (IsSafe(grid, r, c + 1, visited))
                {
                    q.Enqueue(new Point(r, c + 1, curr.Distance + 1));
                }
                if (IsSafe(grid, r - 1, c, visited))
                {
                    q.Enqueue(new Point(r - 1, c, curr.Distance + 1));
                }
                if (IsSafe(grid, r, c - 1, visited))
                {
                    q.Enqueue(new Point(r, c - 1, curr.Distance + 1));
                }

                if (IsSafe(grid, r + 1, c + 1, visited))
                {
                    q.Enqueue(new Point(r + 1, c + 1, curr.Distance + 1));
                }

                if (IsSafe(grid, r - 1, c - 1, visited))
                {
                    q.Enqueue(new Point(r - 1, c - 1, curr.Distance + 1));
                }

                if (IsSafe(grid, r - 1, c + 1, visited))
                {
                    q.Enqueue(new Point(r - 1, c + 1, curr.Distance + 1));
                }

                if (IsSafe(grid, r + 1, c - 1, visited))
                {
                    q.Enqueue(new Point(r + 1, c - 1, curr.Distance + 1));
                }
            }

            //if(curr.c == COLS - 1 && curr.r == ROWS - 1)
            //return curr.Distance;

            //return -1;

            return smallest == int.MaxValue ? -1 : smallest;
        }

        private bool IsSafe(int[][] grid, int r, int c, HashSet<string> visited)
        {
            var str = r + ":" + c;
            if (r >= 0 && r < ROWS && c >= 0 && c < COLS && grid[r][c] != 1 && !visited.Contains(str))
            {
                visited.Add(str);
                return true;
            }

            return false;
        }




        public int[] FindOrder(int numCourses, int[][] prerequisites)
        {
            Dictionary<int, List<int>> adjMap = new Dictionary<int, List<int>>();
            Dictionary<int, int> indegree = new Dictionary<int, int>();

            for (int i = 0; i < numCourses; i++)
            {
                indegree.Add(i, 0);
            }

            for (int i = 0; i < prerequisites.GetLength(0); i++)
            {
                int source = prerequisites[i][1];
                int dest = prerequisites[i][0];

                if (adjMap.ContainsKey(source))
                {
                    adjMap[source].Add(dest);
                }
                else
                {
                    adjMap.Add(source, new List<int>() { dest });
                }

                if (indegree.ContainsKey(dest))
                {
                    indegree[dest]++;
                }

            }
            List<int> res = new List<int>();

            Queue<int> q = new Queue<int>();
            foreach (var item in indegree)
            {
                if (item.Value == 0)
                {
                    q.Enqueue(item.Key);
                }
            }


            while (q.Count > 0)
            {
                var item = q.Dequeue();
                res.Add(item);

                if (adjMap.ContainsKey(item))
                {
                    foreach (var v in adjMap[item])
                    {
                        indegree[v]--;
                        if (indegree[v] == 0)
                        {
                            q.Enqueue(v);
                        }
                    }
                }
            }


            return res.Count == numCourses ? res.ToArray() : new List<int>().ToArray();
        }



        TreeNode root2 = null;
        public TreeNode SortedArrayToBST(int[] nums)
        {
            TreeNode root = null;
            root = BuildTree(nums, 0, nums.Length - 1);
            return root;
        }

        TreeNode BuildTree(int[] nums, int low, int high)
        {

            if (low > high) return null;

            int mid = (low + high) / 2;

            var root = new TreeNode(nums[mid]);


            root.left = BuildTree(nums, low, mid - 1);
            root.right = BuildTree(nums, mid + 1, high);

            return root;
        }


        public class Count
        {
            public char team;
            public int[] rank;

            public Count(char team, int len)
            {
                this.team = team;
                rank = new int[len];
            }
        }

        public string RankTeams(string[] votes)
        {
            int len = votes[0].Length;
            Dictionary<char, Count> map = new Dictionary<char, Count>();

            foreach (var v in votes)
            {
                int index = 0;
                foreach (var c in v)
                {
                    if (!map.ContainsKey(c))
                    {
                        map.Add(c, new Count(c, len));
                    }

                    map[c].rank[index]++;

                    index++;
                }
            }

            var voteStat = map.Values.ToArray();

            Array.Sort(voteStat, (a, b) =>
            {
                for(int index = 0; index < a.rank.Length; ++index)
                {
                    var aVotes = a.rank[index];
                    var bVotes = b.rank[index];

                    if(aVotes != bVotes)
                    {
                        return bVotes - aVotes;
                    }
                }

                return a.team - b.team;

            });


            var res = "";

            foreach(var stat in voteStat)
            {
                res += stat.team;
            }

            return res;
        }

    }
}
