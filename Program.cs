﻿namespace CodingPractice
{
    using CodingPractice;
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            //ArrayOperations arrayOperations = new ArrayOperations();

            //arrayOperations.PrintRange(new List<List<int>>
            //{
            //    new List<int> {1,2,3,4},
            //    new List<int> {1, 3, 4, 5, 7},
            //});

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
            TreeNode root = new TreeNode(15);

            root.Left = new TreeNode(10);
            root.Right = new TreeNode(20);

            root.Left.Left = new TreeNode(5);
            root.Left.Right = new TreeNode(11);

            root.Right.Left = new TreeNode(1);
            //root.Right.Right = new TreeNode(17);

            //root.Left.Left.Left = new TreeNode(18);
            //root.Left.Left.Right = new TreeNode(25);


            TreeOperations treeOps = new TreeOperations();
            var r = treeOps.PathSum3(root, 36);
            //root = treeOps.Util(new int[] { 1, 2, 3, 4, 5 },0, 4);

            //TreeNode t3 = null;

            //t3 = treeOps.MergeTree2(root, root2);

            //var dict = new Dictionary<int, int>();

            //treeOps.PrintRightViewOfTree(root, 0, dict);

            ////treeOps.PrintAllPathMatchToSum(root, 34, new Stack<int>());

            //var rt = new TrieNode();

            //treeOps.BuildTrie(new string[] { "sachin", "chimu" }, rt);

            //System.Console.WriteLine(treeOps.IsPresentInTrie(rt, "saner"));

            //var result = treeOps.FindLCA(root, 10, 17);
            var str = new  List<string>();

            StringOperations so = new StringOperations();
            var res2 = so.AutoCompleteIDE("MVC", new List<string> {"ModelViewController", "MouseClickHandler","MouseHandler","MouseCHdan" });

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
