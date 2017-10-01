namespace CodingPractice
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            TreeOperations treeOperations = new TreeOperations();
            //TreeNode root = null;
            //int index = 0;
            //root = treeOperations.BuildBSTFromPreorder(new int[] {11, 12,13,14,15});
            //treeOperations.PrintPreorder(root);
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

            //treeOperations.PrintPreorder(root);
           // treeOperations.Preorder_Iterative_2(root);

            //TreeNode prev = null;
            //int count = 0;
            //treeOperations.FindSwapedNodeInBST(root, ref prev, ref count);

            //int[] arr = { 5, 1, 4, 3, 6, 8, 10, 7, 9};
            //////int[] arr = { 50,5,20,30,40 };

            //int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            ////int[] a = { 2, 3, 1, 4, 1, 0, 0, 1, 2, 1 }; 
            //ArrayOperations arrOp = new ArrayOperations();

            //arrOp.PrintNextGreaterElement(new int[] { 5, 1, 4, 3, 6, 8, 10, 7, 9 });
            //arrOp.PrintSpiralMatrix(matrix);
            //arrOp.GeneratePaldindromes(204);
            //Console.WriteLine(arrOp.findElement(arr));
            //Console.WriteLine(arrOp.SearchIncreasingDecreasingArray(arr, 4));

            //arrOp.PrintNextGreaterElement(arr);

            //arrOp.PartitionArray(arr, 3);

            //StringOperations strOps = new StringOperations();

            //S = "ADOBECODEBANC"
            //T = "ABC"
            //Minimum window is "BANC".
            //Console.WriteLine(strOps.MinWindow("ADOBECODEBANC", "ABC"));
            // Console.WriteLine(strOps.MinWindow("AAAOABC", "ABC"));
            //int distance = strOps.LVDistance("kitten", "sitting");
            //Console.WriteLine(distance);   

            ListNode head = new ListNode(1);
            head.Next = new ListNode(2);
            head.Next.Down = new ListNode(5);
            head.Next.Down.Down = new ListNode(6);
            head.Next.Next = new ListNode(3);
            head.Next.Next.Down = new ListNode(7);
            head.Next.Next.Down.Down = new ListNode(8);
            head.Next.Next.Next = new ListNode(4);
            //ListNode prev = null;
            //ListNode newHead = null;
            head.FlattenList(head);
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
