using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class SegmentTree
    {
        public int Start;
        public int End;

        public SegmentTree Left;
        public SegmentTree Right;

        public int Value;

        public SegmentTree(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }

        public SegmentTree BuildTree(int[] nums, int start, int end)
        {
            SegmentTree root = new SegmentTree(start, end);

            if(start == end)
            {              
                root.Value = nums[start];

                //IMP return here else it will be infine loop
                return root;
            }

            int mid = (start + end) / 2;

            root.Left = BuildTree(nums, start, mid);
            root.Right = BuildTree(nums, mid + 1, end);

            root.Value = root.Left.Value + root.Right.Value;

            //Find smallest number in the range
            //root.Value = Math.Min(root.Left.Value, root.Right.Value);

            return root;
        }

        public void Update(SegmentTree root, int index, int val)
        {
            if(root == null)
            {
                return;
            }
         
            if(root.Start == index && root.End == index)
            {
                root.Value = val;
                return;
            }

            int mid = (root.Start + root.End) / 2;

            if(index <= mid)
            {
                Update(root.Left, index, val);
            }
            else
            {
                Update(root.Right, index, val);
            }

            root.Value = root.Left.Value + root.Right.Value;

            //Find smallest number in the range
            //root.Value = Math.Min(root.Left.Value, root.Right.Value);
        }

        public int GetRange(SegmentTree root, int start, int end)
        {
            if(root == null)
            {
                return 0;
            }

            if(root.Start == start && root.End == end)
            {
                return root.Value;
            }

            int mid = (root.Start + root.End) / 2;

            if(start > mid)
            {
                return GetRange(root.Right, start, end);
            }
            if(end <= mid)
            {
                return GetRange(root.Left, start, end);
            }

            int leftRange = GetRange(root.Left, start, mid);
            int rightRange = GetRange(root.Right, mid + 1, end);
            int total = leftRange + rightRange;

            return total;

            //Find smallest number in the range
            //return Math.Min(leftRange, rightRange);
        }
    }
}
