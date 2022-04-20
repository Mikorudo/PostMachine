﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolePostMachine
{
	abstract class Command
	{
		public abstract int ExecuteCommand(ref Machine machine);
		public abstract string GetInfo();
	}
	class MoveLeftCmd : Command
	{
		private int nextLine;
		public MoveLeftCmd(int nextLine)
		{
			this.nextLine = nextLine;
		}
		public override int ExecuteCommand(ref Machine machine)
		{
			//machine.MoveLeft();
			return nextLine;
		}
		public override string GetInfo()
		{
			return "Сдвиг влево и переход к " + nextLine + " строке";
		}
	}
	class MoveRightCmd : Command
	{
		private int nextLine;
		public MoveRightCmd(int nextLine)
		{
			this.nextLine = nextLine;
		}
		public override int ExecuteCommand(ref Machine machine)
		{
			//machine.MoveRight();
			return nextLine;
		}
		public override string GetInfo()
		{
			return "Сдвиг вправо и переход к " + nextLine + " строке";
		}
	}
	class IfElseCmd : Command
	{
		private int nextLineIfTrue;
		private int nextLineIfFalse;
		public IfElseCmd(int nextLine1, int nextLine2)
		{
			this.nextLineIfTrue = nextLine1;
			this.nextLineIfFalse = nextLine2;
		}
		public override int ExecuteCommand(ref Machine machine)
		{
			if (true/*machine.IsPointed()*/)
				return nextLineIfTrue;
			else
				return nextLineIfFalse;
		}
		public override string GetInfo()
		{
			return "Если ячейка закрашена, переход к " + nextLineIfTrue + " строке, иначе переход к " + nextLineIfFalse + " строке";
		}
	}
	class PointCellCmd : Command
	{
		private int nextLine;
		public PointCellCmd(int nextLine)
		{
			this.nextLine = nextLine;
		}
		public override int ExecuteCommand(ref Machine machine)
		{
			//machine.Point();
			return nextLine;
		}
		public override string GetInfo()
		{
			return "Закрасить ячейку и переход к " + nextLine + " строке";
		}
}
	class EraseCellCmd : Command
	{
		private int nextLine;
		public EraseCellCmd(int nextLine)
		{
			this.nextLine = nextLine;
		}
		public override int ExecuteCommand(ref Machine machine)
		{
			//machine.Erase();
			return nextLine;
		}
		public override string GetInfo()
		{
			return "Стереть ячейку и переход к " + nextLine + " строке";
		}
	}
	class StopCmd : Command
	{
		//Конструктор не нужен, нет переменных
		public override int ExecuteCommand(ref Machine machine)
		{
			//machine.Stop(); ИЛИ ничего
			return 0;
		}
		public override string GetInfo()
		{
			return "Остановить программу";
		}
	}
}
