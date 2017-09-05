using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingPractice
{
    public class ListNode
    {
        public int Value { get; set; }
        public ListNode Next { get; set; }

        public ListNode(int value)
        {
            this.Value = value;
            this.Next = null;
        }

        public void AppendNode(ListNode node)
        {
            this.Next = node;
        }

        public void ReverseList(ListNode head, ref ListNode prev, ref ListNode newHead)
        {
            if(head == null)
            {
                return;
            }

            newHead = head;

            ReverseList(head.Next, ref head,ref newHead);

            head.Next = prev;

            //if(head != null)
            //{
            //    newHead = head;
            //    newHead = newHead.Next;
            //}
        }
    }
}
