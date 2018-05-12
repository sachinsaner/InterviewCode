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
}
