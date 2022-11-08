using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffmanCoding
{
    public class FrequencyComparer : IComparer<int>
    {
        // not sure if this does what it is supposed to
        public int Compare(int a, int b)
        {
            if (a > b)
            {
                return 1;
            }
            if (b > a)
            {
                return -1;
            }
        
               
            return 0;
             

        }
    }
}
