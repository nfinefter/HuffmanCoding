using System;
using System.Collections.Generic;
using System.Text;

namespace BinarySearchTree
{
    public class Node<T>
    {
        public Node<T> LeftNode { get; set; }
        public Node<T> RightNode { get; set; }
        public T Data { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }
}
