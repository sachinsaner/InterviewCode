using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class TrieOperations
    {
        public void BuildTrie(string[] words, TrieNode root)
        {
            TrieNode curr = root;

            foreach (var word in words)
            {
                foreach (var chr in word.ToCharArray())
                {
                    int index = chr - 'a';

                    if (curr.Children[index] == null)
                    {
                        curr.Children[index] = new TrieNode() { Chr = chr };
                    }

                    curr = curr.Children[index];
                }

                curr.IsWord = true;
                curr = root;
            }
        }

        public bool IsPresentInTrie(TrieNode root, string word)
        {
            TrieNode curr = root;

            foreach (char chr in word)
            {
                int index = chr - 'a';

                if (curr.Children[index] == null)
                {
                    return false;
                }

                curr = curr.Children[index];
            }

            return true;
        }

        //Gets the last node of the pattern i.e. for sac it will return node containing c 
        private TrieNode GetLastNode(TrieNode root, string word)
        {
            TrieNode curr = root;

            foreach (char chr in word)
            {
                int index = chr - 'a';

                if (curr.Children[index] == null)
                {
                    return null;
                }

                curr = curr.Children[index];
            }

            return curr;
        }

        //Tells if the end of tree branch from this node
        private bool IsLastWord(TrieNode node)
        {
            for (int i = 0; i < node.Children.Length; i++)
            {
                if (node.Children[i] != null)
                {
                    return false;
                }
            }

            return true;
        }

        public List<Tuple<string, int>> GetSuggestion(string word, TrieNode root)
        {
            List<Tuple<string, int>> sol = new List<Tuple<string, int>>();

            var currNode = GetLastNode(root, word);

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

            for (int i = 0; i < node.Children.Length; i++)
            {
                if (node.Children[i] != null)
                {
                    //word += node.Children[i].Chr;
                    GetSuggestionsUtil(node.Children[i], word + node.Children[i].Chr, ref sol);

                }
            }
        }

    }
}
