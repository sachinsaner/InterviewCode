using System;
using System.Collections.Generic;

namespace CodingPractice
{
    public class Stacks
    {
        public string SimplifyPath(string path)
        {
            var list = new LinkedList<string>();

            var dirs = path.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var dir in dirs)
            {

                switch (dir)
                {
                    case ".":
                        break;

                    case "..":
                        if (list.Count > 0) list.RemoveLast();
                        break;
                    default:
                        list.AddLast(dir);
                        break;
                }

            }
            string res = string.Empty;
            if (list.Count == 0)
                res = "/";
            else
            {
                foreach (var item in list)
                {
                    res += "/";
                    res += item;

                }

                list.Clear();
            }

            Console.WriteLine(res);

            return res;

        }
    }
}
