namespace CodingPractice
{
    using System;
    using System.Collections.Generic;

    public class TreeOperations
    {
        //public static TreeNode prev;

        public TreeNode CreateBST(TreeNode root, TreeNode newNode)
        {
            if (root == null)
            {
                return root = newNode;
            }
            if (newNode.Value < root.Value)
            {
                root.Left = CreateBST(root.Left, newNode);
            }
            else
            {
                root.Right = CreateBST(root.Right, newNode);
            }

            return root;
        }

        public void CreateTree(ref TreeNode root, int[] nodes, ref int index)
        {
            if(nodes[index] == -1)
            {
                return ;
            }

            root = new TreeNode(nodes[index++]);
            CreateTree(ref root.Left, nodes, ref index);
            CreateTree(ref root.Right, nodes, ref index);

            //return root;
        }

        public TreeNode InsertIntoBST(TreeNode node, int data)
        {
            if (node == null)
                node = new TreeNode(data);
            else
            {
                if (data <= node.Value)
                    node.Left = InsertIntoBST(node.Left, data);
                else
                    node.Right = InsertIntoBST(node.Right, data);
            }
            return node;
        }

        public bool ISBST(TreeNode root, ref TreeNode prev)
        {
            if (root != null)
            {
                if (!ISBST(root.Left, ref prev))
                {
                    return false;
                }

                if (prev != null && root.Value < prev.Value)
                {
                    return false;
                }

                int rootval1 = prev == null ? -1 : prev.Value;

                Console.WriteLine("root value: " + root.Value + " prev value: " + rootval1);

                prev = root;

                if (!ISBST(root.Right, ref prev))
                {
                    return false;
                }
            }

            return true;
        }

        public void BSTToDLL(TreeNode root, ref TreeNode prev, ref TreeNode head)
        {
            if (root == null)
            {
                return;
            }

            BSTToDLL(root.Left, ref prev, ref head);

            if (prev != null && head == null)
            {
                head = prev;
            }

            if (prev != null)
            {
                prev.Right = root;
                root.Left = prev;
            }
            prev = root;

            BSTToDLL(root.Right, ref prev, ref head);
        }

        public void InorderIterative(TreeNode root)
        {
            if (root == null)
                return;

            Stack<TreeNode> stack = new Stack<TreeNode>();
            bool done = true;
            while (done)
            {
                if (root != null)
                {
                    stack.Push(root);
                    root = root.Left;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        root = stack.Pop();
                        Console.WriteLine(root.Value);

                        root = root.Right;
                    }
                    else
                    {
                        done = false;
                    }
                }
            }
        }

        public void PreOrder(TreeNode root, ref TreeNode prev)
        {
            if (root == null)
                return;

            PreOrder(root.Left, ref prev);

            Console.WriteLine("Current Value " + root.Value + " Prev Value:" + prev.Value);

            prev = root;

            PreOrder(root.Right, ref prev);

            
            //if (root != null)
            //{
            //    Console.WriteLine(root.Value);
            //}
        }

        public void BSTToDLL_Iterative(TreeNode root)
        {
            TreeNode prev = null;
            TreeNode head = null;

            if (root == null)
                return;

            var stack = new Stack<TreeNode>();
            bool done = true;
            while (done)
            {
                if (root != null)
                {
                    stack.Push(root);
                    root = root.Left;
                }
                else
                {
                    if (stack.Count != 0)
                    {
                        root = stack.Pop();

                        if (prev == null)
                        {
                            prev = root;
                            head = root;
                        }
                        else
                        {
                            prev.Right = root;
                            root.Left = prev;

                            prev = prev.Right;
                        }

                        root = root.Right;
                    }
                    else
                    {
                        done = false;
                    }
                }
            }

            while (head != null)
            {
                Console.WriteLine(head.Value);
                head = head.Right;
            }
        }

        public bool FindSwapedNodeInBST(TreeNode root, ref TreeNode prev, ref int count)
        {
            if (root != null)
            {
                if(!FindSwapedNodeInBST(root.Left, ref prev, ref count))
                {
                    return false;
                }

                if (prev != null)
                {
                    if (prev.Value > root.Value)
                    {
                        if (count == 0)
                        {
                            Console.WriteLine("Swapped Node " + prev.Value);
                            count++;
                        }
                        else
                        {
                            Console.WriteLine("Swapped Node " + root.Value);
                            return false;
                        }
                    }
                }

                prev = root;

                if (!FindSwapedNodeInBST(root.Right, ref prev, ref count))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
