namespace CodingPractice
{
    using CodingPractice;
    using System;
    using System.Collections.Generic;
	using System.Text;

	//Sliding window
	//https://leetcode.com/problems/find-all-anagrams-in-a-string/discuss/92007/sliding-window-algorithm-template-to-solve-all-the-leetcode-substring-search-problem
	class Program
    {
		private static HashSet<string> words = new HashSet<string>
        {
            //"mobile",
            "samsung",
            "sam",
            "sung",
            //"man",
            //"mango",
            //"icecream",
            //"and",
            //"go",
            //"i",
            //"like",
            //"ice",
            //"cream",
            "cats",
            "cat",
            "dog",
            "and",
            "sand"

			//"pineapple","apple","pen","applepen","pine",
        };


		static void Main(string[] args)
		{
			int[,] a = new int[,]
			{
				{9, 9, 4},
				{6, 6, 8},
				{2, 1, 1}
			};
           
        }

  
		public static void DFS(int[,] matrix, int r, int c, ref int count)
		{
			int rows = matrix.GetLength(0);
			int colms = matrix.GetLength(1);

			if(r < 0 || r > rows - 1 || c < 0 || c > colms - 1 || matrix[r,c] == 1)
			{
				return;
			}

            if(r == rows - 1 && c == colms - 1)
			{
				count++;
			}

			matrix[r, c] = 1;
			DFS(matrix, r, c + 1, ref count);
			DFS(matrix, r - 1, c + 1, ref count);
			DFS(matrix, r + 1, c + 1, ref count);

			matrix[r, c] = 0;
		}


    }
}
