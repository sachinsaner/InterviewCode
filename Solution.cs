using System;
using System.Collections.Generic;

public class Solution
{
    //int[] arr = { 3, 1, 6, 4, 5, 2 };
    public void MaxValue(int[] a, int n)
    {
        // Stores prefix sum
        int[] presum = new int[n];

        presum[0] = a[0];

        // Find the prefix sum array
        for (int i = 1; i < n; i++)
        {
            presum[i] = presum[i - 1] + a[i];
        }

        // l[] and r[] stores index of nearest smaller elements on left and right respectively              
        int[] l = new int[n], r = new int[n];
        var st = new Stack<int>();

        // Find all left index
        for (int i = 1; i < n; i++)
        {
            // Until stack is non-empty & top element is greater than the current element                         
            while (st.Count > 0 && a[st.Peek()] >= a[i])
            {
                st.Pop();
            }
            // If stack is empty
            if (st.Count > 0)
                l[i] = st.Peek() + 1;
            else
                l[i] = 0;

            // Push the current index i
            st.Push(i);
        }

        // Reset stack
        st.Clear();

        // Find all right index
        for (int i = n - 1; i >= 0; i--)
        {
            // Until stack is non-empty & top element is greater than the current element         
            while (st.Count > 0 && a[st.Peek()] >= a[i])
            {
                st.Pop();
            }

            if (st.Count > 0)
                r[i] = st.Peek() - 1;
            else
                r[i] = n - 1;

            // Push the current index i
            st.Push(i);
        }

        // Stores the maximum product
        int maxProduct = 0;

        int tempProduct;

        // Iterate over the range [0, n)
        for (int i = 0; i < n; i++)
        {
            // Calculate the product
            var right = presum[r[i]];
            int left = 0;
            if (l[i] != 0)
            {
                left = presum[l[i] - 1];
            }

            tempProduct = a[i] * (right - (l[i] == 0 ? 0 : left));
            //tempProduct = a[i] * (right - left);
            // Update the maximum product
            maxProduct = Math.Max(maxProduct, tempProduct);
        }

        // Return the maximum product
        Console.WriteLine(maxProduct);
    }
    //       { 0, 1, 2, 3, 4, 5 }   
    //      
    //arr =  { 3, 1, 6, 4, 5, 2 };

    //left = { 0, 0, 2, 2, 4, 2 }

    //right= { 0, 5, 2, 4, 4, 5 }

    //prefix={ 3, 4, 10,14,19,21}

    public int MaxSumMinProduct(int[] nums)
    {
        int[] prefix = new int[nums.Length + 1];
        int[] left = new int[nums.Length];
        int[] right = new int[nums.Length];

        for (int i = 1; i <= nums.Length; i++)
        {
            prefix[i] = nums[i - 1] + prefix[i - 1];
        }

        Stack<int> stack = new Stack<int>();

        for (int i = 0; i < nums.Length; i++)
        {
            while (stack.Count > 0 && nums[stack.Peek()] >= nums[i])
            {
                stack.Pop();
            }

            //why + 1 ? it is to exclude the num at stack.peek location since that breaks the
            // monotonically increasing sequence
            //
            left[i] = stack.Count == 0 ? -1 : stack.Peek(); 

            stack.Push(i);
        }

        stack.Clear();
        for(int i = nums.Length - 1; i >=0; i--)
        {
            if(stack.Count > 0 && nums[stack.Peek()] >= nums[i])
            {
                stack.Pop();
            }

            right[i] = stack.Count > 0 ? stack.Peek() : nums.Length;
            stack.Push(i);
        }

        //prefix[0] = nums[0];

      
        int ans = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            //int r = prefix[right[i]];
            //int l = prefix[left[i] + 1];
            ////int l = 0;
            ////if (left[i] != 0)
            ////{
            ////    l = prefix[left[i] - 1];
            ////}

            //int currProd = nums[i] * (r - l);

            int l = left[i];
            int r = right[i];
            int sum = prefix[r] - prefix[l + 1];
            var currProd = sum * nums[i];
            ans = Math.Max(ans, currProd);
        }
        return ans;
    }

    
}