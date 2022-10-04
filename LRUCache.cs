using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class CacheNode
    {
        public int Key { get; set; }
        public int Value { get; set; }
    }

    public class LRUCache
    {
        private LinkedList<CacheNode> list;
        private int capacity;
        Dictionary<int, LinkedListNode<CacheNode>> map = new Dictionary<int, LinkedListNode<CacheNode>>();


        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            list = new LinkedList<CacheNode>();
        }

        public int Get(int key)
        {
            if(map.ContainsKey(key))
            {
                MoveToFront(key);

                return map[key].Value.Value;
            }

            return -1;
        }

        public void Put(int key, int value)
        {
            if (!map.ContainsKey(key))
            {
                LinkedListNode<CacheNode> node;
                if (map.Count >= capacity)
                {
                    node = list.Last;
                    map.Remove(node.Value.Key);
                    list.RemoveLast();
                }

                node = new LinkedListNode<CacheNode>(new CacheNode() {Key = key, Value = value });
                map[key] = node;
                list.AddFirst(node);
            }
            else
            {
                // if the key is already present then replace the value and move to front
                map[key].Value.Value = value;
                MoveToFront(key);                            
            }
        }

        private void MoveToFront(int key)
        {
            var node = map[key];
            list.Remove(node);
            list.AddFirst(node);
        }
    }
}
