using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice
{
    public class ArrayOperations
    {
        public int OrderArray(int[] arr)
        {
            int i = 0;
            int j = 0;
            while(i < arr.Length)
            {
                if(arr[i] != 0)
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

            while(i < arr.Length)
            {
                isMax = (i % 2) == 0 ? true : false;

                int index = GetMaxorMinIndex(arr, i, isMax);
                swap(arr, i, index);

                i++;
            }
        }

        public int GetMaxorMinIndex(int[] arr, int start, bool isMax)
        {
            int item = arr[start];
            int i = start;
            int itemIndex = start;

            while(i < arr.Length)
            {
                if((isMax && arr[i] > item) || (!isMax && arr[i] < item))
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
    }
}
