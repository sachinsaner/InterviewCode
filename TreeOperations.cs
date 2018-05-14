namespace CodingPractice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeOperations
    {
        //https://leetcode.com/problems/longest-univalue-path/description/
        public int LongestUnivaluePath(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int maxCount = 0;
            int count = 0;

            Util(root, null, ref count, ref maxCount);

            return maxCount;
        }

        private void Util(TreeNode root, TreeNode prev, ref int count, ref int maxCount)
        {
            if (root == null)
            {
                return;
            }

            if (prev != null && root.Value == prev.Value)
            {
                count++;
            }
            else
            {
                count = 0;
            }

            Util(root.Left, root, ref count, ref maxCount);
            Util(root.Right, root, ref count, ref maxCount);

            maxCount = Math.Max(count, maxCount);
        }

        //https://leetcode.com/problems/path-sum-iii/description/
        //Find the number of paths that sum to a given value.
        //The path does not need to start or end at the root or a leaf, but it must go downwards(traveling only from parent nodes to child nodes).
        public IList<IList<int>> PathSum3(TreeNode root, int sum)
        {
            var result = new List<List<int>>();

            PathSumUtil(root, sum, result, new Stack<int>());

            return result.ToArray();
        }

        void PathSumUtil(TreeNode root, int sum, List<List<int>> result, Stack<int> stack)
        {
            if (root == null)
            {
                return;
            }

            stack.Push(root.Value);

            sum -= root.Value;

            if (sum == 0)
            {
                var s = new List<int>();
                s.AddRange(stack.ToList());
                s.Reverse();
                result.Add(s);
            }

            PathSumUtil(root.Left, sum, result, stack);
            PathSumUtil(root.Right, sum, result, stack);

            stack.Pop();
        }

        public TreeNode Util(int[] nums, int low, int high)
        {
            if (low > high)
            {
                return null;
            }

            int mid = (low + high) / 2;

            TreeNode root = new TreeNode(nums[mid]);

            root.Left = Util(nums, low, mid - 1);
            root.Right = Util(nums, mid + 1, high);

            return root;
        }
        
        /*
         * https://leetcode.com/problems/convert-bst-to-greater-tree/description/
         * Input: The root of a Binary Search Tree like this:
              5
            /   \
           2     13

           Output: The root of a Greater Tree like this:
             18
            /   \
          20     13
          */
        public void GreaterBST(TreeNode root)
        {
            if (root == null)
            {
                return;
            }

            GreaterBST(root.Right);
            GreaterBST(root.Left);

            if (root.Right != null)
            {
                root.Value += root.Right.Value;
            }

            if (root.Left != null)
            {
                root.Left.Value += root.Value;
            }
        }

        /*
         * https://leetcode.com/problems/symmetric-tree/description/
         * Given a binary tree, check whether it is a mirror of itself (ie, symmetric around its center).
           For example, this binary tree [1,2,2,3,4,4,3] is symmetric:
                1
               / \
              2   2
             / \ / \
            3  4 4  3
            But the following [1,2,2,null,3,null,3] is not:
                1
               / \
              2   2
               \   \
               3    3
        */
        public bool IsSymmetric(TreeNode root)
        {
            return Util(root.Left, root.Right);
        }

        bool Util(TreeNode root1, TreeNode root2)
        {
            if (root1 == null && root2 == null)
            {
                return true;
            }
            if (root1 == null || root2 == null)
            {
                return false;
            }

            return (root1.Value == root2.Value) && 
                Util(root1.Left, root2.Right) && 
                Util(root1.Right, root2.Left);
        }

        public TreeNode MergeTree2(TreeNode t1, TreeNode t2)
        {
            TreeNode t3 = new TreeNode(0);

            if (t1 == null && t2 == null)
            {
                return null;
            }

            if (t1 == null)
            {
                t3.Value = t2.Value;
            }

            if (t2 == null)
            {
                t3.Value = t1.Value;
            }

            if (t1 != null && t2 != null)
            {
                t3.Value = t1.Value + t2.Value;
            }

            t3.Left = MergeTree2(t1 == null ? null : t1.Left, t2 == null ? null : t2.Left);
            t3.Right = MergeTree2(t1 == null ? null : t1.Right, t2 == null ? null : t2.Right);

            return t3;
        }

       
       
        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var map = new Dictionary<int, List<int>>();

            LevelOrderUtil(root, 0, map);

            var res = new List<IList<int>>();
            foreach (var val in map.Values)
            {
                res.Add(val);
            }

            return res;
        }

        void LevelOrderUtil(TreeNode root, int depth, Dictionary<int, List<int>> map)
        {
            if (root == null)
            {
                return;
            }

            if (!map.TryGetValue(depth, out List<int> v))
            {
                map[depth] = new List<int>() { root.Value };
            }
            else
            {
                map[depth].Add(root.Value);
            }

            LevelOrderUtil(root.Left, depth + 1, map);
            LevelOrderUtil(root.Right, depth + 1, map);
        }

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

                //int rootval1 = prev == null ? -1 : prev.Value;

                //Console.WriteLine("root value: " + root.Value + " prev value: " + rootval1);

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
            
            PrintPreorder(root.Left, stack);
            PrintPreorder(root.Right, stack);

            Console.WriteLine();
            foreach (var item in stack)
            {
                Console.Write(item + "\t");
            }

            stack.Pop();
        }

        public void PrintAllPathMatchToSum(TreeNode root, int sum, Stack<int> stack)
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

                if (tempSum == sum)
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

            if (leftHeight == int.MinValue)
            {
                return int.MinValue; // Propagate error up
            }

            int rightHeight = checkHeight(root.Right);
            if (rightHeight == int.MinValue)
            {
                return int.MinValue; // Propagate error up
            }

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

        public void PrintRightViewOfTree(TreeNode root, int depth, Dictionary<int, int> dict)
        {
            if (root == null)
            {
                return;
            }

            dict[depth] = root.Value;

            PrintRightViewOfTree(root.Left, depth + 1, dict);
            PrintRightViewOfTree(root.Right, depth + 1, dict);
        }

        //https://leetcode.com/problems/binary-tree-longest-consecutive-sequence/description/
        /*
         * Given a binary tree, find the length of the longest consecutive sequence path.
           The path refers to any sequence of nodes from some starting node to any node in the tree along the parent-child connections.
           The longest consecutive path need to be from parent to child (cannot be the reverse).
           For example,
               1
                \
                 3
                / \
               2   4
                    \
                     5
            Longest consecutive sequence path is 3-4-5, so return 3.
               2
                \
                 3
                / 
               2    
              / 
             1
            Longest consecutive sequence path is 2-3,not3-2-1, so return 2.
        */
        public int MaxConsecutiveSequence(TreeNode root)
        {
            int maxSeq = 0;

            MaxConUtil(root, root, 0, ref maxSeq);

            return maxSeq;
        }

        private void MaxConUtil(TreeNode root, TreeNode prev, int currentMaxSeq, ref int maxSeq)
        {
            if (root == null)
            {
                return;
            }

            if (prev != null && prev.Value + 1 == root.Value)
            {
                currentMaxSeq++;

                maxSeq = Math.Max(maxSeq, currentMaxSeq);
            }
            else
            {
                currentMaxSeq = 0;
            }

            MaxConUtil(root.Left, root, currentMaxSeq, ref maxSeq);
            MaxConUtil(root.Right, root, currentMaxSeq, ref maxSeq);
        }

        public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
        {
            TreeNode result = null;
            while (root != null)
            {
                if (p.Value < root.Value)
                {
                    result = root;
                    root = root.Left;
                }
                else
                {
                    root = root.Right;
                }
            }

            return result;
        }

        public TreeNode InorderSuccessor2(TreeNode root, TreeNode p)
        {
            TreeNode result = null;

            InorderUtil(root, p, ref result);

            return result;
        }

        public void InorderUtil(TreeNode root, TreeNode p, ref TreeNode result)
        {
            if (root == null)
            {
                return;
            }

            if (root.Value > p.Value)
            {
                InorderUtil(root.Left, p, ref result);
            }
            else
            {
                InorderUtil(root.Right, p, ref result);
            }

            if (result == null && root.Value > p.Value)
            {
                result = root;
            }
            if (result != null && root.Value > p.Value && root.Value < result.Value)
            {
                result = root;
            }
        }

        //https://leetcode.com/problems/second-minimum-node-in-a-binary-tree/description/
        public int FindSecondMinimumValue2(TreeNode root)
        {
            int min1 = -1, min2 = int.MaxValue;

            if (root == null || root.Left == null && root.Right == null)
            {
                return min2;
            }

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            min1 = root.Value;

            while (q.Count > 0)
            {
                var node = q.Dequeue();

                if (node.Value < min2 && node.Value > min1)
                {
                    min2 = node.Value;
                }

                if (node.Left != null && node.Left.Value < min2)
                {
                    q.Enqueue(node.Left);
                }

                if (node.Right != null && node.Right.Value < min2)
                {
                    q.Enqueue(node.Left);
                }
            }

            return min2;
        }

        public int FindSecondMinimumValue(TreeNode root)
        {
            int min2 = -1;
            int min1 = -1;

            if (root == null || root.Left == null && root.Right == null)
            {
                return min2;
            }

            min1 = root.Value;

            if (root.Value != root.Left.Value)
            {
                min2 = root.Left.Value;
                root = root.Right;
            }
            else if (root.Value != root.Right.Value)
            {
                min2 = root.Right.Value;
                root = root.Left;
            }

            MinNodeUtil(root, ref min1, ref min2);

            return min2;
        }

        private void MinNodeUtil(TreeNode root, ref int min1, ref int min2)
        {
            if (root.Value < min2 && root.Value > min1)
            {
                min2 = root.Value;
                return;
            }

            if (root.Left == null && root.Right == null)
            {
                return;
            }

            MinNodeUtil(root.Left, ref min1, ref min2);
            MinNodeUtil(root.Right, ref min1, ref min2);
        }

        public string SerializeTree(TreeNode root)
        {
            string str = string.Empty;

            SerializeTreeUtil(root);

            return str;
        }

        private string SerializeTreeUtil(TreeNode root)
        {
            if(root == null)
            {
                return "#";
            }

            //string res = root.Value + "," + SerializeTreeUtil(root.Left) + "," + SerializeTreeUtil(root.Right);

           // string res = root.Value;
            string left = root.Value + "," + SerializeTreeUtil(root.Left);
            string right = left + "," + SerializeTreeUtil(root.Right);

            Console.WriteLine(right);

            return right;
        }
               
        public TreeNode BuildTree(List<string> input)
        {
            TreeNode root = null;

            if (input.Count > 0)
            {
                var val = input[0];
                input.RemoveAt(0);
                if (val != "null")
                {
                    root = new TreeNode(int.Parse(val));
                    root.Left = BuildTree(input);
                    root.Right = BuildTree(input);
                }
            }

            return root;
        }

        //https://leetcode.com/problems/count-of-smaller-numbers-after-self/
        /*
         * Given nums = [5, 2, 6, 1]
           [2, 1, 1, 0].
           
           num = [2, 0, 1]
           ans = [2, 0, 0]
         */
        public IList<int> CountSmaller(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return new List<int>();
            }

            int[] res = new int[nums.Length];

            TreeNode root = new TreeNode(nums[nums.Length - 1]);

            for (int i = nums.Length - 2; i >= 0; i--)
            {
                TreeNode temp = root;
                res[i] = Util(temp, nums[i]);
            }

            return res.ToList();
        }

        private int Util(TreeNode root, int val)
        {
            int count = 0;

            while (root != null)
            {
                //every time we move to right we add children of the left sub tree as they are smaller than current node
                //and + 1 to add the root node as well as its also smaller
                
                if (val > root.Value)
                {
                    count += (root.LeftChildCount + 1);

                    if (root.Right == null)
                    {
                        root.Right = new TreeNode(val);
                        break;
                    }
                    else
                    {
                        root = root.Right;
                    }
                }
                else
                {
                    //Increament the count as there is one more left child for this root
                    root.LeftChildCount++;

                    if (root.Left == null)
                    {
                        root.Left = new TreeNode(val);
                        break;
                    }
                    else
                    {
                        root = root.Left;
                    }
                }
            }

            return count;
        }

        /*
         * Given a binary tree, collect a tree's nodes as if you were doing this: Collect and remove all leaves, repeat until the tree is empty.

            Example:
            Given binary tree 
                      1
                     / \
                    2   3
                   / \     
                  4   5    
            Returns [4, 5, 3], [2], [1].
        */
        public IList<IList<int>> FindLeaves(TreeNode root)
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();

            Util(root, ref map);

            map.OrderBy(x => x.Key);

            var sol = new List<IList<int>>();

            foreach (var item in map)
            {
                sol.Add(item.Value);
            }

            return sol;
        }

        public int Util(TreeNode root, ref Dictionary<int, List<int>> map)
        {
            if (root == null)
            {
                return 0;
            }

            int left = Util(root.Left, ref map);
            int right = Util(root.Right, ref map);

            int res = Math.Max(left, right) + 1;

            if (map.ContainsKey(res))
            {
                map[res].Add(root.Value);
            }
            else
            {
               map.Add(res, new List<int>() { root.Value });
            }

            return res;
        }

		//https://leetcode.com/problems/most-frequent-subtree-sum/description/
        /*
         * Given the root of a tree, you are asked to find the most frequent subtree sum. The subtree sum of a node is defined as the sum of all the node values formed by the subtree rooted at that node (including the node itself). So what is the most frequent subtree sum value? If there is a tie, return all the values with the highest frequency in any order.

            Examples 1
            Input:

              5
             /  \
            2   -3
            return [2, -3, 4], since all the values happen only once, return all of them in any order.
            Examples 2
            Input:

              5
             /  \
            2   -5
            return [2], since 2 happens twice, however -5 only occur once.
        */
		public int[] FindFrequentTreeSum(TreeNode root)
        {
            var map = new Dictionary<int, int>();

            Util(root, ref map);
			//Todo: need to filter out elements
            return map.Select(x => x.Key).ToArray();
        }

        private int Util(TreeNode root, ref Dictionary<int, int> map)
        {
            if (root == null)
            {
                return 0;
            }

			int left = Util(root.Left, ref map);
			int right = Util(root.Right, ref map);

			int sum = left + right + root.Value;

            if (map.ContainsKey(sum))
            {
                map[sum]++;
            }
            else
            {
                map.Add(sum, 1);
            }

            return sum;
        }
    }
}
