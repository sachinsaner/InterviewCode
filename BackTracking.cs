namespace CodingPractice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
   
    //General backtracking
    //https://leetcode.com/problems/subsets/discuss/27281/A-general-approach-to-backtracking-questions-in-Java-(Subsets-Permutations-Combination-Sum-Palindrome-Partitioning)

    public class BackTracking
    {
        //https://leetcode.com/problems/combination-sum-ii/description/
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
        public int FindTargetSumWays(int[] nums, int S)
        {
            int count = 0;
            var map = new Dictionary<string, int>();

            count = Util(nums, 0, S, 0, ref map);

            return count;
        }

        private int Util(int[] nums, int index, int S, int sum, ref Dictionary<string, int> map)
        {
            string str = index + "->" + sum;
            int add, sub = 0;
            if (map.ContainsKey(str))
            {
                return map[str];
            }
            if (index == nums.Length)
            {
                if (sum == S)
                {
                    return 1;
                }

                return 0;
            }
            else
            {
                add = Util(nums, index + 1, S, sum + nums[index], ref map);
                sub = Util(nums, index + 1, S, sum - nums[index], ref map);
                map.Add(str, add + sub);
            }

            return add + sub;
        }


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

        public List<List<int>> FindSubsequences(int[] nums)
        {
            List<List<int>> res = new List<List<int>>();

            helper(new LinkedList<int>(), 0, nums, res);

            return res;
        }

        private void helper(LinkedList<int> list, int index, int[] nums, List<List<int>> res)
        {
            if (list.Count > 1)
            {
                res.Add(new List<int>(list));
            }

            HashSet<int> used = new HashSet<int>();

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