using System;
using System.Linq;

namespace CodingPractice
{
     /**
     * ShortURL: Bijective conversion between natural numbers (IDs) and short strings
     *
     * ShortURL.Encode() takes an ID and turns it into a short string
     * ShortURL.Decode() takes a short string and turns it into an ID
     *
     * Features:
     * + large alphabet (51 chars) and thus very short resulting strings
     * + proof against offensive words (removed 'a', 'e', 'i', 'o' and 'u')
     * + unambiguous (removed 'I', 'l', '1', 'O' and '0')
     *
     * Example output:
     * 123456789 <=> pgK8p
     */
    public class TinyUrlEncode
    {

        private const string Alphabet = "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_";
        private static readonly int Base = Alphabet.Length;

        //num is url Id in db
        public static string Encode(int num)
        {
            var sb = new System.Text.StringBuilder();
            while (num > 0)
            {
                sb.Insert(0, Alphabet.ElementAt(num % Base));
                num = num / Base;
            }
            return sb.ToString();
        }

        public static int Decode(string str)
        {
            var num = 0;
            for (var i = 0; i < str.Length; i++)
            {
                num = num * Base + Alphabet.IndexOf(str.ElementAt(i));
            }
            return num;
        }

    }
}
