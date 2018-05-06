namespace CodingPractice
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    //https://leetcode.com/problems/find-all-anagrams-in-a-string/discuss/92007/sliding-window-algorithm-template-to-solve-all-the-leetcode-substring-search-problem
    public class StringOperations
    {
        /*
         * Input:  str1 = "aab", str2 = "xxy"
            Output: True
            'a' is mapped to 'x' and 'b' is mapped to 'y'.

            Input:  str1 = "aab", str2 = "xyz"
            Output: False
            One occurrence of 'a' in str1 has 'x' in str2 and 
            other occurrence of 'a' has 'y'.
         */
        //https://leetcode.com/problems/isomorphic-strings/description/
        public bool IsIsomorphic(string s, string t)
        {
            var map = new Dictionary<char, char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (!map.ContainsKey(s[i]))
                {
                    if (map.ContainsValue(t[i]))
                    {
                        return false;
                    }
                    map.Add(s[i], t[i]);
                }
                else if (map[s[i]] != t[i])
                {
                    return false;
                }
            }

            return true;
        }

        //https://leetcode.com/problems/count-and-say/description/
        public string CountAndSay(int n)
        {
            string res = string.Empty;

            if(n <= 0)
            {
                return res;
            }
            res = "1";

            for (int i = 2; i <= n; i++)
            {
                StringBuilder temp = new StringBuilder();

                for (int j = 0; j < res.Length; j++)
                {
                    char currChar = res[j];
                    int k = j;
                    int count = 0;
                    while(k < res.Length && currChar == res[k])
                    {
                        k++;
                        count++;
                    }

                    temp.Append(count);
                    temp.Append(currChar);
                    j = k - 1;
                }
                res = temp.ToString();
            }

            return res;
        }

        //https://leetcode.com/problems/string-compression/description/
        public int Compress(char[] chars)
        {
            int index = 0;

            for (int i = 0; i < chars.Length; i++)
            {
                char currChar = chars[i];
                int k = i;
                int count = 0;
                while (k < chars.Length && currChar == chars[k])
                {
                    k++;
                    count++;
                }
                chars[index++] = currChar;
                if (count > 1)
                {
                    foreach (char c in count.ToString())
                    {
                        chars[index++] = c;
                    }
                    i = k - 1;
                }

            }

            return index;
        }

        //https://leetcode.com/problems/remove-duplicate-letters/description/
        //https://leetcode.com/problems/remove-duplicate-letters/discuss/76766/Easy-to-understand-iterative-Java-solution
        public string RemoveDuplicateLetters(string s)
        {
            var map = new Dictionary<char, int>();

            //populate map with last seen index of chars
            int index = 0;
            foreach (var c in s)
            {
                if (map.ContainsKey(c))
                {
                    map[c] = index;
                }
                else
                {
                    map.Add(c, index);
                }
                index++;
            }

            int resLen = map.Count;
            StringBuilder result = new StringBuilder(map.Count);
            int start = 0;
            int end;

            for (int i = 0; i < resLen; i++)
            {
                end = this.MinIndexFromMap(map);
                char min = (char)('z' + 1);
                int minIndex = 0;

                //find smallest letter between start index and end index
                for (int j = start; j <= end; j++)
                {
                    if (s[j] < min && map.ContainsKey(s[j]))
                    {
                        min = s[j];
                        minIndex = j;
                    }
                }

                result.Append(min);

                //remove this from map as this letter is part of result 
                //and we need unique letters
                map.Remove(min);
                start = minIndex + 1;
            }

            return result.ToString();
        }

        private int MinIndexFromMap(Dictionary<char, int> map)
        {
            int min = Int32.MaxValue;
            foreach (var val in map.Values)
            {
                min = Math.Min(min, val);
            }

            return min;
        }

        //https://leetcode.com/problems/longest-word-in-dictionary/description/
        public string LongestWord(string[] words)
        {
            Array.Sort(words);

            string result = "";
            var set = new HashSet<string>();

            foreach (var w in words)
            {
                if (w.Length == 1 || set.Contains(w.Substring(0, w.Length - 1)))
                {
                    if (w.Length > result.Length)
                    {
                        result = w;

                    }
                    set.Add(w);
                }
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

        //https://leetcode.com/problems/judge-route-circle/description/
        public bool JudgeCircle(string moves)
        {
            int x = 0, y = 0;

            foreach (var d in moves)
            {
                switch (d)
                {
                    case 'U':
                        y++;
                        break;
                    case 'D':
                        y--;
                        break;
                    case 'L':
                        x--;
                        break;
                    case 'R':
                        x++;
                        break;
                }
            }

            return x == 0 && y == 0;

        }

        public List<String> FindRepeatedDnaSequences(String s)
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

        //https://leetcode.com/problems/find-all-anagrams-in-a-string/discuss/92007/sliding-window-algorithm-template-to-solve-all-the-leetcode-substring-search-problem
        //S = "ADOBECODEBANC"
        //T = "ABC"
        //Minimum window is "BANC".
        //S = "ADOBECODEBANCB"
        //T = "ABBC"
        //Minimum window is "BANCB".
        //code also handles duplicate in pattern
        public string MinWindow(string s, string t)
        {
            var map = new Dictionary<char, int>();
            string pattern = t;
           
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

            int start = 0, end = 0;
            int minStart = 0, minLen = int.MaxValue;
            int size = s.Length;

            /*** IMP pattern count is map.Count **/
            int patternCount = map.Count;

            // Move end to find a valid window.
            while (end < size)
            {
                char currChar = s[end];

                // If char in s exists in pattern, decrease pattern counter
                if (map.ContainsKey(currChar))
                {
                    // decrease value in map
                    map[currChar]--;

                    if (map[currChar] == 0)
                    {
                        patternCount--;
                    }
                }

                end++;

                // When we found a valid window, move start to find smaller window.
                while (patternCount == 0)
                {
                    currChar = s[start];

                    if (map.ContainsKey(currChar))
                    {
                        map[currChar]++;

                        // When char exists in pattern, increase counter.
                        if (map[currChar] > 0)
                        {
                            patternCount++;
                        }
                    }

                    if (end - start < minLen)
                    {
                        minStart = start;
                        minLen = end - start;
                    }

                    /* https://leetcode.com/problems/find-all-anagrams-in-a-string/description/
                     * 
                     * if(end-begin == t.length())
                     * {
                     *   result.add(begin);
                     * }
                     */

                    start++;
                }
            }

            if (minLen != int.MaxValue)
            {
                return s.Substring(minStart, minLen);
            }

            return "";
        }

        //https://leetcode.com/problems/longest-substring-without-repeating-characters/description/
        public int LengthOfLongestSubstring(string s)
        {
            int maxLen = 0;
            int start = 0, end = 0, counter = 0;

            var map = new Dictionary<char, int>();

            while(end < s.Length)
            {
                char currChar = s[end];

                if(map.ContainsKey(currChar))
                {
                    map[currChar]++;
                }
                else
                {
                    map.Add(currChar, 1);
                }

                if(map[currChar] > 1)
                {
                    counter++;
                }

                end++;

                while(counter > 0)
                {
                    currChar = s[start];

                    if(map[currChar] > 1)
                    {
                        counter--;
                    }

                    map[currChar]--;
                    start++;
                }

                maxLen = Math.Max(maxLen, end - start);
            }


            return maxLen;
        }

        //https://leetcode.com/problems/longest-substring-with-at-most-two-distinct-characters/description/
        //substring should have at most 2 distinct chars
        //abcabcabc -> ab or ca or bc ...
        public int LengthOfLongestSubstringTwoDistinct(string s)
        {
            int maxLen = 0;
            int start = 0, end = 0, counter = 0;

            var map = new Dictionary<char, int>();

            while (end < s.Length)
            {
                char currChar = s[end];

                if (map.ContainsKey(currChar))
                {
                    map[currChar]++;
                }
                else
                {
                    //increment counter only when new char is added to map
                    map.Add(currChar, 1);
                    counter++;
                }

                end++;

                //only 2 distinct chars are allowed 
                while (counter > 2)
                {
                    currChar = s[start];

                    map[currChar]--;

                    if (map[currChar] == 0)
                    {
                        counter--;

                        //remove the char so if we see it again it gets treated like new char
                        map.Remove(currChar);
                    }
                    start++;
                }

                maxLen = Math.Max(maxLen, end - start);
            }

            return maxLen;
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

                    // if only 1 word can be fit then dont add next word
                    if (start < end)
                    {
                        res.Append(words[start++]);
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

        public List<String> fullJustify(String[] words, int L)
        {
            List<String> res = new List<string>();

            for (int i = 0, k; i < words.Length; i = k)
            {
                // i: the index of word 
                // k: the current index of words in the line
                // len: current total len of words in the line
                int len = -1;
                for (k = i; k < words.Length && len + words[k].Length + 1 <= L; k++)
                {
                    len += words[k].Length + 1;
                }

                StringBuilder curStr = new StringBuilder(words[i]);
                int space = 1, extra = 0;

                // not 1 char, not last line
                if (k != i + 1 && k != words.Length)
                {
                    space = (L - len) / (k - i - 1) + 1; // 1 is for another space
                    extra = (L - len) % (k - i - 1);
                }

                // not 1 char, including last line, initialize space == 1 is to deal with last line case.
                for (int j = i + 1; j < k; j++)
                { 
                    // j: index of word in the current line
                    for (int s = space; s > 0; s--)
                    {
                        curStr.Append(" "); // add the "even" space
                    }

                    if (extra-- > 0)
                    {
                        curStr.Append(" ");
                    }

                    curStr.Append(words[j]);
                }

                // if it's the last line
                int strLen = L - curStr.Length;
                while (strLen-- > 0)
                {
                    curStr.Append(" ");
                }

                res.Add(curStr.ToString());
            }

            return res;
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



