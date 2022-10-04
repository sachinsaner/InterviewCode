using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class TrieNode
    {
        public Dictionary<char,TrieNode> Children
        {
            get;
            set;
        }

        public bool IsWord
        {
            get;
            set;
        }

        public char Chr
        {
            get;
            set;
        }

        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
            IsWord = false;
        }

        public int RelevanceIndex;
    }


    public class NarryNode
    {
        public List<NarryNode> Children
        {
            get;
            set;
        }

        public bool IsActive
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public NarryNode(string key, int value, bool isActive)
        {
            Children = new List<NarryNode>();
            IsActive = isActive;
            Value = value;
            this.Key = key;
        }
                
    }
}
