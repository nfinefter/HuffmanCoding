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
            string compressed = HuffmanEncoder.Huffman("hello");

            Assert.True(compressed == "0");           
        }
        //Compare this to GMR Huffman (add GMR to the solution).
    }
}