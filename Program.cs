namespace CodingPractice
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {

            //int x = 1 << 3;

            //PowerSet("abc");
            //Graph g = new Graph(4);
            //g.AddEdge(0, 1);
            //g.AddEdge(0, 2);
            //g.AddEdge(1, 2);
            //g.AddEdge(2, 0);
            //g.AddEdge(2, 3);
            //g.AddEdge(3, 3);

            //g.DFS(2);

            //g.Neighbors.Add(new Graph(2));
            //g.Neighbors.Add(new Graph(2));

            //Graph g2 = new Graph(2);
            //g2.Neighbors.Add(new Graph(5));
            //g2.Neighbors.Add(new Graph(3));

            //Graph g3 = new Graph(1);

            //List<Graph> nodes = new List<Graph> { g2, g, g3 };
            //List<int> visitedList = new List<int>();
            //foreach(var node in nodes)
            //{
            //    g.Visit(visitedList);
            //}


            TreeOperations treeOperations = new TreeOperations();
            //TreeNode root = null;
            //int index = 0;
            //treeOperations.CreateTree(ref root, new int[] {20, 8, 4, 12, -1 }, ref index);
            //treeOperations.InorderIterative(root);
            TreeNode root = new TreeNode(15);

            root.Left = new TreeNode(10);
            root.Right = new TreeNode(19);

            root.Left.Left = new TreeNode(17);
            root.Left.Right = new TreeNode(11);

            root.Right.Left = new TreeNode(5);
            root.Right.Right = new TreeNode(21);

            //treeOperations.InorderIterative(root);

            //List<int> path = new List<int>();
            //path.Add(root.Value);

            //treeOperations.FindLCA(root, 17, 5);

            //treeOperations.CreateBST(root, new TreeNode(10));
            //treeOperations.CreateBST(root, new TreeNode(20));
            //treeOperations.CreateBST(root, new TreeNode(5));
            //treeOperations.CreateBST(root, new TreeNode(13));
            //treeOperations.CreateBST(root, new TreeNode(20));
            //treeOperations.CreateBST(root, new TreeNode(19));

            //treeOperations.InorderIterative(root);

            //TreeNode prev = null;
            //int count = 0;
            //treeOperations.FindSwapedNodeInBST(root, ref prev, ref count);

            //int[] arr = { 5, 1, 4, 3, 6, 8, 10, 7, 9};
            //////int[] arr = { 50,5,20,30,40 };

            //int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            ////int[] a = { 2, 3, 1, 4, 1, 0, 0, 1, 2, 1 }; 
            ArrayOperations arrOp = new ArrayOperations();

            Console.WriteLine(arrOp.TapRainWater(new int[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 }));
            //arrOp.PrintSpiralMatrix(matrix);
            //arrOp.GeneratePaldindromes(204);
            //Console.WriteLine(arrOp.findElement(arr));
            //Console.WriteLine(arrOp.SearchIncreasingDecreasingArray(arr, 4));

            //arrOp.PrintNextGreaterElement(arr);

            //arrOp.PartitionArray(arr, 3);

            StringOperations strOps = new StringOperations();

            //S = "ADOBECODEBANC"
            //T = "ABC"
            //Minimum window is "BANC".
            //Console.WriteLine(strOps.MinWindow("ADOBECODEBANC", "ABC"));
            Console.WriteLine(strOps.MinWindow("AAAOABC", "ABC"));
            //int distance = strOps.LVDistance("kitten", "sitting");
            //Console.WriteLine(distance);   

            //ListNode head = new ListNode(1);
            //head.Next = new ListNode(2);
            //head.Next.Next = new ListNode(3);
            //head.Next.Next.Next = new ListNode(4);
            //ListNode prev = null;
            //ListNode newHead = null;
            //head.ReverseList(head, ref prev, ref newHead);
        }

        public static void PowerSet(string set)
        {
            int max = 1 << set.Length;
            List<List<string>> allSets = new List<List<string>>();

            for (int i = 0; i < max; i++)
            {
                int index = 0;
                List<string> subset = new List<string>();

                for (int k = i; k > 0; k = k >> 1)
                {
                    if ((k & 1) > 0)
                    {
                        subset.Add(set[index].ToString());
                    }
                    index++;
                }
                allSets.Add(subset);
            }

            foreach (var item in allSets)
            {
                foreach (var s in item)
                {
                   Console.WriteLine(" " + s);
                }
                Console.WriteLine();
            }
        }
    }
}
