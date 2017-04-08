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

            TreeNode root = new TreeNode(15);

            treeOperations.CreateBST(root, new TreeNode(10));
            treeOperations.CreateBST(root, new TreeNode(20));
            treeOperations.CreateBST(root, new TreeNode(5));
            treeOperations.CreateBST(root, new TreeNode(13));
            treeOperations.CreateBST(root, new TreeNode(17));
            treeOperations.CreateBST(root, new TreeNode(35));

            TreeNode prev = null;
            TreeNode head = null;

            treeOperations.BSTToDLL(root, ref prev, ref head);

            TreeNode temp = head;
            TreeNode lastNode = null;
            while(temp != null)
            {
                Console.Write("\t" + temp.Value);
                if(temp.Right == null)
                {
                    lastNode = temp;
                }

                temp = temp.Right;
            }

            Console.WriteLine();

            //temp = head;
            while (lastNode != null)
            {
                Console.Write("\t" + lastNode.Value);
                lastNode = lastNode.Left;
            }

            Console.WriteLine();
        }
    }
}
