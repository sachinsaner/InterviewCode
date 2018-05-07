namespace CodingPractice
{
    using CodingPractice;
    using System;
    using System.Collections.Generic;

    //Sliding window
    //https://leetcode.com/problems/find-all-anagrams-in-a-string/discuss/92007/sliding-window-algorithm-template-to-solve-all-the-leetcode-substring-search-problem
   

    class Program
    {
        static void Main(string[] args)
        {

            StringOperations ss = new StringOperations();
            ss.WordBreak2("catsanddog");
                                     

            SegmentTree segmentTree = new SegmentTree(0, 5);

            var root1 = segmentTree.BuildTree(new int[] { 1, -1, 0, 2, -2}, 0, 4);
            var temp = segmentTree.GetRange(root1, 1, 4);

            //StringOperations stringOperations = new StringOperations();
            //stringOperations.MinWindow("ADOBECODEBANCB","ABBC");

            //BFS bFS = new BFS();
            //bFS.RemoveInvalidParentheses("()())()");
            //to.LongestWord(new string[]{"m", "mo", "moc", "moch", "mocha", "l", "la", "lat", "latt", "latte", "c", "ca", "cat"});
  
            //var num = TinyUrlEncode.Decode(url);
            ArrayOperations arrayOperations = new ArrayOperations();

            var t = arrayOperations.FindElement(new int[] { 5, 1, 4, 3, 6, 8, 10, 7, 9});

            arrayOperations.PrintRange(new List<List<int>>
            {
                new List<int> {1,2,3,4},
                new List<int> {1, 3, 4, 5, 7},
            });

            //arrayOperations.IsRectangle(new int[,]
            //{
            //    {1, 0, 0, 1, 0},
            //    {1, 0, 1, 1, 1},
            //    {1, 0, 0, 0, 0},
            //    {1, 0, 0, 1, 1},

            //});

            //int res5 = ArrayOperations.GetMaxArea(new int[] { 4, 2, 0, 3, 2, 5});

            //int res4 = arrayOperations.MaximalRectangle(new char[,] 
            //{ 
            //    {'0', '1'},
            //    {'1', '0'}
            //    //{'0', '1', '1', '0'},
            //    //{'0', '1', '1', '1'},
            //    //{'0', '1', '1', '0'},
            //    //{'1', '1', '1', '0'},
            //});

            //Graph g = new Graph(6);

            //var grid = new int[,] { { 1, 1, 1 }, { 1, 0, 0 }, { 1, 0, 0 } };

            //g.CleanSpace(grid, 0, 0);
            //g.AddEdge(5, 2);
            //g.AddEdge(5, 0);
            //g.AddEdge(4, 0);
            //g.AddEdge(4, 1);
            //g.AddEdge(2, 3);
            //g.AddEdge(3, 1);

            //g.TopologicalSort();

            //StringOperations s1 = new StringOperations();
            //s1.GenerateT9Combinations("23");

            //s1.PermuteUnique(new int[]{1,1,2});
            //ArrayOperations ao = new ArrayOperations();
            //ao.GetRightRange(new int[]{ 8, 8}, 8);

            //ao.IncreasingTriplet(new int[]{ 6, 2, 8, 3, 1, 4});
            //IsCircular("R");
            //alert(new int[] { 5, 1, 2, 100, 2, 2 }, 3, 1.5F);

            //TreeNode root = null;
            //int index = 0;
            //root = treeOperations.BuildBSTFromPreorder(new int[] {11, 12,13,14,15});
            //treeOperations.PrintPreorder(root);

            TreeOperations treeOps = new TreeOperations();

            var root = treeOps.BuildTree(new List<string> {"4", "-7", "-3", "null", "null", "-9", "-3", "9", "-7",
                "-4", "null", "6", "null", "-6", "-6", "null", "null", 
                "0", "6", "5", "null", "9", "null", "null", "-1", "-4", "null", "null", "null", "-2" });

            treeOps.Preorder_Iterative(root);

           
            int[] arr = { 1, 3, 5, 2, 8, 4, 6 };

            //int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            //int[] a = { 3, 34, 4, 12, 5, 2 }; 
            ArrayOperations arrOp = new ArrayOperations();
            arrOp.LengthOfLIS(arr);

            //[186,419,83,408]
            //6249
            //arrOp.CoinChange(new int[] { 186, 419, 83, 408 }, 6249);

            //arrOp.CoinChange(new int[] { 1}, 2);
            //Console.WriteLine(arrOp.MinimumCoinBottomUp(6249, new int[] { 186, 419, 83, 408}));

            //arrOp.SubsetSumExists2(a, 0, 0, 18, new int[a.Length]);

            //arrOp.PrintNextGreaterElement(new int[] { 5, 1, 4, 3, 6, 8, 10, 7, 9 });
            //arrOp.PrintSpiralMatrix(matrix);
            //arrOp.GeneratePaldindromes(204);
            //Console.WriteLine(arrOp.findElement(arr));
            //Console.WriteLine(arrOp.SearchIncreasingDecreasingArray(arr, 4));

            //arrOp.PrintNextGreaterElement(arr);

            //arrOp.PartitionArray(arr, 3);

            //StringOperations strOps = new StringOperations();

            //Console.WriteLine(strOps.LongestPalindromicSubstring("forgeeksskeegfor"));

            //var words = new List<string>()
            //{
            //    //"This", "is", "an", "example", "of", "text", "justification."
            //    "world", "owes", "you", "a", "living;", "the", "world", "owes", "you", "nothing;", "it", "was", "here", "first."

            //};
            //"What", "must", "be", 
            //var words = new List<string>()
            //{
            //    "shall", "be."
            //};

            //strOps.FullJustify(words.ToArray(), 30);
            //var res = strOps.NumEncoding("1234");

            //foreach(var item in res)
            //{
            //    Console.WriteLine(item);
            //}

            //S = "ADOBECODEBANC"
            //T = "ABC"
            //Minimum window is "BANC".
            //Console.WriteLine(strOps.CheckAnagram("abcxyzaaxbycaabc", "aabc"));
            //Console.WriteLine(strOps.CheckAnagram("AAAOABC", "ABC"));
            //Console.WriteLine(strOps.MinimumWindow("ADOBECODEBANC", "ABC"));
            //int distance = strOps.LVDistance("kitten", "sitting");
            //Console.WriteLine(distance);   

            //ListNode head = new ListNode(1);
            //int count = 6;
            //ListNode temp = head;
            //for (int i = 2; i <= count; i++)
            //{
            //    temp.Next = new ListNode(i);
            //    temp = temp.Next;
            //}

            // head.Next = new ListNode(2);
            ////head.Next.Down = new ListNode(5);
            ////head.Next.Down.Down = new ListNode(6);
            // head.Next.Next = new ListNode(3);
            ////head.Next.Next.Down = new ListNode(7);
            ////head.Next.Next.Down.Down = new ListNode(8);
            //head.Next.Next.Next = new ListNode(4);
            //////ListNode prev = null;
            //////ListNode newHead = null;
            ////head.FlattenList(head);

            //head.ReverseListInBlocks(head, 3);

            //StringOperations strop = new StringOperations();
            //Console.WriteLine(strop.NumDecodings("123"));

            //StringOperations strop = new StringOperations();
            ////Console.WriteLine(strop.CheckAnagram("xyzbbacc","abcc"));
            //Console.WriteLine(strop.CheckAnagram("ADOBECODEBANCB ", "ABBC"));

            //ArrayOperations ops = new ArrayOperations();

            //int[,] board = new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            //ops.GameOfLife(board);


            //so.Partition("nitin");

            //var ans = so.Permute(new int[] {1,2,3});
            
            //Console.WriteLine(so.LongestValidParentheses(")()())()()("));
            //Console.WriteLine(so.LongestValidParentheses(")(()(()(((())(((((()()))((((()()(()()())())())()))()()()())(())()()(((()))))()((()))(((())()((()()())((())))(())))())((()())()()((()((())))))((()(((((()((()))(()()(())))((()))()))())"));
            //Console.WriteLine(so.LongestValidParentheses("(()"));
            //Console.WriteLine(so.LongestValidParentheses("())"));
            //Console.WriteLine(so.LongestValidParentheses(")()"));
            //Console.WriteLine(so.LongestValidParentheses("((("));
            //Console.WriteLine(so.IsInterleave("a", "", "a"));

            //Console.WriteLine(so.LadderLength("hit", "cog", new List<string>{"hot", "dot", "dog", "lot", "log", "cog"}));
        }
    }
}
