using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolePostMachine
{
	abstract class Commands
	{
		public abstract int ExecuteCommand(ref Machine machine);
	}
	class MoveLeftCmd : Commands
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
	}
	class MoveRightCmd : Commands
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
	}
	class IfElseCmd : Commands
	{
		private int ifTrueLine;
		private int ifFalseLine;
		public IfElseCmd(int ifTrueLine, int ifFalseLine)
		{
			this.ifTrueLine = ifTrueLine;
			this.ifFalseLine = ifFalseLine;
		}
		public override int ExecuteCommand(ref Machine machine)
		{
			if (true/*machine.IsPointed()*/)
				return ifTrueLine;
			else
				return ifFalseLine;
		}
	}
	class PointCell : Commands
	{
		private int nextLine;
		public PointCell(int nextLine)
		{
			this.nextLine = nextLine;
		}
		public override int ExecuteCommand(ref Machine machine)
		{
			//machine.Point();
			return nextLine;
		}
	}
	class EraseCellCmd : Commands
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
	}
	class StopCmd : Commands
	{
		//Конструктор не нужен, нет переменных
		public override int ExecuteCommand(ref Machine machine)
		{
			//machine.Stop(); ИЛИ ничего
			return 0;
		}
	}
}
