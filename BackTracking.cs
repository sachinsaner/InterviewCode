namespace CodingPractice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
   
    //General backtracking
    //https://leetcode.com/problems/subsets/discuss/27281/A-general-approach-to-backtracking-questions-in-Java-(Subsets-Permutations-Combination-Sum-Palindrome-Partitioning)

	//https://medium.com/leetcode-patterns/leetcode-pattern-3-backtracking-5d9e5a03dc26

    public class BackTracking
    {
		public List<List<int>> Subsets(int[] nums)
        {
            var list = new List<List<int>>();

            Array.Sort(nums);

            backtrack(list, new List<int>(), nums, 0);

            return list;
        }

        private void backtrack(List<List<int>> list, List<int> tempList, int[] nums, int start)
        {
            list.Add(new List<int>(tempList));

            for (int i = start; i < nums.Length; i++)
            {
                tempList.Add(nums[i]);

                backtrack(list, tempList, nums, i + 1);

                tempList.RemoveAt(tempList.Count - 1);
            }
        }

        //https://leetcode.com/problems/combination-sum-ii/description/
        /*
         * Given a collection of candidate numbers (candidates) and a target number (target), 
         * find all unique combinations in candidates where the candidate numbers sums to target.

            Each number in candidates may only be used once in the combination.

            Note:

            All numbers (including target) will be positive integers.
            The solution set must not contain duplicate combinations.
            Example 1:

            Input: candidates = [10,1,2,7,6,1,5], target = 8,
            A solution set is:
            [
              [1, 7],
              [1, 2, 5],
              [2, 6],
              [1, 1, 6]
            ]
          */
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            List<IList<int>> res = new List<IList<int>>();

            Array.Sort(candidates);

            Util(candidates, ref res, new List<int>(), target, 0);

            return res;
        }

        private void Util(int[] nums, ref List<IList<int>> res, List<int> temp, int target, int index)
        {
            if (target < 0)
            {
                return;
            }
            if (target == 0)
            {
                res.Add(new List<int>(temp));
            }
            else
            {
                for (int i = index; i < nums.Length; i++)
                {
                    if (nums[i] > target)
                    {
                        continue;
                    }

                    //skip duplicates or already visited
                    if (i > index && nums[i] == nums[i - 1])
                    {
                        continue;
                    }
                   
                    temp.Add(nums[i]);
                    Util(nums, ref res, temp, target - nums[i], i + 1);
                    temp.RemoveAt(temp.Count - 1);
                }
            }
        }

        //https://leetcode.com/problems/target-sum/description/
        /*
         * You are given a list of non-negative integers, a1, a2, ..., an, and a target, S. Now you have 2 symbols + and -. For each integer, you should choose one from + and - as its new symbol.

            Find out how many ways to assign symbols to make sum of integers equal to target S.

            Example 1:
            Input: nums is [1, 1, 1, 1, 1], S is 3. 
            Output: 5
            Explanation: 

            -1+1+1+1+1 = 3
            +1-1+1+1+1 = 3
            +1+1-1+1+1 = 3
            +1+1+1-1+1 = 3
            +1+1+1+1-1 = 3

            There are 5 ways to assign symbols to make the sum of nums be target 3.
        */
        public int FindTargetSumWays(int[] nums, int S)
        {
            int count = 0;
            var map = new Dictionary<string, int>();

            count = Util(nums, 0, S, 0, ref map);

            return count;
        }

		private int Util(int[] nums, int index, int expectedSum, int currSum, ref Dictionary<string, int> map)
        {
            string str = index + "->" + currSum;
            int add, sub = 0;
            if (map.ContainsKey(str))
            {
                return map[str];
            }
            if (index == nums.Length)
            {
                if (currSum == expectedSum)
                {
                    return 1;
                }

                return 0;
            }
            else
            {
                add = Util(nums, index + 1, expectedSum, currSum + nums[index], ref map);
                sub = Util(nums, index + 1, expectedSum, currSum - nums[index], ref map);
                map.Add(str, add + sub);
            }

            return add + sub;
        }

		//https://leetcode.com/problems/increasing-subsequences/description/
        /*
         * Given an integer array, your task is to find all the different possible increasing subsequences of the given array, 
         * and the length of an increasing subsequence should be at least 2 .

            Example:
            Input: [4, 6, 7, 7]
            Output: [[4, 6], [4, 7], [4, 6, 7], [4, 6, 7, 7], [6, 7], [6, 7, 7], [7,7], [4,7,7]]
         */
        public List<List<int>> FindSubsequences(int[] nums)
        {
            var res = new List<List<int>>();

            helper(new LinkedList<int>(), 0, nums, res);
            
            return res;
        }

        private void helper(LinkedList<int> list, int index, int[] nums, List<List<int>> res)
        {
            if (list.Count > 1)
            {
                res.Add(new List<int>(list));
            }

            var used = new HashSet<int>();

            for (int i = index; i < nums.Length; i++)
            {
                if (used.Contains(nums[i]))
                {
                    continue;
                }

                if (list.Count == 0 || nums[i] >= list.LastOrDefault())
                {
                    used.Add(nums[i]);
                    list.AddLast(nums[i]);

                    helper(list, i + 1, nums, res);

                    list.RemoveLast();
                }
            }
        }

        public IList<string> GenerateParenthesis(int n)
        {
            var res = new List<string>();

            Util(ref res, string.Empty, 0, 0, n);

            return res;
        }

        private void Util(ref List<string> result, string str, int open, int close, int n)
        {
            if (open == n && close == n)
            {
                result.Add(str);
                return;
            }

            if (open < n)
            {
                Util(ref result, str + "(", open + 1, close, n);
            }

            if (close < open)
            {
                Util(ref result, str + ")", open, close + 1, n);
            }
        }

        private Dictionary<Tuple<int, int>, bool> memo = new Dictionary<Tuple<int, int>, bool>();

        public bool SubsetSumExists(int[] set, int n, int sum)
        {
            if (sum == 0)
            {
                return true;
            }

            if (n == 0 && sum != 0)
            {
                return false;
            }

            //if last element is greater than sum then ignore it 
            if (set[n - 1] > sum)
            {
                SubsetSumExists(set, n - 1, sum);
            }

            bool memoVal;
            if (!memo.TryGetValue(Tuple.Create(n, sum), out memoVal))
            {
                var res = SubsetSumExists(set, n - 1, sum) || SubsetSumExists(set, n - 1, sum - set[n - 1]);
                memo[Tuple.Create(n, sum)] = res;

                return res;
            }
            else
            {
                return memoVal;
            }
        }

        public void SubsetSumExists2(int[] set, int index, int currSum, int sum, int[] sol)
        {
            if (sum == currSum)
            {
                Console.WriteLine("\nSum found");
                for (int i = 0; i < sol.Length; i++)
                {
                    if (sol[i] == 1)
                    {
                        Console.WriteLine("  " + set[i]);
                    }
                }
            }

            if (index == set.Length)
            {
                return;
            }

            currSum += set[index];
            sol[index] = 1;
            SubsetSumExists2(set, index + 1, currSum, sum, sol);

            //ignore current element
            currSum -= set[index];
            sol[index] = 0;
            SubsetSumExists2(set, index + 1, currSum, sum, sol);
        }
    }
}