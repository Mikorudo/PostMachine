using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractPostMachine
{
    public class Tape
    {
        private DoublyNode currentNode;
        public Tape()
        {
            currentNode = new DoublyNode(0, false);
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
            DoublyNode node = currentNode;
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
    class DoublyNode
    {
        public int index;
        public bool mark;
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
            mark = _value;
        }
    }
    public abstract class Machine
    {
        protected Tape tape;
        protected List<Command> commands;
        public Machine()
        {
            commands = null;
            tape = new Tape();
        }
        public virtual void LoadCommands(List<Command> commands)
        {
            if (commands == null)
                throw new ArgumentNullException();
            this.commands = commands;
        }
        public virtual void LoadCommands(string path)
        {
            LoadCommands(CommandInterpreter.TxtToCommands(path));
        }
        public virtual void LoadTape(Tape tape)
        {
            if (tape == null)
                throw new ArgumentNullException();
            this.tape = tape;
        }
        public virtual void ExecuteCommands()
        {
            int currentCommand = 1;
            while (true)
            {
                currentCommand = commands[currentCommand].ExecuteCommand(tape);
                if (currentCommand == 0 || currentCommand == -1)
                    break;
            }
        }
    }
}
