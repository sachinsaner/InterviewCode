using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice
{
    public class TreeNode
    {
        public int Value;

        public TreeNode Left = null;
        public TreeNode Right = null;

        public TreeNode(int value)
        {
            this.Value = value;
        }
    }
}
