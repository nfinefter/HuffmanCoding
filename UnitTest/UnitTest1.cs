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
        //Not sure if it works properly
        //Didn't know how to add GMR Solution
    }
}