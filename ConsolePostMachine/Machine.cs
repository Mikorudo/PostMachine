using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolePostMachine
{
	class Tape
	{
		DoublyNode currentNode;
		
		public Tape()
		{
			currentNode = new DoublyNode(0, false);
			DoublyNode node = currentNode;
			for (int i = -1; i >= -10; i--)
			{
				node.leftNode = new DoublyNode(i, false);
				node = node.leftNode;
			}
			node = currentNode;
			for (int i = 1; i <= 10; i++)
			{
				node.rightNode = new DoublyNode(i, false);
				node = node.rightNode;
			}
			currentNode = node;
		}
		public void MoveLeft()
		{
			currentNode = currentNode.leftNode;
			//Если левая пустая, то...
		}
		public <int, bool>
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

	}
}
