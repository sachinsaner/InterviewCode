using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice
{
    public class Interval :IComparable<Interval>
    {
        public int Start { get; set; }
        public int End { get; set; }

        public Interval(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }

        public int CompareTo(Interval other)
        {
            if (this.Start == other.Start)
                return 0;
            if (this.Start < other.Start)
                return -1;
            return 1;
        }
    }
}
