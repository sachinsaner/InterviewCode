using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingPractice
{
    public class AffirmInterview
    {
        public AffirmInterview()
        {
        }

        /*
            Given an input list of strings, for each letter appearing anywhere 
            in the list, find the other letter(s) that appear in the most 
            number of words with that letter.

            Example: 
            ['abc', 'bcd', 'cde'] =>
              {
	            a: [b, c],	# b appears in 1 word with a, c appears in 1 word with a
	            b: [c], 	# c appears in 2 words with b, a and d each appear in only 1 word with b
	            c: [b, d], 	# b appears in 2 words with c, d appears in 2 words with c. But a and e each 
					              appear in only 1 word with c.
	            d: [c],		# c appears in 2 words with d. But b and e each appear in only 1 word with d
	            e: [c, d], 	# c appears in 1 word with e, d appears in 1 word with e
		
              }
        */

        /*Notes: Maintain adjeceny list through map for abc => a is neighbour of b so add a-> {b,1} where 1 is count of how many times we see b as a neighbour of a
         * similarly b is also neighbour of a so add b -> {a,1}
         * 
         */
        public static void LetterCount()
        {
            //List<string> words = new List<string>() { "abef", "bcd", "bde", "cadf" };
            List<string> words = new List<string>() { "abc", "bcd", "cde" };
            Dictionary<char, Dictionary<char, int>> map = new Dictionary<char, Dictionary<char, int>>();

            foreach (var s in words)
            {
                for (int i = 0; i < s.Length - 1; i++)
                {
                    for (int j = i + 1; j < s.Length; j++)
                    {
                        char c1 = s[i];
                        char c2 = s[j];

                        //Add c2 as a neighbour of c1
                        AddToMap(map, c1, c2);

                        //Add c1 as a neighbour of c2
                        AddToMap(map, c2, c1);
                    }
                }
            }

            foreach (var item in map)
            {
                Console.Write("{0} : ", item.Key);

                int prev = Int32.MinValue;
                foreach (var c in item.Value.OrderByDescending(c => c.Value))
                {
                    if (c.Value >= prev)
                    {
                        Console.Write(" {0} ", c.Key);
                        prev = c.Value;
                    }

                }

                Console.WriteLine();
            }
        }

        private static void AddToMap(Dictionary<char, Dictionary<char, int>> map, char c1, char c2)
        {
            if (map.ContainsKey(c1))
            {
                if (map[c1].ContainsKey(c2))
                {
                    map[c1][c2]++;
                }
                else
                {
                    map[c1].Add(c2, 1);
                }
            }
            else
            {
                var dict = new Dictionary<char, int>();
                dict.Add(c2, 1);
                map.Add(c1, dict);
            }
        }

        public static void ShortSubStr()
        {
            List<string> names = new List<string>() { "cheapair", "cheapoair", "peloton", "pelican" };

            Dictionary<string, string> map = new Dictionary<string, string>();

            foreach (var name in names)
            {
                for (int i = 0; i < name.Length - 1; i++)
                {
                    for (int j = i + 1; j < name.Length; j++)
                    {
                        var str = name.Substring(i, j - i);
                        bool found = false;
                        foreach (var n in names)
                        {
                            if (n == name) continue;

                            if (n.Contains(str))
                            {
                                found = true;
                            }
                        }

                        if (!found)
                        {
                            if (!map.ContainsKey(name))
                            {
                                map[name] = str;
                            }
                            else
                            {
                                if (map[name].Length > str.Length)
                                {
                                    map[name] = str;
                                }
                            }
                        }
                    }
                }
            }

            foreach (var item in map)
            {
                Console.WriteLine("{0} : {1}", item.Key, item.Value);
            }

        }
    }
}
