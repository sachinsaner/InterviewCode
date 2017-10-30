namespace CodingPractice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ArrayOperations
    {
        private Dictionary<Tuple<int, int>, bool> memo = new Dictionary<Tuple<int, int>, bool>();

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
                if (a[low] <= a[mid]) //Low is less than mi
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

        // int arr[] = {23,10,22,5,33,8,9,21,50,41,60,80,99, 22,23,24,25,26,27};
        public int LongestIncreasingSubsequenceLength(int[] arr)
        {
            int[] resultArr = new int[arr.Length];
            int[] resultSequence = new int[arr.Length];

            int j = 0;
            resultArr[0] = 1;

            for (int i = 1; i < arr.Length; i++)
            {
                for (j = 0; j < i; j++)
                {
                    if (arr[i] > arr[j] && (resultArr[j] + 1 > resultArr[i]))
                    {
                        resultArr[i] = resultArr[j] + 1;

                        //Might be wrong line, trying print the numbers in sequnce
                        resultSequence[i] = arr[j];
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

            while(j < items.Count())
            {
                if(items[j].Start < items[i].End)
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

        public bool SubsetSumExists(int[] set, int n, int sum)
        {
            if(sum == 0)
            {
                return true;
            }

            if(n == 0 && sum != 0)
            {
                return false;
            }

            //if last element is greater than sum then ignore it 
            if (set[n - 1] > sum)
            {
                SubsetSumExists(set, n - 1, sum);
            }

            bool memoVal;
            if (!memo.TryGetValue(Tuple.Create(n, sum), out memoVal))
            {
                var res = SubsetSumExists(set, n - 1, sum) || SubsetSumExists(set, n - 1, sum - set[n - 1]);
                memo[Tuple.Create(n, sum)] = res;

                return res;
            }
            else
            {
                return memoVal;
            }
        }

        public void SubsetSumExists2(int[] set, int index, int currSum, int sum, int[] sol)
        {
            if(sum == currSum)
            {
                Console.WriteLine("\nSum found");
                for (int i = 0; i < sol.Length; i++)
                {
                    if (sol[i] == 1)
                    {
                        Console.WriteLine("  " + set[i]);
                    }
                }
            }

            if(index == set.Length)
            {
                return;
            }

            currSum += set[index];
            sol[index] = 1;
            SubsetSumExists2(set, index + 1, currSum, sum, sol);

            //ignore current element
            currSum -= set[index];
            sol[index] = 0;
            SubsetSumExists2(set, index + 1, currSum, sum, sol);
        }
    }
}
