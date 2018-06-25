namespace CodingPractice
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class ArrayOperations
	{
		//https://leetcode.com/problems/longest-increasing-path-in-a-matrix/description/
		/*
         * Input: nums = 
            [
              [9,9,4],
              [6,6,8],
              [2,1,1]
            ] 
            Output: 4 
            Explanation: The longest increasing path is [1, 2, 6, 9].
        */
		public static int LongestIncreasingPath(int[,] matrix)
		{
			int maxCount = 0;

			int[,] cache = new int[matrix.GetLength(0), matrix.GetLength(1)];

			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					int c = DFS(matrix, i, j, cache, int.MaxValue);

					maxCount = Math.Max(c, maxCount);
				}
			}

			return maxCount;
		}

		private static int DFS(int[,] matrix, int x, int y, int[,] cache, int prev)
		{
			int ROWS = matrix.GetLength(0);
			int COLS = matrix.GetLength(1);

			if (x < 0 || x >= ROWS || y < 0 || y >= COLS || matrix[x, y] >= prev)
			{
				return 0;
			}
			if (cache[x, y] > 0)
			{
				return cache[x, y];
			}


			int a = DFS(matrix, x + 1, y, cache, matrix[x, y]) + 1;
			int b = DFS(matrix, x, y + 1, cache, matrix[x, y]) + 1;
			int c = DFS(matrix, x - 1, y, cache, matrix[x, y]) + 1;
			int d = DFS(matrix, x, y - 1, cache, matrix[x, y]) + 1;

			int m = Math.Max(a, Math.Max(b, Math.Max(c, d)));
			cache[x, y] = m;
			return m;
		}


		public int[] MaxSlidingWindow(int[] nums, int k)
		{
			var q = new LinkedList<int>();
			int i = 0;
			var result = new List<int>();

			for (i = 0; i < k; i++)
			{
				while (q.Count > 0 && nums[q.Last()] < nums[i])
				{
					q.RemoveLast();
				}

				q.AddLast(i);
			}

			for (; i < nums.Length; ++i)
			{
				result.Add(nums[q.First()]);

				while (q.Count > 0 && i - k >= q.First())
				{
					q.RemoveFirst();
				}

				while (q.Count > 0 && nums[q.Last()] < nums[i])
				{
					q.RemoveLast();
				}

				q.AddLast(i);
			}

			if (q.Count > 0)
			{
				result.Add(nums[q.First()]);
			}

			return result.ToArray();
		}

		public int numberOfPaths(int row, int column)
		{
			int[] paths = new int[row];
			paths[row - 1] = 1; //start from bottom-left corner
			for (int col = 1; col < column; col++)
			{
				int upper_left_value = 0;
				for (int r = 0; r < row; r++)
				{
					int left_value = paths[r];
					paths[r] += upper_left_value + (r == row - 1 ? 0 : paths[r + 1]);
					upper_left_value = left_value;
				}
			}

			return paths[row - 1]; //exit from bottom-right
		}


		//https://leetcode.com/problems/remove-duplicates-from-sorted-array-ii/description/
		/*
         * Given a sorted array nums, remove the duplicates in-place such that duplicates appeared at most twice and return the new length.

            Do not allocate extra space for another array, you must do this by modifying the input array in-place with O(1) extra memory.

            Example 1:

            Given nums = [1,1,1,2,2,3],

            Your function should return length = 5, with the first five elements of nums being 1, 1, 2, 2 and 3 respectively.

            It doesn't matter what you leave beyond the returned length.
         */
		public int RemoveDuplicates2(int[] nums)
		{
			int start = 0;

			for (int i = 0; i < nums.Length; i++)
			{
				int currNum = nums[i];
				int j = i;
				int count = 0;

				while (j < nums.Length && nums[j] == currNum)
				{
					j++;
					count++;
				}

				if (count >= 2)
				{
					nums[start++] = nums[i];
					nums[start++] = nums[i + 1];
				}
				else
				{
					nums[start++] = nums[i];
				}

				i = j - 1;
			}

			return start;
		}

		public int RemoveDuplicates(int[] nums)
		{
			int start = 0;

			for (int i = 0; i < nums.Length; i++)
			{
				int currNum = nums[i];
				int j = i;
				while (j < nums.Length && nums[j] == currNum)
				{
					j++;
				}

				nums[start++] = nums[i];

				//i will increament in next iteration for loop hence j-1
				i = j - 1;
			}

			return start + 1;
		}

		//Given an array with input - [1,2,3,4,5] , [1,3,4,5,7]
		//Program should output [1-5],[1-1,3-5,7-7]  
		public List<string> PrintRange(List<List<int>> input)
		{
			var result = new List<string>();

			foreach (var list in input)
			{
				if (list.Count == 1)
				{
					result.Add(list[0] + "->" + list[0]);
					//Console.WriteLine(list[0] + "-" + list[0]);
					continue;
				}

				int startItemIndex = 0;

				for (int i = 1; i < list.Count; i++)
				{
					if (list[i] - list[i - 1] > 1)
					{
						result.Add(list[startItemIndex] + "->" + list[i - 1]);
						//Console.WriteLine(list[startItemIndex] + "-" + list[i - 1]);
						startItemIndex = i;
					}
				}

				if (startItemIndex <= list.Count - 1)
				{
					result.Add(list[startItemIndex] + "->" + list[list.Count - 1]);
					//Console.WriteLine(list[startItemIndex] + "-" + list[list.Count - 1]);
				}
			}

			return result;
		}

		public int MaximalRectangle(char[,] matrix)
		{
			if (matrix == null)
			{
				return 0;
			}

			int[] row = new int[matrix.GetLength(1)];
			int maxArea = 0;

			for (int i = 0; i < row.Length; i++)
			{
				row[i] = int.Parse(matrix[0, i].ToString());
			}

			maxArea = LargestRectangleArea(row);

			for (int i = 1; i < matrix.GetLength(0); i++)
			{
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (matrix[i, j] == '1')
					{
						row[j] += int.Parse(matrix[i, j].ToString());
					}
					else
					{
						row[j] = 0;
					}
				}

				int currArea = LargestRectangleArea(row);
				maxArea = Math.Max(maxArea, currArea);
			}

			return maxArea;
		}

		public int LargestRectangleArea(int[] heights)
		{
			var stack = new Stack<int>();
			int maxArea = 0;
			int currentBarIndex = 0;
			int currentArea = 0;

			int i = 0;
			while (i < heights.Length)
			{
				if (stack.Count == 0 || heights[stack.Peek()] <= heights[i])
				{
					stack.Push(i++);
				}
				else
				{
					while (stack.Count > 0 && heights[stack.Peek()] >= heights[i])
					{
						currentBarIndex = stack.Pop();

						// Here the current poped element is the smallest element in consideration
						// we need to consider all element to its right which are greater than it
						// and all element to its left till element which are smaller than it 
						// on the left prev element on the stack is smallest element than this element
						// as we only push element in ascending order on the stack

						/// -1 is to eliminate the stack.peek from consideration as its smaller than currentIndex
						/// becaue s.peek() could be smaller than currentIndex
						/// ex. 5,6,15,7
						/// if current is 7 we poped 15 and stack top is 6 which we need to eliminate
						int multiple = stack.Count == 0 ? i : i - (stack.Peek() - 1);

						currentArea = heights[currentBarIndex] * multiple;

						maxArea = Math.Max(maxArea, currentArea);
					}
				}
			}

			while (stack.Count > 0)
			{
				currentBarIndex = stack.Pop();

				currentArea = heights[currentBarIndex] * (stack.Count == 0 ? i : i - stack.Peek() - 1);

				maxArea = Math.Max(maxArea, currentArea);
			}

			return maxArea;
		}

		public int GetRightRange(int[] nums, int target)
		{
			int start = 0;
			int end = nums.Length - 1;

			while (start <= end)
			{
				int mid = (start + end) / 2;

				if ((mid == nums.Length - 1 || nums[mid + 1] > target) && nums[mid] == target)
				{
					return mid;
				}
				else if (nums[mid] <= target)
				{
					start = mid + 1;
				}
				else
				{
					end = mid - 1;
				}
			}

			return -1;
		}

		//https://leetcode.com/problems/product-of-array-except-self/description/
		public int[] ProductExceptSelf2(int[] nums)
		{
			int[] res = new int[nums.Length];

			//init res[0] = 1 becasue we need to skip element at location of i
			res[0] = 1;
			int prod = nums[0];

			//populate res such a that res[i] = product of all element from 0 to i-1
			//in= {1,2,3}
			//res = {1,1,2}
			for (int i = 1; i < nums.Length; i++)
			{
				res[i] = prod;
				prod *= nums[i];
			}

			//start multiplying res[i] with prod which product of all elements from i + 1 to n
			prod = nums[nums.Length - 1];

			for (int j = nums.Length - 2; j >= 0; j--)
			{
				res[j] *= prod;
				prod *= nums[j];

			}

			return res;
		}

		//A = [2,3,1,4,1,0,0,1,2,1]
		//A = [2, 3, 1, 1, 4]
		//given jumps at every index can we reach to the last index
		public bool CanJump(int[] nums)
		{
			int max = 0;

			for (int i = 0; i < nums.Length; i++)
			{
				if (i > max)
				{
					return false;
				}

				//max distance we can go from current index
				max = Math.Max(nums[i] + i, max);
			}

			return true;
		}

		//Given array A = [2,3,1,1,4]
		//The minimum number of jumps to reach the last index is 2. 
		//(Jump 1 step from index 0 to 1, then 3 steps to the last index.)
		//https://leetcode.com/problems/jump-game-ii/description/
		public int JumpGame2(int[] A)
		{
			int step_count = 0;
			int last_jump_max = 0;
			int current_jump_max = 0;

			for (int i = 0; i < A.Length - 1; i++)
			{
				current_jump_max = Math.Max(current_jump_max, i + A[i]);

				if (i == last_jump_max)
				{
					step_count++;
					last_jump_max = current_jump_max;
				}
			}
			return step_count;
		}


		public int MinPathSum(int[,] grid)
		{

			int m = grid.GetLength(0);// row
			int n = grid.GetLength(1); // column
			for (int i = 0; i < m; i++)
			{
				for (int j = 0; j < n; j++)
				{
					if (i == 0 && j != 0)
					{
						grid[i, j] = grid[i, j] + grid[i, j - 1];
					}
					else if (i != 0 && j == 0)
					{
						grid[i, j] = grid[i, j] + grid[i - 1, j];
					}
					else if (i == 0 && j == 0)
					{
						grid[i, j] = grid[i, j];
					}
					else
					{
						grid[i, j] = Math.Min(grid[i, j - 1], grid[i - 1, j]) + grid[i, j];
					}
				}
			}

			return grid[m - 1, n - 1];
		}

		//initial : [1, 2, 0, 3], small = MAX, big = MAX
		//loop1 : [1, 2, 0, 3], small = 1, big = MAX
		//loop2 : [1, 2, 0, 3], small = 1, big = 2
		//loop3 : [1, 2, 0, 3], small = 0, big = 2 // <- Uh oh, 0 technically appeared after 2
		//loop4 : return true since 3 > small && 3 > big // Isn’t this a violation??

		//If you observe carefully, the moment we updated big from MAX to some other value, 
		//that means that there clearly was a value less than it(which would have been assigned to small in the past). 
		//What this means is that once you find a value bigger than big, you’ve implicitly found an increasing triplet.
		public bool IncreasingTriplet(int[] nums)
		{
			// start with two largest values, as soon as we find a number bigger than both, while both have been updated, return true.
			int small = int.MaxValue, big = int.MaxValue;
			foreach (var n in nums)
			{
				if (n <= small)
				{
					small = n;
				} // update small if n is smaller than both
				else if (n <= big)
				{
					big = n;
				} // update big only if greater than small but smaller than big
				else
				{
					return true; // return if you find a number bigger than both
				}
			}
			return false;
		}

		//     [1],
		//    [1,1],
		//   [1,2,1],
		//  [1,3,3,1],
		// [1,4,6,4,1]

		public IList<IList<int>> GeneratePascalTriangle(int numRows)
		{
			var res = new List<IList<int>>();
			res.Add(new List<int>() { 1 });

			numRows--;

			while (numRows > 0)
			{
				var lastRow = res.Last();
				var newRow = new List<int>();

				newRow.Add(lastRow.First());

				for (int i = 1; i < lastRow.Count; i++)
				{
					newRow.Add(lastRow[i] + lastRow[i - 1]);
				}

				newRow.Add(lastRow.Last());
				res.Add(newRow);
				numRows--;

			}

			return res;
		}

		//Arrange elements in an array such that non-zero elements on 
		//left and zeros on right
		public int OrderArray(int[] arr)
		{
			int i = 0;
			int j = 0;
			while (i < arr.Length)
			{
				if (arr[i] != 0)
				{
					swap(arr, i, j);
					j++;
				}
				i++;
			}

			return j;
		}

		/*
         * Sort an un sorted array such that every alternate element is max and min 
         *  input { 3, 1, 5, 2, 6, 4 }
         *  output {6, 1, 5, 2, 4, 3}
         */
		public void SortAlternate(int[] arr)
		{
			int i = 0;
			bool isMax = false;

			while (i < arr.Length)
			{
				isMax = (i % 2) == 0 ? true : false;

				int index = GetMaxorMinIndex(arr, i, isMax);
				swap(arr, i, index);

				i++;
			}
		}

		private int GetMaxorMinIndex(int[] arr, int start, bool isMax)
		{
			int item = arr[start];
			int i = start;
			int itemIndex = start;

			while (i < arr.Length)
			{
				if ((isMax && arr[i] > item) || (!isMax && arr[i] < item))
				{
					itemIndex = i;
					item = arr[i];
				}

				i++;
			}

			return itemIndex;
		}

		private void swap(int[] arr, int i, int j)
		{
			int temp = arr[i];
			arr[i] = arr[j];
			arr[j] = temp;
		}

		/*
         * Partition sorted array into k sets with almost equal sum  
         * input : int[] arr = { 1, 3, 6, 9, 10 } k = 3
         * output : {10} {9,1} {6,3}
         */
		public void PartitionArray(int[] arr, int k)
		{
			var sets = new List<PartitionSet>();

			for (int i = 0; i < k; i++)
			{
				sets.Add(new PartitionSet());
			}

			int index = arr.Length - 1;

			while (index >= 0)
			{
				this.AddToSet(arr[index--], sets);
			}

			foreach (var set in sets)
			{
				set.PrintSet();
				Console.WriteLine();
			}
		}

		private void AddToSet(int element, List<PartitionSet> sets)
		{
			//Find set with lowest sum
			int minSum = sets.FirstOrDefault().CurrentSum;
			int minSumSetIndex = 0;
			int index = 0;
			foreach (var set in sets)
			{
				if (set.CurrentSum < minSum)
				{
					minSum = set.CurrentSum;
					minSumSetIndex = index;
				}

				index++;
			}

			sets[minSumSetIndex].Add(element);
		}

		public void PrintNextGreaterElement(int[] input)
		{
			Stack<int> stack = new Stack<int>();

			stack.Push(input[0]);

			for (int i = 1; i < input.Length; i++)
			{
				int prev = stack.Peek();

				if (prev <= input[i])
				{
					//Pop all elements from stack smaller than current element
					while (prev < input[i])
					{
						prev = stack.Pop();
						Console.WriteLine(prev + "->" + input[i]);

						if (stack.Count == 0)
						{
							break;
						}

						prev = stack.Peek();
					}
					//Push current element on to stack
					stack.Push(input[i]);
				}
				else
				{
					stack.Push(input[i]);
				}
			}

			while (stack.Count > 0)
			{
				int element = stack.Pop();
				Console.WriteLine(element + "-> -1");
			}
		}

		public int SearchRotatedArray(int[] a, int key)
		{
			int low = 0;
			int high = a.Length - 1;
			int mid = (low + high) / 2;

			while (low <= high)
			{
				mid = (low + high) / 2;

				if (a[mid] == key)
				{
					return mid;
				}
				//int[] arr = { 11, 12, 13, 1, 2, 3, 4 };
				//int[] arr = { 5,20,30,40,50 };
				/*Algo:
                 * 1. Check first if the half is in right state
                 * 2. and key lies with the range of that half
                 * 3. else key lies in the other half
                 */

				if (a[low] <= a[mid])
				{
					if (key >= a[low] && key <= a[mid])
					{
						high = mid - 1;
					}
					else
					{
						low = mid + 1;
					}
				}
				else if (a[mid] <= a[high])
				{
					if (key >= a[mid] && key <= a[high])
					{
						low = mid + 1;
					}
					else
					{
						high = mid - 1;
					}
				}
			}

			return -1;
		}

		public int SearchIncreasingDecreasingArray(int[] a, int key)
		{
			int low = 0;
			int high = a.Length - 1;
			int mid = (low + high) / 2;

			while (low <= high)
			{
				mid = (low + high) / 2;

				if (a[mid] == key)
				{
					return mid;
				}

				if (a[low] <= a[mid])
				{
					if (key >= a[low] && key <= a[mid])
					{
						high = mid - 1;
					}
					else
					{
						low = mid + 1;
					}
				}
				else if (a[low] >= a[mid] && a[mid] >= a[high])
				{
					if (key >= a[mid])
					{
						high = mid - 1;
					}
					else
					{
						low = mid + 1;
					}
				}
			}

			return -1;
		}

		// program to find the element which is greater than
		// all left elements and smaller than all right elements.

		//Input:   arr[] = {5, 1, 4, 3, 6, 8, 10, 7, 9};
		//Output:  Index of element is 4
		//Input:   arr[] = {5, 1, 4, 4};
		//Output:  Index of element is -1
		public int FindElement(int[] arr)
		{
			// leftMax[i] stores maximum of arr[0..i-1]
			int[] leftMax = new int[arr.Length];
			leftMax[0] = -1;

			// Fill leftMax[]1..n-1]
			for (int i = 1; i < arr.Length; i++)
				leftMax[i] = Math.Max(leftMax[i - 1], arr[i - 1]);

			// Initialize minimum from right
			int rightMin = 9999;

			// Traverse array from right
			for (int i = arr.Length - 1; i >= 0; i--)
			{
				// Check if we found a required element
				if (leftMax[i] < arr[i] && rightMin > arr[i])
					return i;

				// Update right minimum
				rightMin = Math.Min(rightMin, arr[i]);
			}

			// If there was no element matching criteria
			return -1;
		}

		public void PrintZigZagMatrix(int[,] a)
		{
			int rowCount = a.GetLength(0);
			int colCount = a.GetLength(1);

			int i = 0;
			int j = 0;
			while (i < rowCount)
			{
				while (j < colCount && i < rowCount)
				{
					Console.Write(a[i, j++] + " ");
				}

				i++;
				j--;
				while (j >= 0 && i < rowCount)
				{
					Console.Write(a[i, j--] + " ");
				}
				i++;
				j = 0;
			}
		}

		public void PrintSpiralMatrix(int[,] a)
		{
			int size = a.GetLength(0);
			int rowStart = 0;
			int colStart = 0;
			int rowEnd = size - 1;
			int colEnd = size - 1;

			while (rowStart <= rowEnd && colStart <= colEnd)
			{
				//Go right
				for (int j = colStart; j <= colEnd; j++)
				{
					Console.WriteLine(a[rowStart, j]);
				}
				//eliminate start row
				rowStart++;

				//go down
				for (int i = rowStart; i <= rowEnd; i++)
				{
					Console.WriteLine(a[i, colEnd]);
				}
				//eliminate last col
				colEnd--;

				//go left
				for (int j = colEnd; j >= colStart; j--)
				{
					Console.WriteLine(a[rowEnd, j]);
				}
				//eliminat last row
				rowEnd--;

				//go up
				for (int i = rowEnd; i >= rowStart; i--)
				{
					Console.WriteLine(a[i, colStart]);
				}
				//eliminate start col
				colStart++;
			}
		}

		private int createPalindrome(int input, bool isOdd)
		{
			//int n = input;
			int palin = input;
			int decimalBase = 10;

			// checks if number of digits is odd or even
			// if odd then neglect the last digit of input in
			// finding reverse as in case of odd number of
			// digits middle element occur once
			if (isOdd)
				input = input / decimalBase;

			// Creates palindrome by just appending reverse
			// of number to itself
			while (input > 0)
			{
				palin = palin * decimalBase + (input % decimalBase);
				input = input / decimalBase;
			}
			return palin;
		}

		// Function to print decimal palindromic number
		public void GeneratePaldindromes(int n)
		{
			int number;

			// Run two times for odd and even length palindromes
			for (int j = 0; j < 2; j++)
			{
				// Creates palindrome numbers with first half as i. 
				// Value of j decided whether we need an odd length
				// of even length palindrome.
				int i = 12;
				while ((number = createPalindrome(i, j % 2 == 0 ? true : false)) < n)
				{
					Console.WriteLine(number);
					i++;
				}
			}
		}

		public void BuildHeap(int[] a)
		{
			int length = a.Length;
			int i = 0;
			int root = a[i];
			int leftChild = 2 * i + 1;
			int rightChild = 2 * i + 2;

			while (i <= length)
			{
				root = i;
				leftChild = 2 * i + 1;
				rightChild = 2 * i + 2;

				if (leftChild < length && a[leftChild] > a[root])
				{
					root = leftChild;
				}
				if (rightChild < length && a[rightChild] > a[root])
				{
					root = rightChild;
				}

				if (root != i)
				{
					swap(i, root, a);
				}

				i++;
			}
		}

		private void swap(int i, int j, int[] a)
		{
			int temp = a[i];
			a[i] = a[j];
			a[j] = temp;
		}

		//Input: arr[] = [0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1]
		//Output: 6
		public int TapRainWater(int[] towerHeight)
		{
			int right = towerHeight.Length - 1;
			int left = 0;
			int maxLeft = 0;
			int maxRight = 0;
			int result = 0;

			while (left <= right)
			{
				if (towerHeight[left] <= towerHeight[right])
				{
					if (towerHeight[left] >= maxLeft)
					{
						maxLeft = towerHeight[left];
					}

					result += maxLeft - towerHeight[left];
					left++;
				}
				else
				{
					if (towerHeight[right] >= maxRight)
					{
						maxRight = towerHeight[right];
					}
					result += maxRight - towerHeight[right];

					right--;
				}
			}

			return result;
		}

		// A stack based efficient method to calculate stock span values
		public void CalculateSpan(int[] price)
		{
			// Create a stack and push index of first element to it
			Stack<int> st = new Stack<int>();
			st.Push(0);

			// Span value of first element is always 1
			int[] S = new int[price.Length];
			S[0] = 1;

			// Calculate span values for rest of the elements
			for (int i = 1; i < price.Length; i++)
			{
				// Pop elements from stack while stack is not empty and top of
				// stack is smaller than price[i]
				while (!(st.Count() == 0) && price[st.Peek()] <= price[i])
					st.Pop();

				// If stack becomes empty, then price[i] is greater than all elements
				// on left of it, i.e., price[0], price[1],..price[i-1].  Else price[i]
				// is greater than elements after top of stack
				S[i] = (st.Count() == 0) ? (i + 1) : (i - st.Peek());

				// Push this element to stack
				st.Push(i);
			}
		}

		public IList<Interval> MergeIntervals(IList<Interval> intervals)
		{

			if (intervals.Count == 0)
			{
				return intervals;
			}

			var result = new List<Interval>();
			var items = intervals.ToList();

			//items.Sort(delegate (Interval c1, Interval c2) { return c1.Start.CompareTo(c2.Start); });

			items.OrderBy(x => x.Start);

			int start = items[0].Start;
			int end = items[0].End;

			foreach (var interval in items)
			{
				//overlapping interval
				if (interval.Start <= end)
				{
					end = Math.Max(end, interval.End);
				}
				else
				{
					result.Add(new Interval(start, end));
					start = interval.Start;
					end = interval.End;
				}
			}

			result.Add(new Interval(start, end));
			return result;
		}

		public int EraseOverlapIntervals(Interval[] intervals)
		{
			int result = 0;

			if (intervals == null || intervals.Count() == 1)
			{
				return 0;
			}

			var items = intervals.ToList();
			items.Sort(delegate (Interval c1, Interval c2) { return c1.Start.CompareTo(c2.Start); });

			int i = 0;
			int j = 1;

			while (j < items.Count())
			{
				if (items[j].Start < items[i].End)
				{
					result++;
					j++;
				}
				else
				{
					i = j;
					j++;
				}
			}

			return result;
		}

		public void SortColors(int[] nums)
		{
			int low = 0;
			int high = nums.Length - 1;
			int mid = 0;

			while (mid <= high)
			{
				if (nums[mid] == 0)
				{
					Swap(mid, low, ref nums);
					low++;
					mid++;
                }
				else if (nums[mid] == 1)
				{
					mid++;
                }
				else if (nums[mid] == 2)
				{
					Swap(mid, high, ref nums);
					high--;
				}
			}
        }

		private void Swap(int i, int j, ref int[] nums)
		{
			int temp = nums[i];
			nums[i] = nums[j];
			nums[j] = temp;
		}

		public void MoveZeroes(int[] nums)
		{
			int start = 0;
			int index = start;

			while (start < nums.Length)
			{
				while (index < nums.Length && nums[index] != 0)
				{
					index++;
				}

				if (index < nums.Length && nums[start] != 0 && nums[index] == 0 && index < start)
				{
					Swap(start, index, ref nums);
				}
				start++;
			}
		}

		//10, 20, 15, 2, 23, 90, 67
		// peak 20,90
		//If middle element is not smaller than any of its neighbors, then we return it.
		//If the middle element is smaller than the its left neighbor, then there is always a peak in left half(Why? take few examples). 
		//If the middle element is smaller than the its right neighbor, then there is always a peak in right half(due to same reason as left half). 

		public int FindPeakElement(int[] arr)
		{
			int low = 0;
			int mid = 0;
			int high = arr.Length - 1;

			while (low < high)
			{
				mid = (low + high) / 2;

				if (arr[mid] < arr[mid + 1])
				{
					low = mid + 1;
				}
				else
				{
					high = mid;
				}
			}
			return low;
		}

		public void FindMissingNumber(int[] nums)
		{
			int xor1 = 0;
			int xor2 = nums[0];

			for (int i = 0; i < nums.Length; i++)
			{
				xor1 = xor1 ^ i;
				xor2 = xor2 ^ nums[i];
			}

			Console.WriteLine("missing num:{0}", (xor1 ^ xor2));
		}

		public int LengthOfLIS(int[] nums)
		{
			int[] tails = new int[nums.Length];
			int size = 0;

			foreach (int x in nums)
			{
				int i = 0;
				int j = size;

				while (i != j)
				{
					int m = (i + j) / 2;

					if (tails[m] < x)
						i = m + 1;
					else
						j = m;
				}

				tails[i] = x;

				if (i == size)
				{
					++size;
				}
			}

			return size;
		}


		//* Iterative Function to calculate(x^y) in O(logy) */
		public int Power(int x, int y)
		{
			int res = 1;     // Initialize result

			while (y > 0)
			{
				// If y is odd, multiply x with result
				if ((y & 1) == 1)
					res = res * x;

				// n must be even now
				y = y >> 1; // y = y/2
				x = x * x;  // Change x to x^2
			}
			return res;
		}

		/*
         * Implement next permutation, which rearranges numbers into the lexicographically next greater permutation of numbers.
            If such arrangement is not possible, it must rearrange it as the lowest possible order (ie, sorted in ascending order).

            The replacement must be in-place, do not allocate extra memory.

            Here are some examples. Inputs are in the left-hand column and its corresponding outputs are in the right-hand column.
            1,2,3 → 1,3,2
            3,2,1 → 1,2,3
            1,1,5 → 1,5,1
        */
		public void NextPermutation(int[] nums)
		{

			if (nums == null || nums.Length == 0 || nums.Length == 1)
			{
				return;
			}
			int pivote = int.MinValue;

			//https://www.nayuki.io/page/next-lexicographical-permutation-algorithm
			//input : 0, 1, 2, 5, 3, 3, 0
			//output: 0, 1, 3, 0, 2, 3, 5

			//input : 0, 1, 2, 5, 3, 3, 0
			//Find a pair from right such a that a[i] < a[i+1]
			// i.e  2, 5
			for (int i = nums.Length - 2; i >= 0; i--)
			{
				if (nums[i] < nums[i + 1])
				{
					pivote = i;
					break;
				}
			}

			//from right find the next element that is just greater than 2 
			// which 3, swap 2 ,3 => 0, 1, 3, 5, 3, 2, 0
			for (int j = nums.Length - 1; pivote != int.MinValue && j > pivote; j--)
			{
				if (nums[j] > nums[pivote])
				{
					swap(nums, pivote, j);
					break;
				}
			}

			//now reverse all the element to right of pivote, as we have choosen pivote
			//such that all the elements to right of it are greater than it, hence reversing them
			// will give us the sortest sequence
			if (pivote != int.MinValue)
			{
				reverse(nums, pivote + 1);
			}
			else
			{
				reverse(nums, 0);
			}
		}

		void reverse(int[] a, int start)
		{
			int end = a.Length - 1;

			while (start < end)
			{
				swap(a, start++, end--);
			}
		}
	}
}
