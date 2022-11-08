using HuffmanCoding;

namespace UnitTest
{
    public class UnitTest1
    {
        HuffmanEncoder huffman = new HuffmanEncoder(); 

        [Fact]
        public void GetFrequency()
        {
            Dictionary<char, int> temp = huffman.GetFrequency("hiiii");

            var value = 0;

            temp.TryGetValue('h', out value);

            Assert.True(value == 1);
        }
    }
}