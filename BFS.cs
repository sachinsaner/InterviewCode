﻿using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class BFS
    {
        //https://leetcode.com/problems/remove-invalid-parentheses/description/
        /*
         * Remove the minimum number of invalid parentheses in order to make the input string valid. Return all possible results.

            Note: The input string may contain letters other than the parentheses ( and ).

            Example 1:

            Input: "()())()"
            Output: ["()()()", "(())()"]
            Example 2:

            Input: "(a)())()"
            Output: ["(a)()()", "(a())()"]
            Example 3:

            Input: ")("
            Output: [""]
        */
        public List<string> RemoveInvalidParentheses(String s)
        {
            List<String> res = new List<string>();

            // sanity check
            if (s == null) return res;

            HashSet<string> visited = new HashSet<string>();
            Queue<string> queue = new Queue<string>();

            // initialize
            queue.Enqueue(s);
            visited.Add(s);

            bool found = false;

            while (queue.Count > 0)
            {
                s = queue.Dequeue();

                if (isValid(s))
                {
                    // found an answer, add to the result
                    res.Add(s);
                    found = true;
                }

                if (found)
                {
                    continue;
                }

                // generate all possible states
                for (int i = 0; i < s.Length; i++)
                {
                    // we only try to remove left or right paren
                    if (s[i] != '(' && s[i] != ')')
                    {
                        continue;
                    }

                    string t = s.Substring(0, i) + s.Substring(i + 1);

                    if (!visited.Contains(t))
                    {
                        // for each state, if it's not visited, add it to the queue
                        queue.Enqueue(t);
                        visited.Add(t);
                    }
                }
            }

            return res;
        }

        // helper function checks if string s contains valid parantheses
        bool isValid(String s)
        {
            int count = 0;

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '(')
                {
                    count++;
                }
                if (c == ')' && count-- == 0)
                {
                    return false;
                }
            }

            return count == 0;
        }

        public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
        {
            Dictionary<string, Dictionary<string, double>> adj = new Dictionary<string, Dictionary<string, double>>();

            for (int i = 0; i < equations.Count; i++)
            {
                var src = equations[i][0];
                var dest = equations[i][1];

                AddAdj(adj, src, dest, values[i]);
                AddAdj(adj, dest, src, 1 / values[i]);

            }

            List<double> res = new List<double>();
            Queue<Tuple<string, double>> q = new Queue<Tuple<string, double>>();

            foreach (var query in queries)
            {
                var src = query[0];
                var dest = query[1];
                bool found = false;

                if (!adj.ContainsKey(dest))
                {
                    res.Add(-1);
                    continue;
                }

                q.Enqueue(Tuple.Create(src, 1.0));


                HashSet<string> visited = new HashSet<string>();
                visited.Add(src);

                while (q.Count > 0)
                {
                    var curr = q.Dequeue();

                    if (curr.Item1 == dest)
                    {
                        res.Add(curr.Item2);
                        found = true;
                    }

                    if (adj.ContainsKey(curr.Item1))
                    {
                        foreach (var v in adj[curr.Item1])
                        {
                            if (!visited.Contains(v.Key))
                            {
                                q.Enqueue(Tuple.Create(v.Key, v.Value * curr.Item2));
                                visited.Add(v.Key);
                            }
                        }
                    }

                }

                if (!found)
                {
                    res.Add(-1);
                }
            }

            return res.ToArray();
        }

        private void AddAdj(Dictionary<string, Dictionary<string, double>> adj, string src, string dest, double val)
        {
            if (adj.ContainsKey(src))
            {
                adj[src].Add(dest, val);
            }
            else
            {
                adj[src] = new Dictionary<string, double>() { { dest, val } };
            }
        }
    }
}
