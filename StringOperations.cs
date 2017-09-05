﻿using System;

namespace CodingPractice
{
    public class StringOperations
    {
        public int LVDistance(string s, string t)
        {
            // degenerate cases
            if (s == t) return 0;
            if (s.Length == 0) return t.Length;
            if (t.Length == 0) return s.Length;

            //Row 0 represents source string and column 0 represents target string 
            int[,] d = new int[t.Length + 1, s.Length + 1];

            //Set Row 0 values
            for(int i = 0; i < d.GetLength(1); i++)
            {
                d[0, i] = i;
            }
            //Set Column 0
            for (int i = 0; i < d.GetLength(0); i++)
            {
                d[i, 0] = i;
            }

            for(int i = 1; i < d.GetLength(0);  i++)
            {
                for(int j = 1; j < d.GetLength(1); j++)
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
    }
}
