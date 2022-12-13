using BinarySearchTree;

using HuffmanCoding;

namespace UnitTest
{
    public class UnitTest1
    {

        [Fact]
        public void GetFrequency()
        {
            Dictionary<char, int> temp = HuffmanEncoder.GetFrequency("hello");

            var value = 0;

            temp.TryGetValue('h', out value);

            Assert.True(value == 1);
        }
        [Fact]
        public void Huffman()
        {
            string compressed = HuffmanEncoder.Huffman("mississippi", out _);

            Assert.True(compressed == "0");           
        }
        [Fact]
        public void DeCompress()
        {
            string compressed = HuffmanEncoder.Huffman("mississippi", out Node<char> root);

            string original = HuffmanEncoder.DeCompressed(compressed, root);

            Assert.True(original == "mississippi");
        }
        [Fact]
        public void TreeToString()
        {
            string compressed = HuffmanEncoder.Huffman("mississippi", out Node<char> root);

            string tree = HuffmanEncoder.TreeToString(root, compressed);

            Assert.True(tree != "0");
        }
        [Fact]
        public void StringToTree()
        {
            string compressed = HuffmanEncoder.Huffman("mississippi", out Node<char> root);

            Node<char> tree = HuffmanEncoder.StringToTree(compressed);

            Assert.True(tree != null);
        }
    }
}