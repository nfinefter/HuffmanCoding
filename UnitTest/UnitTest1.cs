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

            Assert.True(compressed != "0");           
        }
        [Theory]
        [InlineData("hii")]
        [InlineData("iiiiiiiii")]
        [InlineData("MISSISSIPPI")]
        [InlineData("i")]
        public void DeCompress(string s)
        {
            string compressed = HuffmanEncoder.Huffman(s, out Node<char> root);

            string original = HuffmanEncoder.DeCompressed(compressed, root);

            Assert.True(original == s);
        }
        [Fact]
        public void TreeToString()
        {
            string compressed = HuffmanEncoder.Huffman("MMMM", out Node<char> root);

            Assert.True(compressed == "000000000010010011010000");
        }
        [Fact]
        public void StringToTree()
        {
            string compressed = HuffmanEncoder.Huffman("mississippi", out Node<char> root);

            Node<char> tree = HuffmanEncoder.StringToTree(compressed, out _, out _, out _);

            Assert.True(tree != null);
        }
    }
}