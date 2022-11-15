using BinarySearchTree;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    public static class HuffmanEncoder
    {

        public static string Huffman(string s)
        {
            PriorityQueue<(Node<char>, int), int> items = new PriorityQueue<(Node<char>, int), int>();

            string compressed = "";
            Dictionary<char, string> compressedValue = new Dictionary<char, string>();

            GetFrequency(s, items);
            TreeMaker(items, out compressedValue);

            foreach (var item in compressedValue)
            {
                compressed += item.Value;
            }

            return compressed;
        }

        public static Dictionary<char, int> GetFrequency(string s, PriorityQueue<(Node<char>, int), int> items)
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

        public static Dictionary<char, int> GetFrequency(string s)
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

        public static void TreeMaker(PriorityQueue<(Node<char>, int), int> items, out Dictionary<char, string> compressedValue)
        {   
            (Node<char> Node, int Frequency) firstNode;
            (Node<char> Node, int Frequency) secondNode;

            while (items.Count > 1)
            {
                firstNode = items.Dequeue();
                secondNode = items.Dequeue();

                int sumFreq = firstNode.Frequency + secondNode.Frequency;

                (Node<char> Node, int Frequency) sentinalNode = (new Node<char>('$') { LeftNode = firstNode.Node, RightNode = secondNode.Node }, sumFreq);

                sentinalNode.Node.LeftNode = firstNode.Node;
                sentinalNode.Node.RightNode = secondNode.Node;

                items.Enqueue(sentinalNode, sentinalNode.Item2);
            }

            compressedValue = new Dictionary<char, string>();

            Traversal(items.Dequeue().Item1, compressedValue, "");


        }

        public static void Traversal(Node<char> root, Dictionary<char, string> compressedValue, string s)
        {
            compressedValue.Add(root.Data, "");

            if (root.LeftNode == null) return;

            Traversal(root.LeftNode, compressedValue, s + '0');
            
            Traversal(root.RightNode, compressedValue, s + '1');
            
        }
    }
}
