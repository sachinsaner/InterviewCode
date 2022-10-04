using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class TrieOperations
    {
        private TrieNode root;

        public void BuildTrie(string[] words, TrieNode root)
        {
            TrieNode curr = root;
            this.root = root;

            foreach (var word in words)
            {
                foreach (var chr in word)
                {
                    if (!curr.Children.ContainsKey(chr))
                    {
                        curr.Children.Add(chr, new TrieNode { Chr = chr });
                    }
                    
                    curr = curr.Children[chr];
                }

                curr.IsWord = true;
                curr = root;
            }
        }

        public bool IsPresent(string word)
        {
            TrieNode curr = this.root;

            foreach (char chr in word)
            {
                if (curr.Children.ContainsKey(chr))
                {
                    curr = curr.Children[chr];
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        //Gets the last node of the pattern i.e. for sac it will return node containing c 
        private TrieNode GetLastNode(string word)
        {
            TrieNode curr = this.root;

            foreach (char chr in word)
            {
                if(curr.Children.ContainsKey(chr))
                {
                    curr = curr.Children[chr];
                }
                else
                {
                    return null;
                }
            }

            return curr;
        }

        //Tells if the end of tree branch from this node
        private bool IsLastWord(TrieNode node)
        {
            if(node.Children == null || node.Children.Count == 0)
            {
                return true;
            }

            return false;
        }

        public List<Tuple<string, int>> GetSuggestion(string word)
        {
            List<Tuple<string, int>> sol = new List<Tuple<string, int>>();

            var currNode = GetLastNode(word);

            if (currNode == null)
            {
                return sol;
            }

            GetSuggestionsUtil(currNode, word, ref sol);

            return sol;
        }

        private void GetSuggestionsUtil(TrieNode node, string word, ref List<Tuple<string, int>> sol)
        {
            if (node.IsWord)
            {
                sol.Add(Tuple.Create(word, node.RelevanceIndex));
            }

            if (this.IsLastWord(node))
            {
                return;
            }

            foreach(var child in node.Children)
            {
                GetSuggestionsUtil(child.Value, word + child.Value.Chr, ref sol);   
            }
        }
    }
}
