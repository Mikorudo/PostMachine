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
			//for (int i = -1; i >= -10; i--)
			//{
			//	node.leftNode = new DoublyNode(i, false);
			//	node.leftNode.rightNode = node;
			//	node = node.leftNode;
			//}
			//node = currentNode;
			//for (int i = 1; i <= 10; i++)
			//{
			//	node.rightNode = new DoublyNode(i, false);
			//	node.rightNode.leftNode = node;
			//	node = node.rightNode;
			//}
			//currentNode = node;
		}
		public void MoveLeft()
		{
			currentNode = currentNode.LeftNode;
		}
		public void MoveRight()
		{
			currentNode = currentNode.RightNode;
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
		public (int, bool)[] GetCellsAroundCurrent() //Demo
		{
			(int, bool)[] tapeElements = new (int, bool)[11];
			DoublyNode node = currentNode;
			for (int i = 5; i >= 0; i--)
			{
				tapeElements[i] = (node.index, node.value);
				node = node.LeftNode;
			}
			node = currentNode.RightNode;
			for (int i = 6; i < 11; i++)
			{
				tapeElements[i] = (node.index, node.value);
				node = node.RightNode;
			}
			return tapeElements;
		}
	}
	class DoublyNode
	{
		public int index;
		public bool value;
		public DoublyNode LeftNode
		{
			get
			{
				if (leftNode == null)
				{
					leftNode = new DoublyNode(index - 1, false);
					leftNode.rightNode = this;
				}
				return leftNode;
			}
			set
			{
				leftNode = value;
			}
		}
		public DoublyNode RightNode
		{
			get
			{
				if (rightNode == null)
				{
					rightNode = new DoublyNode(index + 1, false);
					rightNode.leftNode = this;
				}
				return rightNode;
			}
			set
			{
				leftNode = value;
			}
		}
		private DoublyNode leftNode;
		private DoublyNode rightNode;
		public DoublyNode(int _index, bool _value)
		{
			index = _index;
			value = _value;
		}
	}
	class Machine
	{
		private Tape tape;
		private List<Command> commands;
		public Machine()
		{
			commands = null;
			tape = new Tape();
		}
		public void LoadCommands(List<Command> commands)
		{
			if (commands == null)
				throw new NullReferenceException();
			this.commands = commands;
		}
		public void ConsoleExecuteCommands()
		{
			int currentCommand = 1;
			ConsolePrintTape();
			while (true)
			{
				Console.WriteLine("Строка #" + currentCommand + ": " + commands[currentCommand].GetInfo());
				currentCommand = commands[currentCommand].ExecuteCommand(ref tape);
				ConsolePrintTape();
				if (currentCommand == 0)
				{
					Console.WriteLine("Финиш!");
					break;
				}
				if (currentCommand == -1)
				{
					Console.WriteLine("Ошибка!");
					break;
				}
			}
		}
		public void ConsolePrintTape()
		{
			(int, bool)[] array = tape.GetCellsAroundCurrent();
			for (int i = 0; i < 5; i++)
			{
				Console.Write("{0, 4}", array[i].Item1);
			}
			Console.Write(" |{0, -4}| ", array[5].Item1);
			for (int i = 6; i < 11; i++)
			{
				Console.Write("{0, -4}", array[i].Item1);
			}
			Console.WriteLine();
			for (int i = 0; i < 5; i++)
			{
				Console.Write("{0, 4}", array[i].Item2 ? 1 : 0);
			}
			Console.Write(" |{0, -4}| ", array[5].Item2 ? 1 : 0);
			for (int i = 6; i < 11; i++)
			{
				Console.Write("{0, -4}", array[i].Item2 ? 1 : 0);
			}
			Console.WriteLine();
			Console.WriteLine();
		}
	}
}
