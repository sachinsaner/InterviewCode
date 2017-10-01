using System;

namespace CodingPractice
{
    public class ListNode
    {
        public int Value { get; set; }
        public ListNode Next { get; set; }
        public ListNode Down { get; set; }

        public ListNode(int value)
        {
            this.Value = value;
            this.Next = null;
            this.Down = null;
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
        }

        public void FlattenList(ListNode head)
        {
            ListNode newHead = head;

            while(head != null)
            {
                if(head.Down != null)
                {
                    ListNode next = head.Next;

                    head.Next = head.Down;
                    head.Down = null;
                    head = head.Next;
                    head.Next = next;
                }
                else
                {
                    head = head.Next;
                }
            }

            while(newHead != null)
            {
                Console.WriteLine(newHead.Value);
                newHead = newHead.Next;
            }
        }
    }
}
