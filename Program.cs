namespace CodingPractice
{
    using CodingPractice;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    //Sliding window
    //https://leetcode.com/problems/find-all-anagrams-in-a-string/discuss/92007/sliding-window-algorithm-template-to-solve-all-the-leetcode-substring-search-problem
    class Program
    {
        private static HashSet<string> words = new HashSet<string>
        {
            //"mobile",
            "samsung",
            "sam",
            "sung",
            //"man",
            //"mango",
            //"icecream",
            //"and",
            //"go",
            //"i",
            //"like",
            //"ice",
            //"cream",
            "cats",
            "cat",
            "dog",
            "and",
            "sand"

			//"pineapple","apple","pen","applepen","pine",
        };

        public static int MaxDistToClosest(int[] seats)
        {
            int maxLen = 0;
            int start = 0;
            int len = 0;
            for (int i = 0; i < seats.Length; i++)
            {
                if (seats[i] == 1)
                {
                    start = i;
                }
                while (i < seats.Length && seats[i] == 0)
                {
                    len++;
                    i++;

                    if (i == seats.Length - 1)
                    {
                        maxLen = Math.Max(maxLen, (i - start) - 1);
                        break;
                    }

                }
                if (len > 0)
                {
                    i--;
                    maxLen = Math.Max(maxLen, len / 2);
                    len = 0;
                }
            }

            return maxLen + 1;
        }

        public static int HammingDistance(int x, int y)
        {
            int count = 0;
            while (x != 0 || y != 0)
            {
                if (!(((x & 1) == 1 && (y & 1) == 1) || ((x & 1) == 0 && (y & 1) == 0)))
                {
                    count++;
                }

                x = x >> 1;
                y = y >> 1;
            }

            return count;

        }

        public static bool ValidWordAbbreviation(string word, string abbr)
        {
            int j = 0;
            for (int i = 0; i < abbr.Length; i++)
            {
                if (!int.TryParse(abbr[i].ToString(), out int number))
                {
                    j = word.IndexOf(abbr[i], j);
                    if (j < 0)
                    {
                        return false;
                    }
                }
                else
                {
                    int start = i;
                    while (int.TryParse(abbr[i].ToString(), out int digit))
                    {
                        i++;
                    }
                    var digits = abbr.Substring(start, i - 1);
                    number = int.Parse(digits);
                    int end = number + j + 1;
                    if (end > word.Length)
                    {
                        return false;
                    }
                    else
                    {
                        if (word[end] == abbr[i])
                        {
                            j += end;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }

            }

            return true;

        }

        static void Main(string[] args)
        {
            //var res = ValidWordAbbreviation("internationalization", "i12iz4n");

            //Practice p = new Practice();

            //int[] nums = new int[] { -10, -3, 0, 5, 9 };

            //p.SortedArrayToBST(nums);

            //["LRUCache","put","put","get","put","put","get"]
            //[[2],       [2,1],[2,2], [2], [1,1],[4,1], [2]]

            //LRUCache cache = new LRUCache(2);

            //cache.Put(2, 1);
            //cache.Put(2, 2);
            ////cache.Put(3, 3);
            ////cache.Put(4, 4);
            ////cache.Put(5, 5);


            //Console.WriteLine(cache.Get(2));

            //cache.Put(1, 1);
            //cache.Put(4, 1);

            //Console.WriteLine(cache.Get(2));

            //cache.Put(4, 4);

            //Console.WriteLine(cache.Get(1));
            //Console.WriteLine(cache.Get(3));
            //Console.WriteLine(cache.Get(4));

            //cache.Put(6, 6);


            //int[][] grid = new int [3][];

            //grid[0] = new int[] { 1, 2, 3 };
            //Console.WriteLine("Row : " + grid.GetLength(0));
            //Console.WriteLine("cols : " + grid[0].Length);

            //Dictionary<int> map = new HashSet<int>()
            //StringOperations stringOperations = new StringOperations();
            //var t = stringOperations.LongestWord(new string[] { "a", "banana", "app", "appl", "ap", "apply", "apple" });

            //var t2 = HammingDistance(1, 4);

            //StringOperations stringOperations = new StringOperations();

            //var t1 = stringOperations.IsSortedByOrder(new string[]{"cc","cb","bb","ac"}, new char[]{'b','c','a'});

            //ArrayOperations arrayOperations = new ArrayOperations();

            //var res = arrayOperations.MinSubArrayLen(5, new int[] { 2, 3, 1, 1, 1, 1, 1 });

            //var res = new BackTracking().GenerateParenthesis(2);
            //int[][] a = new int[][]
            //{
            //     new int[] {0, 0, 0 },
            //     new int[] {0, 1, 0 },
            //    new int[] {0, 0, 0 }
            //};

            //var dfs = new DFS();

            //Console.WriteLine(dfs.UniquePathsWithObstacles(a));
            //      char[][] board = new char[][]
            //{
            //      new char[] {'A', 'B', 'C', 'E'},
            //      new char[] {'S', 'F', 'E', 'S'},
            //      new char[] {'A', 'D', 'E', 'E'}
            //};
            //      string searchWord = "ABCESEEEFS";

            //      var p = new Practice();
            //      //Console.WriteLine(p.Exist(board, searchWord));
            //      //p.BuildTrie(new string[] { "sachin", "saner", "diptej", "sac", "sad" });
            //      p.NumDistinctIslands(null);

            //Console.WriteLine(p.IsWord("sachin"));
            //Console.WriteLine(p.IsWord("saner"));
            //Console.WriteLine(p.IsWord("diptej"));
            //Console.WriteLine(p.IsWord("sac"));
            //Console.WriteLine(p.IsWord("dip"));
            //Console.WriteLine(p.IsWord("dipte"));
            //Console.WriteLine(p.IsWord("abc"));

            //var sol = p.GetSuggestions("sa");
            //foreach(var word in sol)
            //{
            //    Console.WriteLine(word);
            //}

            //arrayOperations.PrintZigZagMatrix(a);
            //bool found = false;

            //DFS(a, 0, 0, "right", ref found);

            //IList<IList<int>> res = new List<IList<int>>();

            //IList<IList<int>> res = new IList<LinkedList<int>>();

            //TreeOperations treeOperations = new TreeOperations();
            //TreeNode root = new TreeNode(1);
            //root.left = new TreeNode(2);
            //root.left.left = new TreeNode(3);
            //root.right = new TreeNode(1);
            //root.right.left = new TreeNode(11);

            //root.left.left = new TreeNode(6);
            //root.left.left.left = new TreeNode(44);
            //root.left.left.right = new TreeNode(23);
            //root.Left.Right.Right = new TreeNode(1);

            //root.Left.Left.Left = new TreeNode(3);
            //root.Left.Left.Right = new TreeNode(-2);

            //treeOperations.Flatten(root);

            //var root = treeOperations.BuildTree(new List<string> { "1", "1" });

            //TreeNode prev = null;
            //var res = treeOperations.ISBST(root, ref prev);

            //treeOperations.Preorder_Iterative(root);
            //Stacks s = new Stacks();
            //s.SimplifyPath("/home/");

            //RandomizedCollection r = new RandomizedCollection();
            //Console.WriteLine(r.Insert(0));
            //Console.WriteLine(r.Insert(1));
            //Console.WriteLine(r.Remove(0));
            //Console.WriteLine(r.Insert(2));
            //Console.WriteLine(r.Remove(1));
            //Console.WriteLine(r.GetRandom());

            //WarCardGame warCardGame = new WarCardGame();
            //warCardGame.PlayGame();
            //LetterCount();
            //

            //List<Player> platers = new List<Player>();
            //for(int i =0; i < 3; i++)
            //{
            //    platers.Add(new Player() { Name = (i + 1).ToString() });
            //}

            //List<int> cards = new List<int>();
            //for(int i = 1; i < 53; i++)
            //{
            //    cards.Add(i);
            //}

            //WarCardGameMultiPlayer wg = new WarCardGameMultiPlayer(platers, cards);
            //wg.PlayGame();
            //ShortSubStr();
            //[[],["/leet",1],["/leet/code",2],["/leet/code"],["/c/d",1],["/c"]]

            //["FileSystem", "createPath", "createPath", "get", "createPath", "get"]
            //[[],["/leet",1],["/leet/code",2],["/leet/code"],["/leet/code",3],["/leet/code"]]
            //FileSystem fileSystem = new FileSystem();
            //fileSystem.CreatePath("/leet", 1);
            //fileSystem.CreatePath("/leet/code", 2);
            //fileSystem.CreatePath("/leet/code", 3);

            //fileSystem.CreatePath("/leet/code/tree", 3);
            //fileSystem.CreatePath("/leet/sach/tree", 6);

            //fileSystem.Get("/leet/code/tree");
            //fileSystem.Get("/leet/sach/tree");
            //fileSystem.Get("/leet2/sach/tree");

            //[P1, P2, D1, D2]==> valid
            //[P1, D1, P2, D2] ==> valid
            //[P1, D2, D1, P2] ==> invalid
            //[P1, D2] ==> invalid
            //[P1, P2] ==> invalid
            //[P1, D1, D1] ==> invalid
            //[] ==> valid
            //[P1, P1, D1] ==> invalid
            //[P1, P1, D1, D1] ==> invalid
            //[P1, D1, P1] ==> invalid
            //[P1, D1, P1, D1] ==> invalid

            DoorDashInterview doorDashInterview = new DoorDashInterview();

            //Console.WriteLine(doorDashInterview.LongestValidOrder(new List<string>() { "P11", "P12", "D11", "D12" }));
            //Console.WriteLine(doorDashInterview.LongestValidOrder(new List<string>() { "P1", "D1", "P2", "D2" }));
            //Console.WriteLine(doorDashInterview.LongestValidOrder(new List<string>() { "P1", "D2", "D1", "P2" }));
            //Console.WriteLine(doorDashInterview.LongestValidOrder(new List<string>() {  }));

            //Console.WriteLine(doorDashInterview.MinSteps("leetcode","practice"));
            //[4,2,4,8,2]
            //[5,5,5,10,8]
            //[1,2,8,10,4]

            //int[] startTime = new int[] { 4,2,4,8,2 };
            //int[] endTime = new int[] { 5,5,5,10,8 };
            //int[] profit = new int[] { 1,2,8,10,4 };
            //Console.WriteLine(doorDashInterview.JobScheduling(startTime, endTime, profit));

            //doorDashInterview.AppendTime("Mon 11:50 PM", "TUE 2:00 PM", 15, is12hrs:true);

            //doorDashInterview.GetShortestDistance();

            //int[][] a = new int[][]
            //{
            //   new int[] {0, 0, 1 },
            //   new int[] {1, 0, 0 },
            //   new int[] {0, 0, 0 }
            //};

            //Practice p = new Practice();
            //Console.WriteLine(p.ShortestPathBinaryMatrix(a));

            //BFS b = new BFS();


            //b.AlienOrder(new string[] { "wrt", "wrf", "er", "ett", "rftt" });

            //[["x1","x2"],["x2","x3"],["x3","x4"],["x4","x5"]]
            //[3.0,4.0,5.0,6.0]
            //[["x1","x5"],["x5","x2"],["x2","x4"],["x2","x2"],["x2","x9"],["x9","x9"]]

            //Solution s = new Solution();
            //var preq = new int[][]
            //{
            //    new int[]{1, 0},
            //    new int[]{2, 0},
            //    new int[]{3, 1},
            //    new int[]{3, 2},
            //};

            //s.FindOrder(4, preq);
            //var eq = new List<IList<string>>()
            //{
            //    new List<string> { "x1", "x2" },
            //    new List<string> { "x2", "x3" },
            //    new List<string> { "x3", "x4" },
            //    new List<string> { "x4", "x5" },
            //};

            //var vals = new double[] { 3.0, 4.0, 5.0, 6.0 };

            //var w = new List<IList<string>>()
            //{
            //    new List<string> { "x1", "x5" },
            //    new List<string> { "x5", "x2" },
            //    new List<string> { "x2", "x4" },
            //    new List<string> { "x2", "x2" },
            //    new List<string> { "x2", "x9" },
            //    new List<string> { "x9", "x9" },
            //};


            //var eq = new List<IList<string>>()
            //{
            //    new List<string> { "a", "b" },
            //    new List<string> { "c", "d" },

            //};

            //var vals = new double[] { 1.0, 1.0 };

            //var w = new List<IList<string>>()
            //{
            //    new List<string> { "a", "c" },
            //    new List<string> { "b", "d" },
            //    new List<string> { "b", "a" },
            //    new List<string> { "d", "c" },

            //};

            //s.CalcEquation(eq, vals, w);

            //int[] arr = { 3, 1, 6, 4, 5, 2 };

            int[] arr = { 1,2,3,2 };
            // Function call
            //s.MaxSumMinProduct(arr);

            //s.MaxValue(arr, arr.Length);

            //MyHashMap map = new MyHashMap();

            //map.Put(1, 1);
            //map.Put(2, 2);
            //Console.WriteLine("Get {0}", map.Get(1));
            //Console.WriteLine("Get {0}", map.Get(3));
            //map.Put(2, 1);
            //Console.WriteLine("Get {0}", map.Get(2));
            //map.Remove(2);
            //Console.WriteLine("Get {0}", map.Get(2));

            //MedianFinder medianFinder = new MedianFinder();
            //medianFinder.AddNum(1);
            //medianFinder.AddNum(2);
            //Console.WriteLine(medianFinder.FindMedian());
            //medianFinder.AddNum(3);
            //Console.WriteLine(medianFinder.FindMedian());

            Practice practice = new Practice();
            var teams = new string[]
            {
                "ABC","ACB","ABC","ACB","ACB"
            };

            Console.WriteLine(practice.RankTeams(teams));
        }

        public int EvalRPN(string[] tokens)
        {

            Stack<int> stack = new Stack<int>();
            int res = 0;
            foreach (var t in tokens)
            {
                if (Int32.TryParse(t, out int val))
                {
                    stack.Push(val);
                }
                else
                {

                    int val2 = stack.Pop();
                    int val1 = stack.Pop();

                    switch (t)
                    {
                        case "+":
                            res = val1 + val2;
                            stack.Push(res);
                            break;
                        case "-":
                            res = val1 - val2;
                            stack.Push(res);
                            break;
                        case "*":
                            res = val1 * val2;
                            stack.Push(res);
                            break;
                        case "/":
                            res = val1 / val2;
                            stack.Push(res);
                            break;

                    }


                }

            }

            return res;

        }

    }

   
}
