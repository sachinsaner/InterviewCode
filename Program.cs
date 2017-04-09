namespace CodingPractice
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            TreeOperations treeOperations = new TreeOperations();

            //TreeNode root = new TreeNode(71);

            //treeOperations.CreateBST(root, new TreeNode(41));
            //treeOperations.CreateBST(root, new TreeNode(31));
            //treeOperations.CreateBST(root, new TreeNode(35));
            //treeOperations.CreateBST(root, new TreeNode(34));
            //treeOperations.CreateBST(root, new TreeNode(36));
            //treeOperations.CreateBST(root, new TreeNode(57));

            int [] arr = { 3, 1, 5, 2, 6, 4 };

            ArrayOperations arrOp = new ArrayOperations();

            arrOp.SortAlternate(arr);

            foreach(var item in arr)
            {
                Console.WriteLine(item);
            }
            
        }
    }
}
