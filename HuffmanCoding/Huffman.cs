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
    using Variable = Int32;
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

            compressed = TreeToString(root, compressed);

            return compressed;
        }
        public static void Filler(ref string s, string filler)
        {
            for (int i = filler.Length; i < 8; i++)
            {
                s += "0";
            }
            s += filler;
        }

        public static string TreeToString(Node<char> root, string compressed)
        {
            Stack<Node<char>> nodes = new Stack<Node<char>>();

            Node<char> curr = root;

            string treeString = "";

            int leafCount = CompressedValue.Keys.Count;

            string temp = Convert.ToString((byte)(leafCount), 2);

            Filler(ref treeString, temp);
            
            do
            {
                while (curr != null)
                {
                    nodes.Push(curr);
                    if (curr.Sentinal == true)
                    {
                        treeString += "1";
                    }
                    else
                    {
                        treeString += "0";
                        temp = Convert.ToString((byte)(curr.Data), 2);

                        Filler(ref treeString, temp);
                    }
                    curr = curr.LeftNode;
                }

                curr = nodes.Pop();
                curr = curr.RightNode;
            } while (curr != null || nodes.Count != 0);

            treeString += compressed;

            int padCount = 0;
            while ((treeString.Length + 3) % 8 != 0)
            {
                padCount++; 
                treeString += "0";
            }

            string stringCount = Convert.ToString(padCount, 2);

            while (stringCount.Length < 3)
            {
                stringCount = stringCount.Insert(0, "0");
            }

            treeString = treeString.Insert(0, stringCount);
            
            return treeString;
        }


        public static Node<char> StringToTree(string treeString)
        {
            Variable index = 0;

            int padCount = Convert.ToByte(treeString.Substring(index, index += 3), 2);
            int leafCount = Convert.ToByte(treeString.Substring(index, 8), 2);

            if (leafCount == 1)
            {
                char c = (char)Convert.ToByte(treeString.Substring(index += 1, 8), 2);
                return new Node<char>(c, false);
            }

            Node<char> root = new Node<char>('$', true);

            Stack<Node<char>> nodes = new Stack<Node<char>>();

            Queue<Node<char>> allNodes = new Queue<Node<char>>();

            nodes.Push(root);

            Variable leafCounter = 0;

            Node<char> temp;

            for (index += 9; leafCounter < leafCount;)
            {
                if (treeString[index++] == '0')
                {
                    char c = (char)Convert.ToByte(treeString.Substring(index, 8), 2);
                    index += 8;
                    leafCounter++;
                    temp = new Node<char>(c, false);
                    allNodes.Enqueue(temp);
                }
                else
                { 
                    temp = new Node<char>('$', true);
                    allNodes.Enqueue(temp);

                    nodes.Push(temp);

                    //Push the sentinal and then pop it once it has two children
                }

                if (temp.RightNode != null)
                {
                    nodes.Pop();
                }
                //Dont need stack because used recursion
            }
            TreeRebuilder(root, allNodes);

            return root;
        }

        public static void TreeRebuilder(Node<char> node, Queue<Node<char>> temp)
        {
            if (temp.Count == 0 || !node.Sentinal) return;

            var curr = node.LeftNode = temp.Dequeue();
            TreeRebuilder(curr, temp);

            curr = node.RightNode = temp.Dequeue();
            TreeRebuilder(curr, temp);
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
