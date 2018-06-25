namespace CodingPractice
{
    using CodingPractice;
    using System;
    using System.Collections.Generic;
	using System.Linq;
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

		public static int MaxDistToClosest(int[] seats)
        {
            int maxLen = 0;
			int start = 0;
            int len = 0;
            for (int i = 0; i < seats.Length; i++)
            {
				if(seats[i] == 1)
				{
					start = i;
				}
				while (i < seats.Length && seats[i] == 0)
                {
                    len++;
                    i++;

					if(i == seats.Length - 1)
					{
						maxLen = Math.Max(maxLen, (i - start) - 1);
						break;
					}
                    
                }
                if (len > 0)
                {
                    i--;
                    maxLen = Math.Max(maxLen, len / 2);
                    len = 0;
                }
            }

            return maxLen + 1;
        }
		static void Main(string[] args)
		{



			//var res2 = MaxDistToClosest(new int[] { 1, 0, 0, 0 });


			//StringOperations stringOperations = new StringOperations();

			//var s = stringOperations.CheckLongestPalindrom("ghiabcdefhelloadamhelloabcdefghi");


			//int[,] a = new int[,]
			//{
			//	{9, 9, 4},
			//	{6, 6, 8},
			//	{2, 1, 1}
			//};
			TreeOperations treeOperations = new TreeOperations();

			var root = treeOperations.BuildTree(new List<string> { "1", "1" });

			TreeNode prev = null;
			var res = treeOperations.ISBST(root, ref prev);

			treeOperations.Preorder_Iterative(root);
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
