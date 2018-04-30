namespace CodingPractice
{
    public class TreeNode
    {
        public int Value;

        public TreeNode Left = null;
        public TreeNode Right = null;

        public int LeftChildCount;

        public TreeNode(int value)
        {
            this.Value = value;
        }
    }
}
