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
        private int nextLineIfMarked;
        private int nextLineIfUnmarked;
        public IfElseCmd(int nextLine1, int nextLine2)
        {
            nextLineIfMarked = nextLine1;
            nextLineIfUnmarked = nextLine2;
        }
        public override int ExecuteCommand(ref Tape tape)
        {
            if (tape.IsMarked())
                return nextLineIfMarked;
            else
                return nextLineIfUnmarked;
        }
        public override string GetInfo()
        {
            return "Если ячейка закрашена, переход к " + nextLineIfMarked + " строке, иначе переход к " + nextLineIfUnmarked + " строке";
        }
    }
    public class MarkCellCmd : Command
    {
        private int nextLine;
        public MarkCellCmd(int nextLine)
        {
            this.nextLine = nextLine;
        }
        public override int ExecuteCommand(ref Tape tape)
        {
            if (tape.IsMarked())
                return -1;
            tape.Mark();
            return nextLine;
        }
        public override string GetInfo()
        {
            return "Закрасить ячейку и переход к " + nextLine + " строке";
        }
    }
    public class UnmarkCellCmd : Command
    {
        private int nextLine;
        public UnmarkCellCmd(int nextLine)
        {
            this.nextLine = nextLine;
        }
        public override int ExecuteCommand(ref Tape tape)
        {
            if (!tape.IsMarked())
                return -1;
            tape.Unmark();
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
