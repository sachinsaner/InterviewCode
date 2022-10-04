using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingPractice
{
    public class MedianFinder
    {

        List<int> low = new List<int>();
        List<int> hi = new List<int>();

        public MedianFinder()
        {

        }

        public void AddNum(int num)
        {
            low.Add(num);
            low.OrderByDescending(c => c);

            hi.Add(low[0]);

            low.RemoveAt(0);

            if (hi.Count > low.Count)
            {
                hi.OrderBy(c => c);
                low.Add(hi[0]);
                hi.RemoveAt(0);

            }

            low.OrderByDescending(c => c);
            hi.OrderBy(c => c);
        }

        public double FindMedian()
        {


            if (low.Count > hi.Count)
            {
                return low[0];
            }

            return (low[0] + hi[0]) / 2;


        }
    }

}
