using System;
using System.Collections.Generic;
using System.Linq;

namespace CodingPractice
{
    public class DoorDashInterview
    {
        public DoorDashInterview()
        {
            //NarryNode a = new NarryNode("a", 1, true);
            //NarryNode b = new NarryNode("b", 2, true);
            //NarryNode c = new NarryNode("c", 3, true);
            //NarryNode d = new NarryNode("d", 4, true);
            //NarryNode e = new NarryNode("e", 5, true);
            //NarryNode g = new NarryNode("g", 6, true);

            //a.Children.Add(b);
            //a.Children.Add(c);

            //b.Children.Add(d);
            //b.Children.Add(e);

            //c.Children.Add(g);

            //NarryNode a1 = new NarryNode("a", 1, true);
            //NarryNode b1 = new NarryNode("b", 2, true);
            //NarryNode c1 = new NarryNode("c", 3, false);
            //NarryNode d1 = new NarryNode("d", 4, true);
            //NarryNode e1 = new NarryNode("e", 5, true);
            //NarryNode f1 = new NarryNode("f", 66, true);
            //NarryNode g1 = new NarryNode("g", 7, false);

            ////a1.Children.Add(b1);
            //a1.Children.Add(c1);

            ////b1.Children.Add(d1);
            ////b1.children.add(e1);
            ////b1.children.add(f1);

            //c1.Children.Add(f1);

            //var res = GetModifiedItems(a, a1);
        }

        public class Job
        {
            public int start;
            public int end;
            public int profit;
        }


        /*
         * 
         * So here it is 10:05 as first case, so its written as 11005
           2nd is 10:10 so its written as 11010
            ...
            ...
            Stop at 11100
         * 
         */

        private int GetDayOfTheWeek(string day)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            map.Add("mon", 1);
            map.Add("tue", 2);
            map.Add("wed", 3);
            map.Add("thu", 4);
            map.Add("fri", 5);
            map.Add("sat", 6);
            map.Add("sun", 7);

            return map[day.ToLower()];
        }

        public class TimeInterval
        {
            public DateTime start;
            public DateTime end;
            public int startDayOfWeek;
            public int endDayOfWeek;
        }

        private TimeInterval ParseDate(string start, string end)
        {            
            var items = start.Split(new char[] { ' ' });
            DateTime startDate = DateTime.Parse(items[1] + items[2]);
            int startDayOfWeek = GetDayOfTheWeek(items[0]);

            items = end.Split(new char[] { ' ' });
            DateTime endDate = DateTime.Parse(items[1] + items[2]);
            int endDayOfWeek = GetDayOfTheWeek(items[0]);

            if(startDayOfWeek > endDayOfWeek)
            {
                endDate.AddDays(7 + (endDayOfWeek - startDayOfWeek));
            }

            TimeInterval t = new TimeInterval() { end = endDate, start = startDate, startDayOfWeek = startDayOfWeek, endDayOfWeek = endDayOfWeek };
           
            return t;
        }
        
        public void AppendTime(string start, string end, int interval, bool is12hrs)
        {
            var ti = ParseDate(start, end);
            DateTime startTime = ti.start;
            DateTime endTime = ti.end;

            DateTime s = startTime;
            while(s < endTime)
            {
                s = s.AddMinutes(interval);

                int hrs = is12hrs ? s.Hour / 12 : s.Hour;
                int mins = s.Minute;

                string hrsStr = hrs.ToString(), minsStr = mins.ToString();
                if (hrs < 10)
                {
                    hrsStr = "0" + hrs.ToString();
                }
                if(mins < 10)
                {
                    minsStr = "0" + mins.ToString();
                }               

                Console.WriteLine(((int)s.DayOfWeek).ToString() + hrsStr + minsStr);
            }
        }
              
        public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
        {
            int max = 0;
            List<Job> jobs = new List<Job>();

            for(int i = 0; i < startTime.Length; i++)
            {
                jobs.Add(new Job() { start = startTime[i], end = endTime[i], profit = profit[i] });
            }

            jobs = jobs.OrderBy(j => j.end).ToList();
            int[] result = new int[jobs.Count];

            for(int i = 0; i < jobs.Count; i++)
            {
                int maxsofar = jobs[i].profit;
                for(int j = i - 1; j >= 0; j--)
                {
                    if(jobs[j].end <= jobs[i].start)
                    {
                        maxsofar = Math.Max(maxsofar, jobs[i].profit + result[j]);
                        break;
                    }
                }               

                max = Math.Max(max, maxsofar);

                //result of i updated with considering the job at jth and without jth index
                //hence max is set 
                result[i] = max;
            }
            return max;
        }

        public int GetIndex(List<Job> jobs, int index)
        {
            int start = 0, end = index - 1;
            while(start <= end)
            {
                int mid = (start + end) / 2;

                if (jobs[mid].end <= jobs[index].start)
                {
                    if (jobs[mid + 1].end <= jobs[index].start)
                    {
                        start = mid + 1;
                    }
                    else
                    {
                        return mid;
                    }
                }
                else
                {
                    end = mid - 1;
                }
            }

            return -1;
        }

