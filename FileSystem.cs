using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class Node
    {
        public int val;
        public Dictionary<string, Node> children;

        public Node()
        {
            children = new Dictionary<string, Node>();
        }
    }

    public class FileSystem
    {

        Node root;

        public FileSystem()
        {
            root = new Node();
        }

        public bool CreatePath(string path, int value)
        {
            Node curr = root;
            var items = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < items.Length; i++)
            {
                if (curr.children.ContainsKey(items[i]))
                {
                    curr = curr.children[items[i]];
                }
                else
                {
                    if (i == items.Length - 1)
                    {
                        curr.children.Add(items[i], new Node() { val = value });
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public int Get(string path)
        {
            Node curr = root;
            var items = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            for(int i = 0; i < items.Length; i++)
            {
                if(curr.children.ContainsKey(items[i]))
                {
                    if(i == items.Length - 1)
                    {
                        return curr.children[items[i]].val;
                    }

                    curr = curr.children[items[i]];
                }

            }

            return -1;
        }
    }

    /**
     * Your FileSystem object will be instantiated and called as such:
     * FileSystem obj = new FileSystem();
     * bool param_1 = obj.CreatePath(path,value);
     * int param_2 = obj.Get(path);
     */
}
