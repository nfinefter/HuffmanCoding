using BinarySearchTree;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    public static class HuffmanEncoder
    {
        public static Dictionary<char, string> CompressedValue;

        public static string Huffman(string s, out Node<char> root)
        {
            PriorityQueue<(Node<char>, int), int> items = new PriorityQueue<(Node<char>, int), int>();

            string compressed = "";
            Dictionary<char, string> compressedValue = new Dictionary<char, string>();

            GetFrequency(s, items);
            TreeMaker(items, out compressedValue, out root);

            foreach (char c in s)
            {
                compressed += compressedValue[c];
            }

            CompressedValue = compressedValue;

            return compressed;
        }
        public static string TreeToString(Node<char> root)
        {
            string treeString = "";

            Stack<Node<char>> nodes = new Stack<Node<char>>();

            Node<char> curr = root;

            string chars = "";

            int leafCount = CompressedValue.Keys.Count;

            chars += (byte)leafCount;
            //Not casting as byte
            do
            {
                while (curr != null)
                {
                    nodes.Push(curr);
                    curr = curr.LeftNode;
                }
                curr = nodes.Pop();

                if (curr.Sentinal == true)
                {
                    chars += "1";
                }
                else
                {
                    chars += "0";
                    string temp = CompressedValue[curr.Data];

                    for (int i = temp.Length; i < 8; i++)
                    {
                        chars += "0";
                    }
                    chars += temp;

                }
                
                curr = curr.RightNode;
            } while (curr != null || nodes.Count != 0);

            return treeString;
        }
        public static Node<char> StringToTree(string treeString)
        {
            Node<char> root;

            root = new Node<char>('$', true);



            return root;
        }

        public static string DeCompressed(string compressed, Node<char> root)
        {
            string original = "";

            Node<char> curr = root;

            for (int i = 0; i < compressed.Length; i++)
            {
                if (compressed[i] == '0')
                {
                    curr = curr.LeftNode;
                }
                else
                {
                    curr = curr.RightNode;
                }
                if (curr.LeftNode == null && curr.RightNode == null)
                {
                    original += curr.Data;
                    curr = root;
                }
            }

            return original;
        }
        public static Dictionary<char, int> GetFrequency(string s, PriorityQueue<(Node<char>, int), int> items)
        {
            Dictionary<char, int> frequency = new Dictionary<char, int>();

            List<char> chars = new List<char>();

            for (int i = 0; i < s.Length; i++)
            {

                if (!frequency.TryGetValue(s[i], out int value))
                {
                    frequency.Add(s[i], 1);
                }
                else
                {
                    frequency[s[i]] = value + 1;
                }
            }
            foreach (var item in frequency)
            {
                Node<char> node = new Node<char>(item.Key, false);
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

        public static void TreeMaker(PriorityQueue<(Node<char>, int), int> items, out Dictionary<char, string> compressedValue, out Node<char> root)
        {   
            (Node<char> Node, int Frequency) firstNode;
            (Node<char> Node, int Frequency) secondNode;

            while (items.Count > 1)
            {
                firstNode = items.Dequeue();
                secondNode = items.Dequeue();

                int sumFreq = firstNode.Frequency + secondNode.Frequency;

                (Node<char> Node, int Frequency) sentinalNode = (new Node<char>('$', true) { LeftNode = firstNode.Node, RightNode = secondNode.Node }, sumFreq);

                sentinalNode.Node.LeftNode = firstNode.Node;
                sentinalNode.Node.RightNode = secondNode.Node;

                items.Enqueue(sentinalNode, sentinalNode.Item2);
            }

            compressedValue = new Dictionary<char, string>();

            root = items.Dequeue().Item1;

            Traversal(root, compressedValue, "");
        }

        public static void Traversal(Node<char> root, Dictionary<char, string> compressedValue, string s)
        {
            if (root.Sentinal == false) compressedValue.Add(root.Data, s);

            if (root.LeftNode == null) return;

            Traversal(root.LeftNode, compressedValue, s + '0');
            
            Traversal(root.RightNode, compressedValue, s + '1');
        }

    }
}
