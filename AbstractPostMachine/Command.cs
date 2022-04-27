using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractPostMachine
{
    public abstract class Command
    {
        public abstract int ExecuteCommand(ref Tape tape);
        public abstract string GetInfo();
    }
    public class MoveLeftCmd : Command
    {
        private int nextLine;
        public MoveLeftCmd(int nextLine)
        {
            this.nextLine = nextLine;
        }
        public override int ExecuteCommand(ref Tape tape)
        {
            tape.MoveLeft();
            return nextLine;
        }
        public override string GetInfo()
        {
            return "Сдвиг влево и переход к " + nextLine + " строке";
        }
    }
    public class MoveRightCmd : Command
    {
        private int nextLine;
        public MoveRightCmd(int nextLine)
        {
            this.nextLine = nextLine;
        }
        public override int ExecuteCommand(ref Tape tape)
        {
            tape.MoveRight();
            return nextLine;
        }
        public override string GetInfo()
        {
            return "Сдвиг вправо и переход к " + nextLine + " строке";
        }
    }
    public class IfElseCmd : Command
    {
        private int nextLineIfTrue;
        private int nextLineIfFalse;
        public IfElseCmd(int nextLine1, int nextLine2)
        {
            nextLineIfTrue = nextLine1;
            nextLineIfFalse = nextLine2;
        }
        public override int ExecuteCommand(ref Tape tape)
        {
            if (tape.IsPointed())
                return nextLineIfTrue;
            else
                return nextLineIfFalse;
        }
        public override string GetInfo()
        {
            return "Если ячейка закрашена, переход к " + nextLineIfTrue + " строке, иначе переход к " + nextLineIfFalse + " строке";
        }
    }
    public class PointCellCmd : Command
    {
        private int nextLine;
        public PointCellCmd(int nextLine)
        {
            this.nextLine = nextLine;
        }
        public override int ExecuteCommand(ref Tape tape)
        {
            if (tape.IsPointed())
                return -1;
            tape.Point();
            return nextLine;
        }
        public override string GetInfo()
        {
            return "Закрасить ячейку и переход к " + nextLine + " строке";
        }
    }
    public class EraseCellCmd : Command
    {
        private int nextLine;
        public EraseCellCmd(int nextLine)
        {
            this.nextLine = nextLine;
        }
        public override int ExecuteCommand(ref Tape tape)
        {
            if (!tape.IsPointed())
                return -1;
            tape.Erase();
            return nextLine;
        }
        public override string GetInfo()
        {
            return "Стереть ячейку и переход к " + nextLine + " строке";
        }
    }
    public class StopCmd : Command
    {
        public override int ExecuteCommand(ref Tape tape)
        {
            return 0;
        }
        public override string GetInfo()
        {
            return "Остановить программу";
        }
    }
}
