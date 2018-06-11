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

        public void ReverseListInBlocks(ListNode head, int k)
        {
            ListNode curr = head;
            ListNode prev = null;

            prev = ReverseList2(ref curr, k - 1);
            head = curr;
            curr = prev.Next;
            while (curr != null)
            {
                ListNode temp = ReverseList2(ref curr, k - 1);
                prev.Next = curr;
                curr = temp.Next;
            }

            while(head != null)
            {
                Console.WriteLine(head.Value);
                head = head.Next;
            }
        }

		//https://algorithms.tutorialhorizon.com/reverse-a-linked-list/
        public ListNode ReverseList2(ref ListNode head, int k)
        {
            ListNode curr = head.Next;
            ListNode prev = head;
            /*
             * nextNode = currNode.next;
                 currNode.next = prevNode;
                 prevNode = currNode;
                 currNode = nextNode;
             */
            while (curr != null && k > 0)
            {
                /*
                 * before
                 *  1   ->   2  -> 3
                 *  prev   curr   next
                 *  head 
                 *  
                 *  after
                 *  2   ->  1   ->  3
                 *  head    prev    curr  
                 */

                ListNode next = curr.Next;
                prev.Next = curr.Next;
                curr.Next = head;
                head = curr;
                curr = next;
                k--;
            }

            return prev;
        }
    }
}
