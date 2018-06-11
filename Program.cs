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


		static void Main(string[] args)
		{
			StringOperations stringOperations = new StringOperations();

			var s = stringOperations.CheckLongestPalindrom("ghiabcdefhelloadamhelloabcdefghi");


			int[,] a = new int[,]
			{
				{9, 9, 4},
				{6, 6, 8},
				{2, 1, 1}
			};
			TreeOperations treeOperations = new TreeOperations();

			var root = treeOperations.BuildTree(new List<string> { "5", "3", "6", "2", "4", "null", "null", "1" });

			var res = treeOperations.InorderSuccessor2(root, 4);

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
