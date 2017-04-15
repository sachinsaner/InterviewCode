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
         * output : 
         */
        public void PartitionArray(int[] arr, int k)
        {
            var sets = new List<PartitionSet>();

            for(int i = 0; i < k; i++)
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
    }
}
