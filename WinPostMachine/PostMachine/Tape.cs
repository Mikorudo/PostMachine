using System;
using System.Collections.Generic;
using System.Text;

namespace WinPostMachine
{
    public class Tape
    {
        private Node currentNode;
        public Tape()
        {
            currentNode = new Node(0, false);
        }
        public void MoveLeft()
        {
            currentNode = currentNode.LeftNode;
        }
        public void MoveRight()
        {
            currentNode = currentNode.RightNode;
        }
        public bool IsMarked()
        {
            return currentNode.mark;
        }
        public void Mark()
        {
            currentNode.mark = true;
        }
        public void Unmark()
        {
            currentNode.mark = false;
        }
        public void GetCellsAroundCurrent(out int[] indexes, out bool[] marks) //Demo
        {
            indexes = new int[11];
            marks = new bool[11];
            Node node = currentNode;
            for (int i = 5; i >= 0; i--)
            {
                indexes[i] = node.index;
                marks[i] = node.mark;
                node = node.LeftNode;
            }
            node = currentNode.RightNode;
            for (int i = 6; i < 11; i++)
            {
                indexes[i] = node.index;
                marks[i] = node.mark;
                node = node.RightNode;
            }
        }
    }
    internal class Node
    {
        public int index;
        public bool mark;
        public Node LeftNode
        {
            get
            {
                if (leftNode == null)
                {
                    leftNode = new Node(index - 1, false);
                    leftNode.rightNode = this;
                }
                return leftNode;
            }
            set
            {
                leftNode = value;
                leftNode.rightNode = this;
            }
        }
        public Node RightNode
        {
            get
            {
                if (rightNode == null)
                {
                    rightNode = new Node(index + 1, false);
                    rightNode.leftNode = this;
                }
                return rightNode;
            }
            set
            {
                rightNode = value;
                rightNode.leftNode = this;
            }
        }
        private Node leftNode;
        private Node rightNode;
        public Node(int _index, bool _value)
        {
            index = _index;
            mark = _value;
        }
    }
}