        public int JobScheduling2(int[] startTime, int[] endTime, int[] profit)
        {
            int max = 0;
            List<Job> jobs = new List<Job>();

            for (int i = 0; i < startTime.Length; i++)
            {
                jobs.Add(new Job() { start = startTime[i], end = endTime[i], profit = profit[i] });
            }

            jobs = jobs.OrderBy(j => j.end).ToList();

            int[] result = new int[jobs.Count];

            for (int i = 0; i < jobs.Count; i++)
            {
                int maxSofar = jobs[i].profit;
                int index = GetIndex(jobs, i);
                if(index == -1)
                {
                    result[i] = maxSofar;
                }
                else
                {
                    maxSofar = Math.Max(maxSofar, jobs[i].profit + result[index]);                   
                }
                //We need to update the result with 
                max = Math.Max(max, maxSofar);
                result[i] = max;
            }

            return max;
        }


        /* https://leetcode.com/problems/minimum-number-of-steps-to-make-two-strings-anagram/submissions/
         * 
         * the main constraint in the question that says s and t are of the same length.

            So arr[i] positive means t is deficient of those characters and they need to be added to make it same
            arr[i] is negative means t is in surplus of those characters and they need to be replaced. a

            you could have done if(arr[i] < 0 ) ans+=arr[i] and then returned -1*ans as well and it would have resulted in same ans

            consider an example abcde and aaabd

            so your array will look like [-2,0,1,0,1]
            so you can see that sum of negative and postive is always the same
         * 
         */

        public int MinSteps(string s, string t)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            for (int i = 0; i < s.Length; i++)
            {
                var s1 = s[i];
                var t1 = t[i];

                if (map.ContainsKey(s1))
                {
                    map[s1]++;
                }
                else
                {
                    map.Add(s1, 1);
                }

                if (map.ContainsKey(t1))
                {
                    map[t1]--;
                }
                else
                {
                    map.Add(t1, -1);
                }
            }
            int count = 0;
            foreach (var item in map)
            {
                if (item.Value > 0)
                {
                    count += Math.Abs(item.Value);
                }
            }

