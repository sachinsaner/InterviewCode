using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class RandomizedCollection
    {

        Dictionary<int, HashSet<int>> map = null;
        List<int> list = null;
        Random r;

        public RandomizedCollection()
        {
            map = new Dictionary<int, HashSet<int>>();
            list = new List<int>();
            r = new Random();
        }

        public bool Insert(int val)
        {
            list.Add(val);

            if (!map.ContainsKey(val))
            {
                map[val] = new HashSet<int>() { list.Count - 1 };
                return true;
            }

            map[val].Add(list.Count - 1);
            return false;

        }

        public bool Remove(int val)
        {
            if (!map.ContainsKey(val))
            {
                return false;
            }

            int index = map[val].GetEnumerator().Current;
            map[val].Remove(index);
            if (map[val].Count == 0)
            {
                map.Remove(val);
            }

            if (index == list.Count - 1)
            {
                list.RemoveAt(index);
            }
            else
            {
                int lastElem = list[list.Count - 1];
                list[index] = lastElem;
                list.RemoveAt(list.Count - 1);

                //remove last num index from map
                map[lastElem].Remove(list.Count);
                map[lastElem].Add(index);
            }

            return true;
        }

        public int GetRandom()
        {
            int index = r.Next(list.Count);
            return list[index];
        }
    }
}
