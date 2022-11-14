using BinarySearchTree;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    public class HuffmanEncoder
    {
        private PriorityQueue<(Node<char>, int), int> items;

        public string Input = "";

        public HuffmanEncoder(string input)
        {
            Input = input;
            items = new PriorityQueue<(Node<char>, int), int>(new FrequencyComparer());
        }

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
            foreach (var item in frequency)
            {
                Node<char> node = new Node<char>(item.Key);
                items.Enqueue((node, item.Value), item.Value);
            }
            return frequency;
        }

        public void TreeMaker()
        {
            
            while (items.Count > 1)
            {
                (Node<char> Node, int Frequency) firstNode = items.Dequeue();
                (Node<char> Node, int Frequency) secondNode = items.Dequeue();

                int sumFreq = firstNode.Frequency + secondNode.Frequency;

                (Node<char> Node, int Frequency) sentinalNode = (new Node<char>('$') { LeftNode = firstNode.Node, RightNode = secondNode.Node }, sumFreq);
                
                //Test
                sentinalNode.Node.LeftNode = firstNode;
                sentinalNode.Node.RightNode = secondNode;

                items.Enqueue(sentinalNode, sentinalNode.Item2);
            }



        }
        //Code Traversal and Tree

        public Dictionary<char, string> Traversal()
        {
            Dictionary<char, string> compressedValue = new Dictionary<char, string>();

            Traversal(compressedValue);

            return compressedValue;
        }

        private void Traversal(Dictionary<char, string> compressedValue)
        {
            
        }

    }
}
