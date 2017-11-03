﻿namespace CodingPractice
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StringOperations
    {
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

        public void GenerateT9Combinations(string digits)
        {
            LinkedList<string> ans = new LinkedList<string>();
            string[] mapping = new string[] { "0", "1", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };
            ans.AddFirst("");

            for (int i = 0; i < digits.Length; i++)
            {
                int digit = digits[i] - '0';

                while (ans.First.Value.Length == i)
                {
                    string t = ans.First.Value;
                    ans.RemoveFirst();

                    foreach (var c in mapping[digit].ToCharArray())
                    {
                        ans.AddLast(t + c);
                    }
                }
            }

            foreach (var combo in ans)
            {
                Console.WriteLine(combo);
            }
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
            int word_space = 0;
            int remainingSpaces = 0;
            int i, spaces = 0, extra_spaces = 0;
            var result = new List<string>();

            while (start < words.Length)
            {
                len = -1;
                spaces = 0;
                extra_spaces = 0;

                for (i = start; i < words.Length && (len + words[i].Length + 1) <= maxWidth; i++)
                {
                    len += words[i].Length + 1;
                }

                word_space = (i - start) - 1;
                remainingSpaces = (maxWidth - (len - word_space));//substract extra empty spaces counted for words
                if (word_space > 0)
                {
                    spaces = remainingSpaces / word_space;
                    extra_spaces = remainingSpaces % word_space;
                }
                else
                {
                    spaces = remainingSpaces;
                    word_space = 1;
                }

                var res = new StringBuilder(words[start++]);
                while (word_space > 0)
                {
                    while (extra_spaces > 0)
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
                    if (start < i)
                    {
                        res.Append(words[start++]);
                    }
                    word_space--;
                }

                result.Add(res.ToString());
                start = i;
            }

            return result;
        }

        public List<String> FullJustify2(String[] words, int maxWidth)
        {
            List<String> ret = new List<string>();
            if (words.Length == 0 || maxWidth == 0)
            {
                ret.Add(""); //for some reason OJ expects list with empty string for empty array input
                return ret;
            }

            for (int i = 0, w; i < words.Length; i = w)
            {
                int len = -1; //We need to skip the space for last word hence start len = -1
                              //check how many words fit into the line
                for (w = i; w < words.Length && len + words[w].Length + 1 <= maxWidth; w++)
                {
                    len += words[w].Length + 1; // 1 extra for the space
                }

                //calculate the number of extra spaces that can be equally distributed
                //also calculate number of extra spaces that need to be added to first few
                //words till we fill the line width
                //For example line width is 20 we have three words of 3 4 2 4 length
                //[our_,life_,is_,good_,_,_,_,_,] ==> [our_,_,_,life_,_,_is_,_,good] 
                //   Note _, indicates space
                //Count number of empty spaces at end of line:= width-len = 20-(15) = 5 
                //These five spaces need to be equally distributed between 4-1 = 3 gaps
                //n words will have n-1 gaps between them
                // 5 / 3 = 1 extra space between each word (in addition to default 1 space, 
                //                                          total space count = 2)
                // 5 % 3 = 2 extra spaces between first three words as shown above

                int evenlyDistributedSpaces = 1; //If we don't enter loop at line # 37 then we need to have default value
                int extraSpaces = 0;
                int numOfGapsBwWords = w - i - 1; //w is already ponting to next index and -1 since
                                                  //n words have n-1 gaps between them

                //Moreover we don't need to do this computation if we reached the last word
                //of array or there is only one word that can be accommodate on the line
                //then we don't need to do any justify text. In both cases text can be left,
                //left-aligned 

                if (w != i + 1 && w != words.Length)
                {
                    //additional 1 for the default one space between words
                    evenlyDistributedSpaces = ((maxWidth - len) / numOfGapsBwWords) + 1;
                    extraSpaces = (maxWidth - len) % numOfGapsBwWords;
                }

                StringBuilder sb = new StringBuilder(words[i]);
                for (int j = i + 1; j < w; j++)
                {
                    for (int s = 0; s < evenlyDistributedSpaces; s++)
                    {
                        sb.Append(' ');
                    }
                    if (extraSpaces > 0)
                    {
                        sb.Append(' ');
                        extraSpaces--;
                    }
                    sb.Append(words[j]);
                }

                //Handle the above two cases we skipped, where there is only one word on line
                //or we reached end of word array. Last line should remain left aligned.
                int remaining = maxWidth - sb.Length;
                while (remaining > 0)
                {
                    sb.Append(' ');
                    remaining--;
                }
                ret.Add(sb.ToString());
            }
            return ret;
        }
    }
}


