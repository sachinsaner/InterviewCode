using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class TopologicalSort
    {
        List<List<int>> res = new List<List<int>>();

        void TopologicalSortUtil(int V, Dictionary<int, List<int>> adjMap, HashSet<int> visited, Dictionary<int, int> indegree, Stack<int> stack)
        {
            bool flag = false;
            for (int i = 0; i < V; i++)
            {
                if (!visited.Contains(i) && indegree[i] == 0)
                {
                    visited.Add(i);
                    stack.Push(i);

                    if (adjMap.ContainsKey(i))
                    {
                        foreach (var item in adjMap[i])
                        {
                            indegree[item]--;
                        }
                    }

                    TopologicalSortUtil(V, adjMap, visited, indegree, stack);

                    visited.Remove(i);
                    stack.Pop();
                    if (adjMap.ContainsKey(i))
                    {
                        foreach (var adj in adjMap[i])
                        {
                            indegree[adj]++;
                        }
                    }
                    flag = true;
                }
            }

            if (!flag)
            {
                res.Add(new List<int>(stack.ToArray()));
            }

        }

        public List<List<int>> FindOrder(int numCourses, int[][] prerequisites)
        {
            Dictionary<int, List<int>> adjMap = new Dictionary<int, List<int>>();
            Dictionary<int, int> indegree = new Dictionary<int, int>();
            Stack<int> stack = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();

            for (int i = 0; i < numCourses; i++)
            {
                indegree.Add(i, 0);
            }

            for (int i = 0; i < prerequisites.GetLength(0); i++)
            {
                int source = prerequisites[i][1];
                int dest = prerequisites[i][0];

                if (adjMap.ContainsKey(source))
                {
                    adjMap[source].Add(dest);
                }
                else
                {
                    adjMap.Add(source, new List<int>() { dest });
                }

                if (indegree.ContainsKey(dest))
                {
                    indegree[dest]++;
                }
            }

            TopologicalSortUtil(numCourses, adjMap, visited, indegree, stack);
            return res;
        }

        //public string AlienOrder(string[] words)
        //{
        //    Dictionary<char, int> indegree = new Dictionary<char, int>();
        //    Dictionary<char, HashSet<char>> adjMap = new Dictionary<char, HashSet<char>>();

        //    foreach (var w in words)
        //    {
        //        foreach (var c in w)
        //        {
        //            if (!indegree.ContainsKey(c))
        //            {
        //                indegree.Add(c, 0);
        //            }
        //        }
        //    }

        //    for (int i = 0; i < words.Length - 1; i++)
        //    {
        //        var w1 = words[i];
        //        var w2 = words[i + 1];

        //        for (int j = 0; j < Math.Min(w1.Length, w2.Length); j++)
        //        {
        //            var c1 = w1[j];
        //            var c2 = w2[j];
        //            if (c1 != c2)
        //            {
        //                if (adjMap.ContainsKey(c1))
        //                {
        //                    if (!adjMap[c1].Contains(c2))
        //                    {
        //                        adjMap[c1].Add(c2);
        //                        indegree[c2]++;
        //                    }
        //                }
        //                else
        //                {
        //                    adjMap.Add(c1, new HashSet<char>() { c2 });
        //                    indegree[c2]++;
        //                }
        //            }
        //        }
        //    }

        //    Queue<char> q = new Queue<char>();

        //    foreach (var item in indegree)
        //    {
        //        if (item.Value == 0)
        //        {
        //            q.Enqueue(item.Key);
        //        }
        //    }

        //    string res = "";
        //    Console.WriteLine(q.Count);
        //    while (q.Count > 0)
        //    {
        //        var c = q.Dequeue();
        //        res = res + c;

        //        if (adjMap.ContainsKey(c))
        //        {
        //            foreach (var v in adjMap[c])
        //            {
        //                indegree[v]--;

        //                if (indegree[v] == 0)
        //                {
        //                    q.Enqueue(v);
        //                }
        //            }
        //        }
        //    }
        //    Console.WriteLine(res);

        //    return indegree.Count == res.Length ? res : "";

        //}

        //https://leetcode.com/problems/sequence-reconstruction/description/
        /*
         * Check whether the original sequence org can be uniquely reconstructed from the sequences in seqs. 
         * The org sequence is a permutation of the integers from 1 to n, with 1 ≤ n ≤ 104. 
         * Reconstruction means building a shortest common supersequence of the sequences in seqs 
         * (i.e., a shortest sequence so that all sequences in seqs are subsequences of it). 
         * Determine whether there is only one sequence that can be reconstructed from seqs and it is the org sequence.

            Example 1:

            Input:
            org: [1,2,3], seqs: [[1,2],[1,3]]

            Output:
            false

            Explanation:
            [1,2,3] is not the only one sequence that can be reconstructed, because [1,3,2] is also a valid sequence that can be reconstructed.
        */
        public bool SequenceReconstruction(int[] org, IList<IList<int>> seqs)
        {
            if (seqs == null || seqs.Count == 0)
            {
                return false;
            }

            Dictionary<int, int> indegree = new Dictionary<int, int>();
            var map = new Dictionary<int, HashSet<int>>();

            foreach (var o in org)
            {
                indegree.Add(o, 0);
            }

            foreach (var s in seqs)
            {
                if (s.Count == 2)
                {
                    if (map.ContainsKey(s[0]))
                    {
                        map[s[0]].Add(s[1]);
                    }
                    else
                    {
                        map.Add(s[0], new HashSet<int> { s[1] });
                    }
                    indegree[s[1]]++;
                }
            }

            var q = new Queue<int>();

            foreach (var item in indegree)
            {
                if (item.Value == 0)
                {
                    q.Enqueue(item.Key);
                }
            }

            var result = new List<int>();

            while (q.Count > 0)
            {
                int item = q.Dequeue();
                result.Add(item);

                int count = 0;
                if (map.ContainsKey(item))
                {
                    foreach (var v in map[item])
                    {
                        indegree[v]--;

                        if (indegree[v] == 0)
                        {
                            count++;
                            q.Enqueue(v);
                        }

                        if (count > 1)
                        {
                            return false;
                        }
                    }
                }
            }

            return result.Count == org.Length;
        }

        //https://leetcode.com/problems/alien-dictionary/description/
        //https://leetcode.com/problems/alien-dictionary/discuss/70119/Java-AC-solution-using-BFS/72252
        //Topological sort
        /*
         * There is a new alien language which uses the latin alphabet. 
         * However, the order among letters are unknown to you. You receive a list of non-empty words from the dictionary,
         * where words are sorted lexicographically by the rules of this new language. Derive the order of letters in this language.

            Example 1:

            Input:
            [
              "wrt",
              "wrf",
              "er",
              "ett",
              "rftt"
            ]

            Output: "wertf"
            Example 2:

            Input:
            [
              "z",
              "x"
            ]

            Output: "zx"
        */
        public string AlienOrder(string[] words)
        {
            string result = string.Empty;

            Dictionary<char, int> indegree = new Dictionary<char, int>();
            Dictionary<char, HashSet<char>> map = new Dictionary<char, HashSet<char>>();

            foreach (var w in words)
            {
                foreach (var c in w)
                {
                    if (!indegree.ContainsKey(c))
                    {
                        indegree.Add(c, 0);
                    }
                }
            }

            for (int i = 0; i < words.Length - 1; i++)
            {
                var word1 = words[i];
                var word2 = words[i + 1];

                for (int j = 0; j < Math.Min(word1.Length, word2.Length); j++)
                {
                    var c1 = word1[j];
                    var c2 = word2[j];

                    if (c1 != c2)
                    {
                        if (map.ContainsKey(c1))
                        {
                            if (!map[c1].Contains(c2))
                            {
                                //add edge from c1 -> c2, this means c1 comes before c2
                                map[c1].Add(c2);
                                indegree[c2]++;
                            }
                        }
                        else
                        {
                            map.Add(c1, new HashSet<char>() { c2 });
                            indegree[c2]++;
                        }
                        break;
                    }
                }
            }

            //Topological sort
            var q = new Queue<char>();

            foreach (var item in indegree)
            {
                if (item.Value == 0)
                {
                    q.Enqueue(item.Key);
                }
            }

            while (q.Count > 0)
            {
                var c = q.Dequeue();
                result += c;

                if (map.ContainsKey(c))
                {
                    foreach (var t in map[c])
                    {
                        indegree[t]--;
                        if (indegree[t] == 0)
                        {
                            q.Enqueue(t);
                        }
                    }
                }
            }

            if (result.Length != indegree.Count)
            {
                result = "";
            }
            return result;
        }
    }
}
