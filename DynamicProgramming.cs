using System;
namespace CodingPractice
{
    public class DynamicProgramming
    {
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


        // int arr[] = {23,10,22,5,33,8,9,21,50,41,60,80,99, 22,23,24,25,26,27};
        public int LongestIncreasingSubsequenceLength(int[] arr)
        {
            int[] resultArr = new int[arr.Length];
            int[] resultSequence = new int[arr.Length];

            int prev = 0;
            resultArr[0] = 1;

            for (int curr = 1; curr < arr.Length; curr++)
            {
                for (prev = 0; prev < curr; prev++)
                {
                    if (arr[curr] > arr[prev] && (resultArr[prev] + 1 > resultArr[curr]))
                    {
                        resultArr[curr] = resultArr[prev] + 1;
                    }
                }
            }

            int maxLen = 0;

            for (int i = 0; i < resultArr.Length; i++)
            {
                if (resultArr[i] > maxLen)
                {
                    maxLen = resultArr[i];
                }
            }
            return maxLen;
        }

        public int MinimumCoinBottomUp(int total, int[] coins)
        {
            int[] T = new int[total + 1];
            int[] R = new int[total + 1];
            T[0] = 0;
            for (int i = 1; i <= total; i++)
            {
                T[i] = int.MaxValue - 1;
                R[i] = -1;
            }
            for (int j = 0; j < coins.Length; j++)
            {
                for (int i = 1; i <= total; i++)
                {
                    if (i >= coins[j])
                    {
                        if (T[i - coins[j]] + 1 < T[i])
                        {
                            T[i] = 1 + T[i - coins[j]];
                            R[i] = j;
                        }
                    }
                }
            }
            printCoinCombination(R, coins);
            return T[total];
        }

        private void printCoinCombination(int[] R, int[] coins)
        {
            if (R[R.Length - 1] == -1)
            {
                Console.WriteLine("No solution is possible");
                return;
            }
            int start = R.Length - 1;
            Console.WriteLine("Coins used to form total ");
            while (start != 0)
            {
                int j = R[start];
                Console.WriteLine(coins[j] + " ");
                start = start - coins[j];
            }
            Console.WriteLine("\n");
        }

        //[186,419,83,408]
        //6249
        public int CoinChange(int[] coins, int amount)
        {
            if (amount <= 0)
            {
                return 0;
            }

            int[] combinations = new int[amount + 1];

            combinations[0] = 1;

            foreach (int coin in coins)
            {
                for (int i = 1; i < combinations.Length; i++)
                {
                    if (coin > i)
                    {
                        continue;
                    }
                    combinations[i] += combinations[i - coin];
                }
            }

            return combinations[amount] == 0 ? -1 : combinations[amount];
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

    }
}
