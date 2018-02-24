namespace CodingPractice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeOperations
    {
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

        public TreeNode BuildBSTFromPreorder(int[] nodes)
        {
            TreeNode root = new TreeNode(nodes[0]);

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            for (int i = 1; i < nodes.Length; i++)
            {
                TreeNode rt = null;
                if (nodes[i] <= stack.Peek().Value)
                {
                    rt = stack.Peek();
                    rt.Left = new TreeNode(nodes[i]);

                    stack.Push(rt.Left);
                }
                else
                {
                    while (stack.Count > 0 && nodes[i] > stack.Peek().Value)
                    {
                        rt = stack.Pop();
                    }
                    rt.Right = new TreeNode(nodes[i]);
                    stack.Push(rt.Right);
                }
            }

            return root;
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

        public void Preorder_Iterative_2(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);

            while (stack.Count > 0)
            {
                root = stack.Pop();

                Console.WriteLine(root.Value);

                if (root.Right != null)
                {
                    stack.Push(root.Right);
                }

                if (root.Left != null)
                {
                    stack.Push(root.Left);
                }
            }
        }

        public void Preorder_Iterative(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();

            while (root != null)
            {
                Console.WriteLine(root.Value);

                if (root.Right != null)
                {
                    stack.Push(root.Right);
                }
                root = root.Left;
                if (root == null && stack.Count > 0)
                {
                    root = stack.Pop();
                }
            }
        }

        public void PrintPreorder(TreeNode root, Stack<int> stack)
        {
            if (root == null)
            {
                return;
            }

            stack.Push(root.Value);
            //Console.WriteLine(root.Value);
            PrintPreorder(root.Left, stack);
            PrintPreorder(root.Right, stack);

            Console.WriteLine();
            foreach(var item in stack)
            {
                Console.Write(item + "\t");
            }

            stack.Pop();
        }

        public void PrintAllPathMatchToSum(TreeNode root,int sum, Stack<int> stack)
        {
            if (root == null)
            {
                return;
            }

            stack.Push(root.Value);

            PrintAllPathMatchToSum(root.Left, sum, stack);
            PrintAllPathMatchToSum(root.Right, sum, stack);


            int tempSum = 0;
            int index = 0;
            foreach (var item in stack)
            {
                tempSum += item;
                index++;

                if(tempSum == sum)
                {
                    Console.WriteLine();
                    for (int i = 0; i < index; i++)
                    {
                        Console.Write(stack.ElementAt(i) + "\t");
                    }    
                }
                
            }

            stack.Pop();
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
                if (!FindSwapedNodeInBST(root.Left, ref prev, ref count))
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

        public void PrintRootToNodePath(TreeNode root, ref List<int> path, int depth)
        {
            if (root == null)
            {
                return;
            }
            else
            {
                if (path.Count < depth + 1)
                {
                    path.Insert(depth, root.Value);
                }
                else
                {
                    path[depth] = root.Value;
                }
                if (root.Left == null && root.Right == null)
                {
                    foreach (var node in path)
                    {
                        Console.Write(node + "  ");
                    }
                    Console.WriteLine();
                }

                PrintRootToNodePath(root.Left, ref path, depth + 1);
                PrintRootToNodePath(root.Right, ref path, depth + 1);
            }
        }

        public void PrintRootToNodePath2(TreeNode root, Stack<int> stack)
        {
            if (root == null)
            {
                return;
            }
            else
            {
                stack.Push(root.Value);
                if (root.Left == null && root.Right == null)
                {
                    foreach (var node in stack)
                    {
                        Console.Write(node + "  ");
                    }
                    Console.WriteLine();
                }

                PrintRootToNodePath2(root.Left, stack);
                PrintRootToNodePath2(root.Right, stack);
                stack.Pop();
            }
        }

        public TreeNode FindLCA(TreeNode root, int n1, int n2)
        {
            if (root == null)
                return null;

            if (root.Value == n1 || root.Value == n2)
            {
                return root;
            }

            TreeNode left = FindLCA(root.Left, n1, n2);
            TreeNode right = FindLCA(root.Right, n1, n2);

            if (left != null && right != null)
            {
                return root;
            }

            return left != null ? left : right;
        }

        private int checkHeight(TreeNode root)
        {
            if (root == null)
            {
                return -1;
            }
            int leftHeight = checkHeight(root.Left);
            if (leftHeight == int.MinValue) return int.MinValue; // Propagate error up

            int rightHeight = checkHeight(root.Right);
            if (rightHeight == int.MinValue) return int.MinValue; // Propagate error up

            int heightDiff = leftHeight - rightHeight;
            if (Math.Abs(heightDiff) > 1)
            {
                return int.MinValue; // Found error -> pass it back
            }
            else
            {
                return Math.Max(leftHeight, rightHeight) + 1;
            }
        }

        public bool IsBalanced(TreeNode root)
        {
            return checkHeight(root) != int.MinValue;
        }

        public TreeNode PrintNextLeafNode(TreeNode root, int leafNode, ref bool hasLeafFound)
        {
            if (root == null)
            {
                return null;
            }
            else
            {
                TreeNode left = null, right = null;

                if (root.Left == null && root.Right == null)
                {
                    if (hasLeafFound)
                    {
                        return root;
                    }
                    if (root.Value == leafNode)
                    {
                        hasLeafFound = true;
                    }
                }
                left = PrintNextLeafNode(root.Left, leafNode, ref hasLeafFound);
                if (left != null)
                {
                    return left;
                }
                right = PrintNextLeafNode(root.Right, leafNode, ref hasLeafFound);
                if (right != null)
                {
                    return right;
                }

                return null;
            }
        }

        public void BuildTrie(string[] words, TrieNode root)
        {
            TrieNode curr = root;

            foreach(var word in words)
            {
                foreach(var chr in word.ToCharArray())
                {
                    int index = chr - 'a';

                    if(curr.Children[index] == null)
                    {
                        curr.Children[index] = new TrieNode() { Chr = chr };
                    }

                    curr = curr.Children[index];
                }

                curr.IsWord = true;
                curr = root;
            }
         }

        public bool IsPresentInTrie(TrieNode root, string word)
        {
            TrieNode curr = root;

            foreach(char chr in word)
            {
                int index = chr - 'a';

                if (curr.Children[index] == null)
                {
                    return false;
                }

                curr = curr.Children[index];
            }

            return true;
        }

        public void PrintRightViewOfTree(TreeNode root, int depth, Dictionary<int,int> dict)
        {
            if(root == null)
            {
                return;
            }

            dict[depth] = root.Value;

            PrintRightViewOfTree(root.Left, depth + 1, dict);
            PrintRightViewOfTree(root.Right, depth + 1, dict);
        }
    }
}
