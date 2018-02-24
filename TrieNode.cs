using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class TrieNode
    {
        public TrieNode[] Children
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
            Children = new TrieNode[26];
            IsWord = false;
        }
    }
}