            return count;
        }


        /*
         * https://leetcode.com/discuss/interview-question/1544410/Doordash-TPS-Senior-Software-Engineer
         * 
         */
        public List<DateTime> GetAvailableDeliveries()
        {

            List<DateTime> dateTimes = new List<DateTime>();

            //Test Data
            dateTimes.Add(DateTime.Now.AddDays(3));
            dateTimes.Add(DateTime.Now.AddDays(1).AddHours(3));
            dateTimes.Add(DateTime.Now.AddDays(2));
            dateTimes.Add(DateTime.Now.AddDays(1).AddHours(6));


            DateTime currentTime = DateTime.Parse("18:00:00");
            var tier = "low";
            List<DateTime> result = new List<DateTime>();

            foreach (var d in dateTimes)
            {
                switch (tier)
                {
                    case "high":
                        var t = d - currentTime;
                        if (currentTime.Hour >= 18 && t.TotalHours < 24)
                        {
                            result.Add(d);
                        }
                        break;
                    case "low":
                        t = d - currentTime;
                        if (currentTime.Hour >= 19 && t.TotalHours < 24)
                        {
                            result.Add(d);
                        }
                        break;
                }
            }

            return result;
        }


        /**
         * https://leetcode.com/discuss/interview-question/846916/Validate-Orders-Path-(Doordash)
         * 
         * Console.WriteLine(doorDashInterview.LongestValidOrder(new List<string>() { "P11", "P12", "D11", "D12" }));
            Console.WriteLine(doorDashInterview.LongestValidOrder(new List<string>() { "P1", "D1", "P2", "D2" }));
            Console.WriteLine(doorDashInterview.LongestValidOrder(new List<string>() { "P1", "D2", "D1", "P2" }));
            Console.WriteLine(doorDashInterview.LongestValidOrder(new List<string>() {  }));
         */
        public bool LongestValidOrder(List<string> orders)
        {
            HashSet<int> pickups = new HashSet<int>();
            HashSet<int> drops = new HashSet<int>();

            foreach (var o in orders)
            {
                var t = o[0];
                var num = int.Parse(o.Substring(1, o.Length - 1));

                switch (t)
                {
                    case 'P':
                    case 'p':

                        if (pickups.Contains(num)) return false;
                        pickups.Add(num);
                        break;

                    case 'D':
                    case 'd':

                        if (drops.Contains(num) || !pickups.Contains(num)) return false;
                        drops.Add(num);
                        break;
                }

            }

            return pickups.Count == drops.Count;

        }


        //https://leetcode.com/discuss/interview-question/1367130/doordash-phone-interview/1026887
        /*
         * Read the new menu in to the map of key->key value->TreeNode
         * 
         * Iterate over old menu and if there is miss match increment the count
         * 
         */
        public int GetModifiedItems(NarryNode oldMenu, NarryNode newMenu)
        {
            int count = 0;

            var map = new Dictionary<string, NarryNode>();
            GetChildren(newMenu, ref map);
            map[newMenu.Key] = newMenu;

            Queue<NarryNode> q = new Queue<NarryNode>();
            q.Enqueue(oldMenu);

            while (q.Count > 0)
            {
                var item = q.Dequeue();

                if (map.ContainsKey(item.Key))
                {
                    if (!CompareItems(item, map[item.Key]))
                    {
                        count++;
                    }
                }
                else
                {
                    count++;
                }

                foreach (var c in item.Children)
                {
                    q.Enqueue(c);
                }
            }


            return count;
        }

        private bool CompareItems(NarryNode item1, NarryNode item2)
        {
            if (item1.Key != item2.Key) return false;
            if (item1.Value != item2.Value) return false;
            if (item1.IsActive != item2.IsActive) return false;

            return true;
        }

        private void GetChildren(NarryNode root, ref Dictionary<string, NarryNode> children)
        {
            if (root == null)
            {
                return;
            }

            foreach (var child in root.Children)
            {
                children.Add(child.Key, child);
                GetChildren(child, ref children);
            }
        }

        int[,] grid = new int[,]
        {
            { 1, 0, 0, 0, 0}, // 0
            { 1, 2, 0, 0, 0}, // 1
            { 1, 1, 1, 1, 1}, // 2
            { 1, 1, 0, 2, 1}, // 3
        };

        int ROWS = 0, COLS = 0, count = 0, smallest = Int32.MaxValue;
        public int GetShortestDistance()
        {
            int count = 0;
            //for (int i = 0; i < grid.GetLength(0); i++)
            //{
            //    for (int j = 0; j < grid.GetLength(1); j++)
            //    {
            //        if (grid[i][j] == 1)
            //        {
            //            count++;
            //            DFS_1(grid, i, j);
            //        }
            //    }
            //}
            ROWS = grid.GetLength(0);
            COLS = grid.GetLength(1);
            HashSet<string> visited = new HashSet<string>();

            HashSet<string> visitedMart = new HashSet<string>();

            DFS_1(grid, 0, 0, ref visited, ref visitedMart);

            //BFS(grid, 0, 0);

            return count;
            // Code here
        }

        private void DFS_1(int[,] grid, int r, int c, ref HashSet<string> visited, ref HashSet<string> visitedMart)
        {
            if (r >= 0 && r < ROWS && c >= 0 && c < COLS && grid[r, c] != 0 && !visited.Contains(r + ":" + c))
            {
                if (grid[r, c] == 2 && !visitedMart.Contains(r + ":" + c)) 
                {
                    count = visited.Count;
                    smallest = Math.Min(count, smallest);
                    visitedMart.Add(r + ":" + c);
                }
                visited.Add(r + ":" + c);

                DFS_1(grid, r + 1, c, ref visited, ref visitedMart);
                DFS_1(grid, r - 1, c, ref visited, ref visitedMart);
                DFS_1(grid, r, c + 1, ref visited, ref visitedMart);
                DFS_1(grid, r, c - 1, ref visited, ref visitedMart);

                visited.Remove(r + ":" + c);
            }
        }

        public class Point
        {
            public int r;
            public int c;
            public int distance;

            public Point(int r, int c, int distance)
            {
                this.r = r;
                this.c = c;
                this.distance = distance;
            }
        }

        private void BFS(int[,] grid, int r, int c)
        {
            int distance = int.MaxValue;
            Queue<Point> q = new Queue<Point>();
            HashSet<string> visited = new HashSet<string>();

            q.Enqueue(new Point(r, c, 0));

            while(q.Count > 0)
            {
                var curr = q.Dequeue();

                r = curr.r;
                c = curr.c;

                if(grid[curr.r, curr.c] == 2)
                {
                    distance = Math.Min(distance, curr.distance);
                }

                if(IsSafe(grid, r + 1, c, visited))
                {
                    q.Enqueue(new Point(r + 1, c, curr.distance + 1));
                }
                if (IsSafe(grid, r, c + 1, visited))
                {
                    q.Enqueue(new Point(r, c + 1, curr.distance + 1));
                }
                if (IsSafe(grid, r - 1, c, visited))
                {
                    q.Enqueue(new Point(r - 1, c, curr.distance + 1));
                }
                if (IsSafe(grid, r, c - 1, visited))
                {
                    q.Enqueue(new Point(r, c - 1, curr.distance + 1));
                }
            }

            Console.WriteLine(distance);
        }

        private bool IsSafe(int[,] grid, int r, int c, HashSet<string> visited)
        {
            if (r >= 0 && r < ROWS && c >= 0 && c < COLS && grid[r, c] != 0 && !visited.Contains(r + ":" + c))
            {
                visited.Add(r + ":" + c);
                return true;
            }

            return false;
        }
    }
}
