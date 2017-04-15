using System;
namespace CodingPractice
{
    using System.Collections.Generic;

    public class PartitionSet
    {
        public int CurrentSum { get; set; }

        public List<int> Set { get; }

        public PartitionSet()
        {
            this.CurrentSum = 0;
            this.Set = new List<int>();
        }

        public void Add(int element)
        {
            this.Set.Add(element);
            this.CurrentSum += element;
        }

        public void PrintSet()
        {
            foreach(var element in Set)
            {
                Console.Write(element+ "  ");
            }
        }
    }
}
