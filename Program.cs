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

		public static int HammingDistance(int x, int y)
        {
            int count = 0;
            while (x != 0 || y != 0)
            {
                if (!(((x & 1) == 1 && (y & 1) == 1) || ((x & 1) == 0 && (y & 1) == 0)))
                {
                    count++;
                }

                x = x >> 1;
                y = y >> 1;
            }

            return count;

        }
		static void Main(string[] args)
		{

			//var t2 = HammingDistance(1, 4);

			StringOperations stringOperations = new StringOperations();

			var t1 = stringOperations.IsSortedByOrder(new string[]{"cc","cb","bb","ac"}, new char[]{'b','c','a'});
            
            

			//ArrayOperations arrayOperations = new ArrayOperations();

			//var res = arrayOperations.MinSubArrayLen(5, new int[] { 2, 3, 1, 1, 1, 1, 1 });

			//var res = new BackTracking().GenerateParenthesis(2);
			//int[,] a = new int[,]
			//{
			//	{1, 1, 0},
			//	{0, 1, 1},
			//	{0, 1, 1}
			//};
			//bool found = false;

			//DFS(a, 0, 0, "right", ref found);

			//TreeOperations treeOperations = new TreeOperations();

			//var root = treeOperations.BuildTree(new List<string> { "1", "1" });

			//TreeNode prev = null;
			//var res = treeOperations.ISBST(root, ref prev);

			//treeOperations.Preorder_Iterative(root);
        }

        /*
         * Robot in a maze, can move all 4 directions, 
         * print the path from left most corner to right most corner
         */
		public static void DFS(int[,] matrix, int r, int c, string dir, ref bool found)
		{
			int rows = matrix.GetLength(0);
			int colms = matrix.GetLength(1);

			if(r < 0 || r > rows - 1 || c < 0 || c > colms - 1 || matrix[r,c] == 0)
			{
				return;
			}

            if(r == rows - 1 && c == colms - 1)
			{
				found = true;
				return;
			}

			matrix[r, c] = 0;
			DFS(matrix, r, c + 1, "right", ref found);
			DFS(matrix, r + 1, c, "down", ref found);
			DFS(matrix, r - 1, c, "up", ref found);
            DFS(matrix, r, c - 1, "left", ref found);

            if(found)
			{
				Console.WriteLine("x : {0}, y : {1}, direction: {2}", r, c, dir);
			}

			matrix[r, c] = 1;
		}
    }
}
