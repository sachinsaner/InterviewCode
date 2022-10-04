namespace CodingPractice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeOperations
    {

        public bool IsValidBST(TreeNode root)
        {
            bool res = true;
            TreeNode prev = null;
            res = InOrder(root, ref prev);
            Console.WriteLine(res);
            return res;
        }

        bool InOrder(TreeNode root, ref TreeNode prev)
        {
            if (root != null)
            {
                bool left = InOrder(root.left, ref prev);

                if (left == false || prev != null && root.val <= prev.val)
                {
                    return false;
                }

                prev = root;

                if (!InOrder(root.right, ref prev))
                {
                    return false;
                }
            }

            return true;

        }



        /* 
		 * Find the sum of all left leaves in a given binary tree.

            Example:

                3
               / \
              9  20
                /  \
               15   7
           There are two left leaves in the binary tree, with values 9 and 15 respectively. Return 24.
        */
        public int SumOfLeftLeaves(TreeNode root)
        {
            int sum = 0;
            Util(root, false, ref sum);

            return sum;
        }

        private void Util(TreeNode root, bool isLeft, ref int sum)
        {
            if (root == null)
            {
                return;
            }

            if (isLeft && root.left == null && root.right == null)
            {
                sum += root.val;
            }

            Util(root.left, true, ref sum);
            Util(root.right, false, ref sum);
        }

        /*
         * Given a non-empty binary tree, find the maximum path sum.

            For this problem, a path is defined as any sequence of nodes from some 
            starting node to any node in the tree along the parent-child connections. 
            The path must contain at least one node and does not need to go through the root.

            Example 1:

            Input: [1,2,3]

                   1
                  / \
                 2   3

            Output: 6
            Example 2:

            Input: [-10,9,20,null,null,15,7]

               -10
               / \
              9  20
                /  \
               15   7

            Output: 42
        */
		public int MaxPathSum(TreeNode root)
        {
            int max = Int32.MinValue;
            Util(root, ref max);
            return max;
        }

        private int Util(TreeNode root, ref int maxsum)
        {
            if (root == null)
            {
                return 0;
            }

            /****** IMP for negative number we are returning 0 */

            int left = Math.Max(Util(root.left, ref maxsum), 0);
            int right = Math.Max(Util(root.right, ref maxsum), 0);

            /* for left and right subtrees for -tive sum we are returning 0
             * hence if whole tree is -tive then root will be the only one
             * with highest value 
             */

            maxsum = Math.Max(maxsum, left + right + root.val);

            //We can only pick left or right subtree becasue node can be in the path only once
            //hence Math.Max(left, right)
            return Math.Max(left, right) + root.val;
        }

        //https://leetcode.com/problems/longest-univalue-path/description/
        /*  Given a binary tree, find the length of the longest path where each node in the path has the same value. 
		 *  This path may or may not pass through the root.
            Note: The length of path between two nodes is represented by the number of edges between them.
            Example 1:
            Input:

                          5
                         / \
                        4   5
                       / \   \
                      1   1   5
            Output: 2
            
            Example 2:
            Input:
                          1
                         / \
                        4   5
                       / \   \
                      4   4   5
            Output: 2
        */
        public int LongestUnivaluePath(TreeNode root)
        {

            int maxCount = 0;
            Util_2(root, ref maxCount);
            return maxCount;
        }

        public int Util_2(TreeNode root, ref int maxCount)
        {
            if (root == null)
            {
                return 0;
            }

            if (root.left == null && root.right == null)
            {
                return 1;
            }

            int left = Util(root.left, ref maxCount);
            int right = Util(root.right, ref maxCount);

            left = (root.left != null && root.val == root.left.val) ? left : 0;
            right = (root.right != null && root.val == root.right.val) ? right : 0;

            maxCount = Math.Max(maxCount, (left + right));

            return Math.Max(left, right) + 1;
        }

        /*https://leetcode.com/problems/path-sum-iii/description/
            Find the number of paths that sum to a given value.
            The path does not need to start or end at the root or a leaf, 
            but it must go downwards(traveling only from parent nodes to child nodes).
    		root = [10,5,-3,3,2,null,11,3,-2,null,1], sum = 8

                  10
                 /  \
                5   -3
               / \    \
              3   2   11
             / \   \
            3  -2   1

            Return 3. The paths that sum to 8 are:

            1.  5 -> 3
            2.  5 -> 2 -> 1
            3. -3 -> 11
        */

        public int PathSum3(TreeNode root, int targetSum)
        {

            Dictionary<int, int> map = new Dictionary<int, int>();
            int count = 0;
            int currSum = 0;

            Util3(root, targetSum, ref count, currSum, map);

            return count;


        }

        private void Util3(TreeNode root, int targetSum, ref int count, int currSum, Dictionary<int, int> map)
        {
            if (root == null)
            {
                return;
            }

            currSum += root.val;

            if (currSum == targetSum)
            {
                count++;
            }

            if (map.ContainsKey(currSum - targetSum))
            {
                count += map[currSum - targetSum];
            }

            if (map.ContainsKey(currSum))
            {
                map[currSum]++;
            }
            else
            {
                map[currSum] = 1;
            }

            Util3(root.left, targetSum, ref count, currSum, map);
            Util3(root.right, targetSum, ref count, currSum, map);

            //currSum -= root.val;
            map[currSum]--;

        }

        public int PathSum3_Stack(TreeNode root, int targetSum)
        {
            Stack<int> s = new Stack<int>();
            int count = 0;

            Util3_Stack(root, targetSum, ref count, s);

            return count;
        }

        private void Util3_Stack(TreeNode root, int targetSum, ref int count, Stack<int> s)
        {
            if (root == null)
            {
                return;
            }

            s.Push(root.val);

            Util3_Stack(root.left, targetSum, ref count, s);
            Util3_Stack(root.right, targetSum, ref count, s);

            int sum = 0;
            for (int i = 0; i < s.Count; i++)
            {
                sum += s.ElementAt(i);
                if (sum == targetSum)
                {
                    count++;
                }
            }

            s.Pop();
        }




        //Print all the paths from root, with a specified sum in Binary tree
        // IMP: Path starts from the root
        public IList<IList<int>> PathSumFromRoot(TreeNode root, int sum)
        {
            var result = new List<List<int>>();

            PathSumFromRootUtil(root, sum, result, new Stack<int>());

            return result.ToArray();
        }

        void PathSumFromRootUtil(TreeNode root, int sum, List<List<int>> result, Stack<int> stack)
        {
            if (root == null)
            {
                return;
            }

            stack.Push(root.val);

            sum -= root.val;

            if (sum == 0)
            {
                var s = new List<int>();
                s.AddRange(stack.ToList());
                s.Reverse();
                result.Add(s);
            }

            PathSumFromRootUtil(root.left, sum, result, stack);
            PathSumFromRootUtil(root.right, sum, result, stack);

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

            root.left = Util(nums, low, mid - 1);
            root.right = Util(nums, mid + 1, high);

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

            GreaterBST(root.right);
            GreaterBST(root.left);

            if (root.right != null)
            {
                root.val += root.right.val;
            }

            if (root.left != null)
            {
                root.left.val += root.val;
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
            return Util(root.left, root.right);
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

            return (root1.val == root2.val) && 
                Util(root1.left, root2.right) && 
                Util(root1.right, root2.left);
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
                t3.val = t2.val;
            }

            if (t2 == null)
            {
                t3.val = t1.val;
            }

            if (t1 != null && t2 != null)
            {
                t3.val = t1.val + t2.val;
            }

            t3.left = MergeTree2(t1 == null ? null : t1.left, t2 == null ? null : t2.left);
            t3.right = MergeTree2(t1 == null ? null : t1.right, t2 == null ? null : t2.right);

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
                map[depth] = new List<int>() { root.val };
            }
            else
            {
                map[depth].Add(root.val);
            }

            LevelOrderUtil(root.left, depth + 1, map);
            LevelOrderUtil(root.right, depth + 1, map);
        }

        public TreeNode CreateBST(TreeNode root, TreeNode newNode)
        {
            if (root == null)
            {
                return root = newNode;
            }
            if (newNode.val < root.val)
            {
                root.left = CreateBST(root.left, newNode);
            }
            else
            {
                root.right = CreateBST(root.right, newNode);
            }

            return root;
        }

        public TreeNode BuildBSTFromPreorder(int[] nodes)
        {
            TreeNode root = new TreeNode(nodes[0]);

            var stack = new Stack<TreeNode>();
            stack.Push(root);

            for (int i = 1; i < nodes.Length; i++)
            {
                TreeNode rt = null;
                if (nodes[i] <= stack.Peek().val)
                {
                    rt = stack.Peek();
                    rt.left = new TreeNode(nodes[i]);

                    stack.Push(rt.left);
                }
                else
                {
                    while (stack.Count > 0 && nodes[i] > stack.Peek().val)
                    {
                        rt = stack.Pop();
                    }
                    rt.right = new TreeNode(nodes[i]);
                    stack.Push(rt.right);
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
                if (data <= node.val)
                    node.left = InsertIntoBST(node.left, data);
                else
                    node.right = InsertIntoBST(node.right, data);
            }
            return node;
        }

        public bool ISBST(TreeNode root, ref TreeNode prev)
        {
            if (root != null)
            {
                if (!ISBST(root.left, ref prev))
                {
                    return false;
                }

                if (prev != null && root.val <= prev.val)
                {
                    return false;
                }

                //int rootval1 = prev == null ? -1 : prev.Value;

                //Console.WriteLine("root value: " + root.Value + " prev value: " + rootval1);

                prev = root;

                if (!ISBST(root.right, ref prev))
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

            BSTToDLL(root.left, ref prev, ref head);

            if (prev != null && head == null)
            {
                head = prev;
            }

            if (prev != null)
            {
                prev.right = root;
                root.left = prev;
            }
            prev = root;

            BSTToDLL(root.right, ref prev, ref head);
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
                    root = root.left;
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
                            prev.right = root;
                            root.left = prev;

                            prev = prev.right;
                        }

                        root = root.right;
                    }
                    else
                    {
                        done = false;
                    }
                }
            }

            while (head != null)
            {
                Console.WriteLine(head.val);
                head = head.right;
            }
        }
        
        public void InorderIterative(TreeNode root)
        {
            if (root == null)
                return;

            var stack = new Stack<TreeNode>();
            bool done = true;
            while (done)
            {
                if (root != null)
                {
                    stack.Push(root);
                    root = root.left;
                }
                else
                {
					// IMP check stack count to break the loop
                    if (stack.Count != 0)
                    {
                        root = stack.Pop();
                        Console.WriteLine(root.val);

                        root = root.right;
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

                Console.WriteLine(root.val);

                if (root.right != null)
                {
                    stack.Push(root.right);
                }

                if (root.left != null)
                {
                    stack.Push(root.left);
                }
            }
        }

        public void Preorder_Iterative(TreeNode root)
        {
            Stack<TreeNode> stack = new Stack<TreeNode>();

            while (root != null)
            {
                Console.WriteLine(root.val);

                if (root.right != null)
                {
                    stack.Push(root.right);
                }
                root = root.left;
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

            stack.Push(root.val);
            
            PrintPreorder(root.left, stack);
            PrintPreorder(root.right, stack);

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

            stack.Push(root.val);

            PrintAllPathMatchToSum(root.left, sum, stack);
            PrintAllPathMatchToSum(root.right, sum, stack);

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

        public bool FindSwapedNodeInBST(TreeNode root, ref TreeNode prev, ref int count)
        {
            if (root != null)
            {
                if (!FindSwapedNodeInBST(root.left, ref prev, ref count))
                {
                    return false;
                }

                if (prev != null)
                {
                    if (prev.val > root.val)
                    {
                        if (count == 0)
                        {
                            Console.WriteLine("Swapped Node " + prev.val);
                            count++;
                        }
                        else
                        {
                            Console.WriteLine("Swapped Node " + root.val);
                            return false;
                        }
                    }
                }

                prev = root;

                if (!FindSwapedNodeInBST(root.right, ref prev, ref count))
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
                    path.Insert(depth, root.val);
                }
                else
                {
                    path[depth] = root.val;
                }
                if (root.left == null && root.right == null)
                {
                    foreach (var node in path)
                    {
                        Console.Write(node + "  ");
                    }
                    Console.WriteLine();
                }

                PrintRootToNodePath(root.left, ref path, depth + 1);
                PrintRootToNodePath(root.right, ref path, depth + 1);
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
                stack.Push(root.val);
                if (root.left == null && root.right == null)
                {
                    foreach (var node in stack)
                    {
                        Console.Write(node + "  ");
                    }
                    Console.WriteLine();
                }

                PrintRootToNodePath2(root.left, stack);
                PrintRootToNodePath2(root.right, stack);
                stack.Pop();
            }
        }

        public TreeNode FindLCA(TreeNode root, int n1, int n2)
        {
            if (root == null)
                return null;

            if (root.val == n1 || root.val == n2)
            {
                return root;
            }

            TreeNode left = FindLCA(root.left, n1, n2);
            TreeNode right = FindLCA(root.right, n1, n2);

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

            int leftHeight = checkHeight(root.left);

            if (leftHeight == int.MinValue)
            {
                return int.MinValue; // Propagate error up
            }

            int rightHeight = checkHeight(root.right);
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

                if (root.left == null && root.right == null)
                {
                    if (hasLeafFound)
                    {
                        return root;
                    }
                    if (root.val == leafNode)
                    {
                        hasLeafFound = true;
                    }
                }

                left = PrintNextLeafNode(root.left, leafNode, ref hasLeafFound);
                if (left != null)
                {
                    return left;
                }

                right = PrintNextLeafNode(root.right, leafNode, ref hasLeafFound);
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

            dict[depth] = root.val;

            PrintRightViewOfTree(root.left, depth + 1, dict);
            PrintRightViewOfTree(root.right, depth + 1, dict);
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

            if (prev != null && prev.val + 1 == root.val)
            {
                currentMaxSeq++;

                maxSeq = Math.Max(maxSeq, currentMaxSeq);
            }
            else
            {
                currentMaxSeq = 0;
            }

            MaxConUtil(root.left, root, currentMaxSeq, ref maxSeq);
            MaxConUtil(root.right, root, currentMaxSeq, ref maxSeq);
        }


        /*
         * Example 1:

            Input: root = [2,1,3], p = 1

              2
             / \
            1   3

            Output: 2
            Example 2:

            Input: root = [5,3,6,2,4,null,null,1], p = 6

                  5
                 / \
                3   6
               / \
              2   4
             /   
            1

            Output: null
        */
        public TreeNode InorderSuccessor(TreeNode root, int p)
        {
            TreeNode result = null;
            while (root != null)
            {
				//every time we move to the left subtree we save the root because if the successor
                //isnt present in left subtree then it must be the root
				if (root.val > p)
                {
                    result = root;
                    root = root.left;
                }
                else
                {
                    root = root.right;
                }
            }

            return result;
        }

        public TreeNode InorderSuccessor2(TreeNode root, int p)
        {
            TreeNode result = null;

            InorderUtil(root, p, ref result);

            return result;
        }

        public void InorderUtil(TreeNode root, int p, ref TreeNode result)
        {
            if (root == null)
            {
                return;
            }

            if (root.val > p)
            {
                InorderUtil(root.left, p, ref result);
            }
            else
            {
                InorderUtil(root.right, p, ref result);
            }
            //while returning from subtree check if root is greater than given P
            if (result == null && root.val > p)
            {
                result = root;
            }
            if (result != null && root.val > p && root.val < result.val)
            {
                result = root;
            }
        }

        //https://leetcode.com/problems/second-minimum-node-in-a-binary-tree/description/
        /*
         * Example 1:
            Input: 
                2
               / \
              2   5
                 / \
                5   7

            Output: 5
            Explanation: The smallest value is 2, the second smallest value is 5.
            Example 2:
            Input: 
                2
               / \
              2   2

            Output: -1
            Explanation: The smallest value is 2, but there isn't any second smallest value.
        */
        public int FindSecondMinimumValue2(TreeNode root)
        {
            int min1 = -1, min2 = int.MaxValue;

            if (root == null || root.left == null && root.right == null)
            {
                return min2;
            }

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            min1 = root.val;

            foreach(var item in q)
            {
                
            }
            while (q.Count > 0)
            {
                var node = q.Dequeue();

                if (node.val < min2 && node.val > min1)
                {
                    min2 = node.val;
                }

                if (node.left != null && node.left.val < min2)
                {
                    q.Enqueue(node.left);
                }

                if (node.right != null && node.right.val < min2)
                {
                    q.Enqueue(node.left);
                }
            }

            return min2;
        }

        public int FindSecondMinimumValue(TreeNode root)
        {
            int min2 = -1;
            int min1 = -1;

            if (root == null || root.left == null && root.right == null)
            {
                return min2;
            }

            min1 = root.val;

            if (root.val != root.left.val)
            {
                min2 = root.left.val;
                root = root.right;
            }
            else if (root.val != root.right.val)
            {
                min2 = root.right.val;
                root = root.left;
            }

            MinNodeUtil(root, ref min1, ref min2);

            return min2;
        }

        private void MinNodeUtil(TreeNode root, ref int min1, ref int min2)
        {
            if (root.val < min2 && root.val > min1)
            {
                min2 = root.val;
                return;
            }

            if (root.left == null && root.right == null)
            {
                return;
            }

            MinNodeUtil(root.left, ref min1, ref min2);
            MinNodeUtil(root.right, ref min1, ref min2);
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
            string left = root.val + "," + SerializeTreeUtil(root.left);
            string right = left + "," + SerializeTreeUtil(root.right);

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
                    root.left = BuildTree(input);
                    root.right = BuildTree(input);
                }
            }

            return root;
        }

        //https://leetcode.com/problems/count-of-smaller-numbers-after-self/
        /*
         * You are given an integer array nums and you have to return a new counts array. 
         * The counts array has the property where counts[i] is the number of smaller elements to the right of nums[i].
         * 
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
                
                if (val > root.val)
                {
                    count += (root.LeftChildCount + 1);

                    if (root.right == null)
                    {
                        root.right = new TreeNode(val);
                        break;
                    }
                    else
                    {
                        root = root.right;
                    }
                }
                else
                {
                    //Increament the count as there is one more left child for this root
                    root.LeftChildCount++;

                    if (root.left == null)
                    {
                        root.left = new TreeNode(val);
                        break;
                    }
                    else
                    {
                        root = root.left;
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

            int left = Util(root.left, ref map);
            int right = Util(root.right, ref map);

            int res = Math.Max(left, right) + 1;

            if (map.ContainsKey(res))
            {
                map[res].Add(root.val);
            }
            else
            {
               map.Add(res, new List<int>() { root.val });
            }

            return res;
        }

		//https://leetcode.com/problems/most-frequent-subtree-sum/description/
        /*
         * Given the root of a tree, you are asked to find the most frequent subtree sum. 
         * The subtree sum of a node is defined as the sum of all the node values formed by the subtree rooted at that node (including the node itself). 
         * So what is the most frequent subtree sum value? If there is a tie, return all the values with the highest frequency in any order.

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

			int left = Util(root.left, ref map);
			int right = Util(root.right, ref map);

			int sum = left + right + root.val;

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

        int finalSum = 0;
        public int SumNumbers(TreeNode root)
        {
            Util_SUM(root, 0);

            return finalSum;
        }

        void Util_SUM(TreeNode root, int num)
        {
            if (root == null)
            {
                //finalSum += num;
                return;
            }
            //num = (num * 10)
            Util_SUM(root.left, ((num * 10) + root.val));
            Util_SUM(root.right, ((num * 10) + root.val));

            if (root.left == null && root.right == null)
            {
                finalSum += ((num * 10) + root.val);
            }

        }


        Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
        int maxLevel = Int32.MinValue;
        int minLevel = Int32.MaxValue;

        public IList<IList<int>> VerticalOrder(TreeNode root)
        {
            var res = new List<IList<int>>();
            Util_VerticalOrder(root, 0);

            for (int i = minLevel; i <= maxLevel; i++)
            {
                res.Add(map[i]);
            }

            return res;
        }

        void Util_VerticalOrder(TreeNode root, int level)
        {
            if (root == null)
            {
                return;
            }

            if (map.ContainsKey(level))
            {
                map[level].Add(root.val);
            }
            else
            {
                map[level] = new List<int>() { root.val };
            }

            Util_VerticalOrder(root.left, level - 1);
            Util_VerticalOrder(root.right, level + 1);                   

            maxLevel = Math.Max(maxLevel, level);
            minLevel = Math.Min(minLevel, level);
        }

        public void Flatten(TreeNode root)
        {
            TreeNode prev = null;
            //Util(root, ref prev);
            Util_Flatten(root);
        }

        TreeNode Util_Flatten(TreeNode root)
        {
            if (root == null)
            {
                return null;
            }

            if (root.left == null && root.right == null)
            {
                return root;
            }

            TreeNode leftT = Util_Flatten(root.left);
            TreeNode rightT = Util_Flatten(root.right);

            if (leftT != null)
            {

                leftT.right = root.right;
                root.right = root.left;
                root.left = null;
            }

            return rightT == null ? leftT : rightT;
        }

        private void Util(TreeNode root, ref TreeNode prev)
        {
            if (root == null)
            {
                return;
            }

            Util(root.right, ref prev);
            Util(root.left, ref prev);

            root.right = prev;
            root.left = null;

            prev = root;

        }

        public int WidthOfBinaryTree(TreeNode root)
        {

            Queue<Tuple<TreeNode, int>> q = new Queue<Tuple<TreeNode, int>>();

            q.Enqueue(Tuple.Create(root, 0));

            int maxWidth = 0;
            Tuple<TreeNode, int> qElm = null;
            while (q.Count > 0)
            {
                int size = q.Count;
                var start = q.Peek();

                for (int i = 0; i < size; i++)
                {
                    qElm = q.Dequeue();
                    var node = qElm.Item1;

                    if (node.left != null)
                    {
                        q.Enqueue(Tuple.Create(node.left, 2 * qElm.Item2));
                    }
                    if (node.right != null)
                    {
                        q.Enqueue(Tuple.Create(node.right, (2 * qElm.Item2) + 1));
                    }                                    
                }

                var width = (qElm.Item2 - start.Item2) + 1;

                maxWidth = Math.Max(width, maxWidth);

            }

            return maxWidth;
        }

    }
}
