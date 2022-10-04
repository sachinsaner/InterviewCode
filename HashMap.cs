using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingPractice
{
    public class Node
    {
        public int key;
        public int value;

        public Node(int key, int val)
        {
            this.key = key;
            this.value = val;
        }
    }

    public class MyHashMap
    {
        int mod = 2069;
        List<LinkedList<Node>> map;

        public MyHashMap()
        {
            map = new List<LinkedList<Node>>();

            for(int i = 0; i <= mod; i++)
            {
                map.Add(new LinkedList<Node>());
            }

            

        }

        public void Put(int key, int value)
        {
            int index = key % mod;
            bool found = false;
            if (map[index] != null)
            {
                foreach (var node in map[index])
                {
                    if (node.key == key)
                    {
                        node.value = value;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    map[index].AddLast(new Node(key,value));
                }
            }
            else
            {
                
                map[index] = new LinkedList<Node>();                               
                map[index].AddLast(new LinkedListNode<Node>(new Node(key, value)){ });
            }
        }

        public int Get(int key)
        {
            var index = GetIndex(key);

            if(map[index] != null)
            {
                foreach(var node in map[index])
                {
                    if(node.key == key)
                    {
                        return node.value;
                    }
                }
            }

            return -1;
        }

        private int GetIndex(int key)
        {
            return key % mod;
        }

        public void Remove(int key)
        {
            var index = GetIndex(key);

            if(map[index] != null)
            {
                if(map[index].Count == 1 && map[index].First.Value.key == key)
                {
                    map[index] = null;
                }
                else
                {
                    foreach(var node in map[index])
                    {
                        if(node.key == key)
                        {
                            map[index].Remove(node);
                            break;
                        }
                    }
                }
            }

        }
    }
}
