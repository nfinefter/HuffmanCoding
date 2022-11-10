using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    public class HuffmanEncoder
    {
        public PriorityQueue<char, int> items;

        public string Input = "";

        //Not sure what to do, do I make a tree??

        public Dictionary<char, int> GetFrequency(string s)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();

            List<char> chars = new List<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (!chars.Contains(s[i]))
                {
                    chars.Add(s[i]);
                }
            }

            int count = 0;

            for (int i = 0; i < chars.Count; i++)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    if (chars[i] == s[j])
                    {
                        count++;
                    }
                }
                frequency.Add(chars[i], count);
            }
            return frequency;
        }
        public void TreeMaker()
        {
            
        }

        public HuffmanEncoder(string input)
        {
            Input = input;
            items = new PriorityQueue<char, int>(new FrequencyComparer());
        }

    }
}
