namespace CodingPractice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ArrayOperations
    {
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
            if (input.Length < 2)
            {
                return;
            }

            Stack<int> stack = new Stack<int>();
            stack.Push(input[0]);

            int prev = stack.Peek();

            for (int i = 1; i < input.Length; i++)
            {
                int current = input[i];

                if (stack.Count > 0)
                {
                    prev = stack.Pop();

                    while (prev < current)
                    {
                        Console.WriteLine(prev + "->" + current);

                        if (stack.Count == 0) break;

                        prev = stack.Pop();
                    }

                    if (prev > current)
                    {
                        stack.Push(prev);
                    }
                }

                stack.Push(current);
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
                    if(key >= a[mid])
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
        public int findElement(int[] arr)
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

        public void PrintSpiralMatrix(int[,] a)
        {
            int rowCount = a.GetLength(0);
            int colCount = a.GetLength(1);

            int i = 0;
            int j = 0;
            while(i < rowCount)
            {
                while(j < colCount && i < rowCount)
                {
                    Console.Write(a[i,j++] + " "); 
                }

                i++;
                j--;
                while(j >= 0 && i < rowCount)
                {
                    Console.Write(a[i, j--] + " ");
                }
                i++;
                j = 0;
            }
        }

        int createPalindrome(int input, bool isOdd)
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

        // Fruition to print decimal palindromic number
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
    }
}
