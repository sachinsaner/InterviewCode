namespace CodingPractice
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    enum Direction
    {
        N,
        E,
        S,
        W
    };

    public class StringOperations
    {

        public string LongestWord(string[] words)
        {

            Array.Sort(words);

            string result = "";
            HashSet<string> set = new HashSet<string>();
           // string res = ""
            foreach (var w in words)
            {
                if (w.Length == 1 || set.Contains(w.Substring(0, w.Length - 1)))
                {
                    if(w.Length > result.Length)
                    {
                        result = w;

                    }
                    set.Add(w);
                }
            }

            return result;
        }
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
        public string AlienOrder(string[] words)
        {
            string result = string.Empty;

            Dictionary<char, int> indegree = new Dictionary<char, int>();
            Dictionary<char, HashSet<char>> map = new Dictionary<char, HashSet<char>>();

            foreach(var w in words)
            {
                foreach(var c in w)
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

                    if(c1 != c2)
                    {
                        if(map.ContainsKey(c1))
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
            Queue<Char> q = new Queue<char>();

            foreach(var item in indegree)
            {
                if(item.Value == 0)
                {
                    q.Enqueue(item.Key);
                }
            }

            while(q.Count > 0)
            {
                var c = q.Dequeue();
                result += c;

                if(map.ContainsKey(c))
                {
                    foreach(var t in map[c])
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
                result =  "";
            }
            return result;
        }

        //Classes = "ModelViewController", "MouseClickHandler","MouseHandler","MouseCHdan"
        //input: MouClH
        //output: MouseClickHandler
        public List<string> AutoCompleteIDE(string input, List<string> classes)
        {
            var results = new List<string>();

            foreach (var className in classes)
            {
                int inputIdx = 0;
                int classIdx = 0;

                while (classIdx < className.Length)
                {
                    if (input[inputIdx] == className[classIdx])
                    {
                        inputIdx++;
                        if (inputIdx == input.Length)
                        {
                            results.Add(className);
                            break;
                        }
                    }
                    else if (char.IsLower(input[inputIdx]) && input[inputIdx] != className[classIdx])
                    {
                        break;
                    }
                    classIdx++;
                }
            }

            return results;
        }


        public IList<IList<int>> Permute(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return null;
            }

            IList<IList<int>> solution = new List<IList<int>>
            {
                new List<int> { nums[0] }
            };

            for (int i = 1; i < nums.Length; i++)
            {
                var temp = new List<IList<int>>();
                foreach (var item in solution)
                {
                    for (int j = 0; j <= item.Count; j++)
                    {
                        var newItem = new List<int>(item);
                        newItem.Insert(j, nums[i]);
                        temp.Add(newItem);
                    }
                }

                solution = temp;
            }

            return solution;
        }

        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            HashSet<string> set = new HashSet<string>();

            if (nums == null || nums.Length == 0)
            {
                return null;
            }

            IList<IList<int>> solution = new List<IList<int>>
            {
                new List<int> { nums[0] }
            };

            for (int i = 1; i < nums.Length; i++)
            {
                var temp = new List<IList<int>>();

                foreach (var item in solution)
                {
                    for (int j = 0; j <= item.Count; j++)
                    {
                        var newItem = new List<int>(item);
                        newItem.Insert(j, nums[i]);

                        var str = ConvertToString(newItem);

                        if (!set.Contains(str))
                        {
                            temp.Add(newItem);
                            set.Add(str);
                        }
                    }
                }

                solution = temp;
            }

            return solution;
        }

        private string ConvertToString(List<int> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(item.ToString());
            }

            return sb.ToString();
        }

        private HashSet<string> words = new HashSet<string>
        {
            "mobile",
            "samsung",
            "sam",
            "sung",
            "man",
            "mango",
            "icecream",
            "and",
            "go",
            "i",
            "like",
            "ice",
            "cream"
        };

        //Given a sequence of moves for a robot, check if the sequence is circular or not. A sequence of moves is circular if first and last positions of robot are same. A move can be on of the following.

        //  G - Go one unit
        //  L - Turn left
        //  R - Turn right 

        //Examples:

        //Input: path[] = "GLGLGLG"
        //Output: Given sequence of moves is circular 

        //Input: path[] = "GLLG"
        //Output: Given sequence of moves is circular 
        public bool IsRobotIncircle(string path)
        {
            int x = 0, y = 0;

            //        N
            //        |
            //        |
            //W -------------- E
            //        |
            //        |
            //        S          

            //Map contains next direction to be taken from the current command is move Right
            var rMap = new Dictionary<Direction, Direction>()
            {
                {Direction.N, Direction.E},
                {Direction.E, Direction.S},
                {Direction.S, Direction.W},
                {Direction.W, Direction.N},
            };

            //Map contains next direction to be taken from the current command is move Left
            var lMap = new Dictionary<Direction, Direction>()
            {
                {Direction.N, Direction.W},
                {Direction.E, Direction.N},
                {Direction.S, Direction.E},
                {Direction.W, Direction.S},
            };

            Direction dir = Direction.N;

            foreach (char s in path)
            {
                if (s == 'L')
                {
                    dir = lMap[dir];
                }
                else if (s == 'R')
                {
                    dir = rMap[dir];
                }

                if (s == 'G')
                {
                    switch (dir)
                    {
                        case Direction.N:
                            y++;
                            break;
                        case Direction.E:
                            x++;
                            break;
                        case Direction.W:
                            x--;
                            break;
                        case Direction.S:
                            y--;
                            break;
                    }
                }
            }

            return (x == 0 && y == 0);
        }

        public List<String> findRepeatedDnaSequences(String s)
        {
            var seen = new HashSet<string>();
            var repeated = new HashSet<string>();

            for (int i = 0; i + 9 < s.Length; i++)
            {
                String ten = s.Substring(i, 10);
                if (!seen.Add(ten))
                    repeated.Add(ten);
            }
            return new List<string>(repeated);
        }

        public int LVDistance(string s, string t)
        {
            // degenerate cases
            if (s == t) return 0;
            if (s.Length == 0) return t.Length;
            if (t.Length == 0) return s.Length;

            //Row 0 represents source string and column 0 represents target string 
            int[,] d = new int[t.Length + 1, s.Length + 1];

            //Set Row 0 values
            for (int i = 0; i < d.GetLength(1); i++)
            {
                d[0, i] = i;
            }
            //Set Column 0
            for (int i = 0; i < d.GetLength(0); i++)
            {
                d[i, 0] = i;
            }

            for (int i = 1; i < d.GetLength(0); i++)
            {
                for (int j = 1; j < d.GetLength(1); j++)
                {
                    int cost = t[i - 1] == s[j - 1] ? 0 : 1;

                    d[i, j] = Minimum(d[i, j - 1] + 1, d[i - 1, j] + 1, d[i - 1, j - 1] + cost);
                }
            }

            return d[t.Length, s.Length];
        }

        public int LevenshteinDistance(string s, string t)
        {
            // degenerate cases
            if (s == t) return 0;
            if (s.Length == 0) return t.Length;
            if (t.Length == 0) return s.Length;

            // create two work vectors of integer distances
            int[] v0 = new int[t.Length + 1];
            int[] v1 = new int[t.Length + 1];

            // initialize v0 (the previous row of distances)
            // this row is A[0][i]: edit distance for an empty s
            // the distance is just the number of characters to delete from t
            for (int i = 0; i < v0.Length; i++)
                v0[i] = i;

            for (int i = 0; i < s.Length; i++)
            {
                // calculate v1 (current row distances) from the previous row v0

                // first element of v1 is A[i+1][0]
                //   edit distance is delete (i+1) chars from s to match empty t
                v1[0] = i + 1;

                // use formula to fill in the rest of the row
                for (int j = 0; j < t.Length; j++)
                {
                    var cost = (s[i] == t[j]) ? 0 : 1;
                    v1[j + 1] = Minimum(v1[j] + 1, v0[j + 1] + 1, v0[j] + cost);
                }

                // copy v1 (current row) to v0 (previous row) for next iteration
                for (int j = 0; j < v0.Length; j++)
                    v0[j] = v1[j];
            }

            return v1[t.Length];
        }

        private int Minimum(int a, int b, int c)
        {
            return Math.Min(Math.Min(a, b), c);
        }

        //final ans is multiplication of all chars of digits at map
        // for Ex. for 2,3 its multiplication of "abc" and "def" giving total 9 
        //values ad, ae, af, bd, be, bf, cd, ce, cf
        // for 234 -> adg, adh, adi ...
        public void GenerateT9Combinations(string digits)
        {
            var queue = new Queue<string>();
            var mapping = new string[] { "0", "1", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };
            queue.Enqueue("");

            for (int i = 0; i < digits.Length; i++)
            {
                int digit = digits[i] - '0';

                //check the first node of linked list to see if string has reached digit size
                while (queue.Peek().Length == i)
                {
                    string t = queue.Dequeue();

                    foreach (var c in mapping[digit].ToCharArray())
                    {
                        queue.Enqueue(t + c);
                    }
                }
            }

            foreach (var combo in queue)
            {
                Console.WriteLine(combo);
            }
        }

        public IList<string> WordBreak2(string s, IList<string> wordDict)
        {
            var result = new List<string>();
            var q = new Queue<Tuple<int, int>>();

            int idx = 0;

            for (int i = 0; i <= s.Length; i++)
            {
                string str = s.Substring(0, i);
                if (wordDict.Contains(str))
                {
                    q.Enqueue(Tuple.Create(i, idx++));
                    result.Add(str);
                }
            }

            while (q.Count > 0)
            {
                var item = q.Dequeue();

                for (int i = item.Item1; i < s.Length; i++)
                {
                    string word = s.Substring(item.Item1, (i - item.Item1) + 1);

                    if (wordDict.Contains(word))
                    {
                        q.Enqueue(Tuple.Create(i + 1, item.Item2));

                        result[item.Item2] += (" " + word);
                    }
                }
            }

            return result;
        }

        public bool WordBreak(string s)
        {
            //ilikesamsungmobile
            // BFS
            Queue<int> queue = new Queue<int>();
            HashSet<int> visited = new HashSet<int>();

            queue.Enqueue(0);

            while (queue.Count > 0)
            {
                int start = queue.Peek();
                queue.Dequeue();

                if (!visited.Contains(start))
                {
                    visited.Add(start);
                    for (int j = start; j < s.Length; j++)
                    {
                        string word = s.Substring(start, j - start + 1);//this funct needs length hence j -start + 1

                        if (words.Contains(word))
                        {
                            queue.Enqueue(j + 1);
                            if (j + 1 == s.Length)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        //S = "ADOBECODEBANC"
        //T = "ABC"
        //Minimum window is "BANC".
        //S = "ADOBECODEBANCB"
        //T = "ABBC"
        //Minimum window is "BANCB".
        //code also handles duplicate in pattern
        public string MinimumWindow(string s, string pattern)
        {
            var map = new Dictionary<char, int>();

            foreach (var c in pattern)
            {
                int val;
                if (map.TryGetValue(c, out val))
                {
                    map[c]++;
                }
                else
                {
                    map.Add(c, 1);
                }
            }

            // counter represents the number of chars of t to be found in s.
            int start = 0, end = 0;
            int counter = pattern.Length, minStart = 0, minLen = int.MaxValue;
            int size = s.Length;

            // Move end to find a valid window.
            while (end < size)
            {
                int val;
                // If char in s exists in pattern, decrease counter
                if (map.TryGetValue(s[end], out val))
                {
                    if (val >= 1)
                    {
                        counter--;
                    }
                }

                // If char does exists in pattern then decrease value in map
                if (map.ContainsKey(s[end]))
                {
                    map[s[end]]--;
                }

                end++;

                // When we found a valid window, move start to find smaller window.
                while (counter == 0)
                {
                    if (end - start < minLen)
                    {
                        minStart = start;
                        minLen = end - start;
                    }

                    if (map.ContainsKey(s[start]))
                    {
                        map[s[start]]++;

                        // When char exists in pattern, increase counter.
                        if (map[s[start]] >= 1)
                        {
                            counter++;
                        }
                    }

                    start++;
                }
            }

            if (minLen != int.MaxValue)
            {
                return s.Substring(minStart, minLen);
            }

            return "";
        }

        private bool isValid(String s)
        {

            int num = int.Parse(s);

            if (num >= 1 && num <= 26)
                return true;
            else
                return false;
        }

        //System.out.println(numEncoding("1234"));
        public int NumEncoding(String s)
        {
            if (s.Length == 0)
            {
                return 1;
            }
            if (s.Length == 1)
            {
                return 1;
            }

            int num = 0;

            num += NumEncoding(s.Substring(1));
            var temp = s.Substring(0, 2);
            if (isValid(temp))
            {
                num += NumEncoding(s.Substring(2));
            }
            return num;
        }

        //Given string 123 decode it char string 1,2,3; 12,3; 1, 23 
        //2304
        public int NumDecodings(string s)
        {
            if (s == null || s.Length == 0)
            {
                return 0;
            }

            int n = s.Length;
            int[] dp = new int[n + 1];

            //initialization of solution for case where string size less than 3, there is always 1 way to decode it
            dp[0] = 1;
            dp[1] = s[0] != '0' ? 1 : 0;
            /*
             * Algo is to look at the last 2 chars of the string for ex .12
             * Let's say you want to decode "12".
             *  Before the loop, dp[] array becomes [1,1,0]. 
             *  Then when i = 2, first is 2, second is 12, both can be decoded. 
             *  So dp[2] = dp[1] + dp[0], which is 2. 
             *  If you initialize dp[0]=0, then the answer becomes 1, which is incorrect. 
             */
            for (int i = 2; i <= n; i++)
            {
                int first = int.Parse(s.Substring(i - 1, 1));
                int second = int.Parse(s.Substring(i - 2, 2));

                if (first >= 1 && first <= 9)
                {
                    dp[i] += dp[i - 1];
                }
                if (second >= 10 && second <= 26)
                {
                    dp[i] += dp[i - 2];
                }
            }

            return dp[n];
        }

        //a3[b2[c1[d]]]e
        //abcdcdbcdcdbcdcde
        //sd2[f2[e]g]i
        //sdfeegfeegi" 
        public void Decompressstring(string s)
        {
            string result = "";
            int i = 0;
            Stack<int> numStack = new Stack<int>();
            Stack<string> resStack = new Stack<string>();

            /*
             * Algo: 1. Use 2 separate stacks for numbers and result strings
             * 2. keep adding chars to result string until
             * 3. we find '[' bracket, then put the result on result stack and set result to empty
             * 4. append all chars inside '[' bracket to result
             * 5. as soon as we find ']' bracket pop num from numstack (note : '[' will always be followed by num)
             * 6. pop a result from result stack and appent the current result to it unit num is zero
             * */

            while (i < s.Length)
            {
                if (char.IsDigit(s[i]))
                {
                    int number = 0;
                    while (char.IsDigit(s[i]))
                    {
                        number = 10 * number + (s[i] - '0');
                        i++;
                    }
                    numStack.Push(number);
                }
                else if (s[i] == '[')
                {
                    resStack.Push(result);
                    result = "";
                    i++;
                }
                else if (s[i] == ']')
                {
                    int count = numStack.Pop();
                    StringBuilder temp = new StringBuilder(resStack.Pop());

                    while (count > 0)
                    {
                        temp.Append(result);
                        count--;
                    }

                    result = temp.ToString();
                    i++;
                }
                else
                {
                    result += s[i++];
                }
            }
            Console.WriteLine(result);
        }

        private string InsertInside(string str, int leftIndex)
        {
            string left = str.Substring(0, leftIndex + 1);
            string right = str.Substring(leftIndex + 1);
            return left + "()" + right;
        }

        public HashSet<string> GenerateParens(int remaining)
        {
            var set = new HashSet<string>();
            if (remaining == 0)
            {
                set.Add("");
            }
            else
            {
                HashSet<string> prev = GenerateParens(remaining - 1);
                foreach (string str in prev)
                {
                    for (int i = 0; i < str.Length; i++)
                    {
                        if (str[i] == '(')
                        {
                            string s = InsertInside(str, i);
                            /* Add s to set if it is not already in there. Note: 	
                             * HashSet automatically checks for duplicates before
                             * adding, so an explicit check is not necessary. */
                            set.Add(s);
                        }
                    }
                    set.Add("()" + str);
                }
            }
            return set;
        }

        public void AddParen(List<string> list, int leftRem, int rightRem, char[] str, int index)
        {
            if (leftRem < 0 || rightRem < leftRem)
            {
                return; // invalid state
            }

            if (leftRem == 0 && rightRem == 0)
            { /* all out of left and right parentheses */
                list.Add(new string(str));
            }
            else
            {
                str[index] = '('; // Add left and recurse1
                AddParen(list, leftRem - 1, rightRem, str, index + 1);

                str[index] = ')'; // Add right and recurse
                AddParen(list, leftRem, rightRem - 1, str, index + 1);
            }
        }

        public List<string> GenerateParens2(int count)
        {
            char[] str = new char[count * 2];
            List<string> list = new List<string>();
            AddParen(list, count, count, str, 0);
            return list;
        }

        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            if (words == null || words.Length == 0)
            {
                return new List<string>();
            }

            int start = 0;
            int len = -1;
            int space_between_words = 0;
            int remainingSpaces = 0;
            int end, spaces = 0, extra_spaces = 0;
            var result = new List<string>();

            while (start < words.Length)
            {
                len = -1;
                spaces = 0;
                extra_spaces = 0;

                //count the current len including extra space after the word
                for (end = start; end < words.Length && (len + words[end].Length + 1) <= maxWidth; end++)
                {
                    len += words[end].Length + 1;
                }

                // for n words there will be n-1 spaces in between them
                space_between_words = (end - start) - 1;

                //substract extra empty spaces counted for words
                remainingSpaces = (maxWidth - (len - space_between_words));

                //check for div by zero
                if (space_between_words > 0)
                {
                    spaces = remainingSpaces / space_between_words;
                    extra_spaces = remainingSpaces % space_between_words;
                }
                else
                {
                    // if we have only 1 word that can be accomodated in line
                    spaces = remainingSpaces;
                    space_between_words = 1;
                }

                var res = new StringBuilder(words[start++]);
                while (space_between_words > 0 && end != words.Length)
                {
                    if (end != words.Length)
                    {
                        //extra spaces needs to be evenly distributed from left to right
                        if (extra_spaces > 0)
                        {
                            res.Append(" ");
                            extra_spaces--;
                        }
                        int counter = spaces;
                        while (counter > 0)
                        {
                            res.Append(" ");
                            counter--;
                        }
                        if (start < end)
                        {
                            res.Append(words[start++]);
                        }
                    }
                    space_between_words--;
                }

                // if we are processing the last line then extra spaces needs to be added to end
                if (end == words.Length)
                {
                    while (start < end)
                    {
                        res.Append(" ");
                        res.Append(words[start++]);
                        remainingSpaces--;
                    }

                    while (remainingSpaces > 0)
                    {
                        res.Append(" ");
                        remainingSpaces--;
                    }
                }

                result.Add(res.ToString());
                start = end;
            }
            return result;
        }

        public int FixBrackets(string brackets)
        {
            int count = 0;
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < brackets.Length; i++)
            {
                if (brackets[i] == '(')
                {
                    stack.Push('(');
                }
                else
                {
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        count++;
                    }
                }
            }

            return count + stack.Count;
        }

        public void PowerSet(string set)
        {
            int max = 1 << set.Length;
            var allSets = new List<List<string>>();

            for (int i = 0; i < max; i++)
            {
                int index = 0;
                var subset = new List<string>();

                for (int k = i; k > 0; k = k >> 1)
                {
                    if ((k & 1) > 0)
                    {
                        subset.Add(set[index].ToString());
                    }
                    index++;
                }
                allSets.Add(subset);
            }

            foreach (var item in allSets)
            {
                foreach (var s in item)
                {
                    Console.WriteLine(" " + s);
                }
                Console.WriteLine();
            }
        }

        public int LongestPalindromicSubsequence(string str)
        {
            int[,] dp = new int[str.Length, str.Length];

            for (int i = 0; i < str.Length; i++)
            {
                //size of palindrome will be 1 for single char
                dp[i, i] = 1;
            }

            for (int l = 2; l < str.Length; l++)
            {
                //For string ACBA, for l = 2 , str = AC 
                for (int i = 0; i < str.Length - l + 1; i++)
                {
                    int j = i + l - 1;

                    if (l == 2 && str[i] == str[j])
                    {
                        //for BBB, since l =2 i.e for BB 
                        dp[i, j] = 2;
                    }
                    else if (str[i] == str[j])
                    {
                        //for ACDA
                        //as first and last char match 2 + LPS(CD)
                        dp[i, j] = dp[i + 1, j - 1] + 2;
                    }
                    else
                    {
                        //if start and end char dont match then palindrome size could be 
                        //max of eliminate 1 char in string or eliminate last char 
                        dp[i, j] = Math.Max(dp[i, j - 1], dp[i + 1, j]);
                    }
                }
            }
            return dp[0, str.Length - 1];
        }


        //Given a string containing just the characters '(' and ')', find the length of the longest valid(well-formed) parentheses substring.
        //For "(()", the longest valid parentheses substring is "()", which has length = 2.
        //Another example is ")()())", where the longest valid parentheses substring is "()()", which has length = 4.

        //Key here is to push the index of parentheses on the stack
        //once entire string is processed and stack is empty whole string is valid
        //else go through stack to find valid len as last item on stack contains the invalid index
        public int LongestValidParentheses(string s)
        {
            int validLen = 0;
            int maxValidLen = 0;
            var stack = new Stack<int>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(')
                {
                    stack.Push(i);
                }
                else
                {
                    //handle a condition for )( 
                    if (stack.Count > 0 && s[stack.Peek()] == '(')
                    {
                        stack.Pop();
                    }
                    else
                    {
                        //push index of invalid ')' bracket
                        stack.Push(i);
                    }
                }
            }
            if (stack.Count == 0)
            {
                return s.Length;
            }
            else
            {
                var lastIndex = s.Length;

                while (stack.Count > 0)
                {
                    validLen = (lastIndex - stack.Peek()) - 1;

                    maxValidLen = Math.Max(validLen, maxValidLen);

                    lastIndex = stack.Pop();
                }

                //if the max len exists on the left side of the last invalid bracket
                if (lastIndex != 0)
                {
                    maxValidLen = Math.Max(lastIndex, maxValidLen);
                }

                return maxValidLen;
            }
        }

        public bool IsInterleave(string s1, string s2, string s3)
        {
            bool[,] dp = new bool[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++)
            {
                for (int j = 0; j <= s2.Length; j++)
                {
                    int l = i + j - 1;

                    if (i == 0 && j == 0)
                    {
                        dp[i, j] = true;
                    }
                    else if (i == 0)
                    {
                        if (s1.Length > 0 && s1[j - 1] == s3[l])
                        {
                            dp[i, j] = dp[i, j - 1];
                        }
                    }
                    else if (j == 0)
                    {
                        if (s2.Length > 0 && s2[i - 1] == s3[l])
                        {
                            dp[i, j] = dp[i - 1, j];
                        }
                    }
                    else
                    {
                        dp[i, j] = (s1[i - 1] == s3[l] ? dp[i, j - 1] : false) || (s2[j - 1] == s3[l] ? dp[i - 1, j] : false);
                    }
                }
            }

            return dp[s1.Length, s2.Length];
        }




        //https://leetcode.com/problems/word-ladder/discuss/
        //Given two words(beginWord and endWord), and a dictionary's word list, find the length of shortest transformation sequence
        //from beginWord to endWord, such that:
        //Only one letter can be changed at a time.
        //Each transformed word must exist in the word list.Note that beginWord is not a transformed word.
        //For example,
        //Given:
        //beginWord = "hit"
        //endWord = "cog"
        //wordList = ["hot", "dot", "dog", "lot", "log", "cog"]
        //As one shortest transformation is "hit" -> "hot" -> "dot" -> "dog" -> "cog",
        //return its length 5.
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            Queue<Tuple<string, int>> q = new Queue<Tuple<string, int>>();

            q.Enqueue(new Tuple<string, int>(beginWord, 1));

            HashSet<string> dict = new HashSet<string>(wordList);

            while (q.Count > 0)
            {
                var item = q.Dequeue();

                foreach (var word in wordList)
                {
                    if (IsAdjecentWord(item.Item1, word) && dict.Contains(word))
                    {
                        if (word == endWord)
                        {
                            return item.Item2 + 1;
                        }

                        q.Enqueue(new Tuple<string, int>(word, item.Item2 + 1));

                        //Remove this word so as we dont visit it again
                        dict.Remove(word);
                    }
                }
            }

            return 0;
        }

        private bool IsAdjecentWord(string original, string match)
        {
            int count = 0;

            for (int i = 0; i < original.Length; i++)
            {
                if (original[i] != match[i])
                {
                    count++;
                }

                if (count > 1)
                {
                    return false;
                }
            }

            return true;
        }

        public IList<IList<string>> Partition(string s)
        {
            var solution = new List<IList<string>>();
            int[] solArray = new int[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (IsPalindrome(j, i, s))
                    {
                        solArray[i] = Math.Max((i - j) + 1, solArray[i]);
                    }
                }

                //solution.Add(temp);

            }

            return solution;
        }

        private bool IsPalindrome(int start, int end, string s)
        {
            while (start < end)
            {
                if (s[start] != s[end])
                {
                    return false;
                }

                start++;
                end--;
            }

            return true;
        }

        private bool isPalindrome(string str)
        {
            int i = 0;
            int j = str.Length - 1;

            while (i < j)
            {
                if (str[i++] != str[j--]) return false;
            }

            return true;
        }

        //Given a list of unique words, find all pairs of distinct indices (i, j) in the given list, 
        //so that the concatenation of the two words, i.e. words[i] + words[j] is a palindrome.

        //Partition the word into left and right, and see 
        //1) if there exists a candidate in map equals the left side of current word, 
        //and right side of current word is palindrome, so concatenate(current word, candidate) forms a pair: left | right | candidate.
        //2) same for checking the right side of current word: candidate | left | right.
        //https://leetcode.com/problems/palindrome-pairs/discuss/79215/Easy-to-understand-AC-C++-solution-O(n*k2)-using-map
        public List<List<int>> PalindromePairs(List<string> strings)
        {
            var map = new Dictionary<string, int>();
            var ans = new List<List<int>>();

            int index = 0;
            foreach (var str in strings)
            {
                var s = str.ToCharArray();
                Array.Reverse(s);
                map.Add(new string(s), index++);
            }

            for (int i = 0; i < strings.Count; i++)
            {
                for (int j = 0; j < strings[i].Length; j++)
                {
                    string left;
                    string right;

                    left = strings[i].Substring(0, j + 1);
                    right = strings[i].Substring(j + 1, strings[i].Length - (j + 1));

                    if (map.ContainsKey(left) && map[left] != i && isPalindrome(right))
                    {
                        ans.Add(new List<int>() { i, map[left] });
                    }

                    if (map.ContainsKey(right) && map[right] != i && isPalindrome(left))
                    {
                        ans.Add(new List<int>() { map[right], i });
                    }
                }
            }

            return ans;
        }

        //Find longest substring palindrome
        public int LongestPalindrome(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length == 1)
                return 1;

            int maxLen = 0;

            for (int i = 0; i < str.Length; i++)
            {
                int len1 = ExtendPalindrome(str, i, i);
                int len2 = ExtendPalindrome(str, i, i + 1); // for even lenght

                maxLen = Math.Max(Math.Max(len1, len2), maxLen);
            }

            return maxLen;
        }

        private int ExtendPalindrome(string str, int left, int right)
        {
            while (left >= 0 && right < str.Length && str[left] == str[right])
            {
                left--;
                right++;
            }

            return (right - left) - 1;
        }
    }
}



