using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolePostMachine
{
	class Tape
	{
		private DoublyNode currentNode;
		public Tape()
		{
			currentNode = new DoublyNode(0, false);
			DoublyNode node = currentNode;
			for (int i = -1; i >= -10; i--)
			{
				node.leftNode = new DoublyNode(i, false);
				node.leftNode.rightNode = node;
				node = node.leftNode;
			}
			node = currentNode;
			for (int i = 1; i <= 10; i++)
			{
				node.rightNode = new DoublyNode(i, false);
				node.rightNode.leftNode = node;
				node = node.rightNode;
			}
			currentNode = node;
		}
		public void MoveLeft()
		{
			if (currentNode.leftNode == null)
            {
				currentNode.leftNode = new DoublyNode(currentNode.index - 1, false);
				currentNode.leftNode.rightNode = currentNode;
            }
			currentNode = currentNode.leftNode;
		}
		public void MoveRight()
		{
			if (currentNode.rightNode == null)
            {
				currentNode.rightNode = new DoublyNode(currentNode.index + 1, false);
				currentNode.rightNode.leftNode = currentNode;
            }
			currentNode = currentNode.rightNode;
		}
		public bool IsPointed()
        {
			return currentNode.value;
        }
		public void Point()
        {
			currentNode.value = true;
        }
		public void Erase()
		{
			currentNode.value = false;
		}
	}
	class DoublyNode
	{
		public int index;
		public bool value;
		public DoublyNode leftNode;
		public DoublyNode rightNode;
		public DoublyNode(int _index, bool _value)
		{
			index = _index;
			value = _value;
		}
	}
	class Machine
	{
		List<Command> commands;
		public Mach
	}
}
