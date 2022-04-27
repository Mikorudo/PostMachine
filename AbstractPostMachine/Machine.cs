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
                throw new NullReferenceException();
            this.commands = commands;
        }
        public virtual void LoadCommands(string path)
        {
            LoadCommands(CommandInterpreter.TxtToCommands(path));
        }
        public virtual void LoadTape(Tape tape)
        {
            if (tape == null)
                throw new NullReferenceException();
            this.tape = tape;
        }
        public virtual void ExecuteCommands()
        {
            int currentCommand = 1;
            while (true)
            {
                currentCommand = commands[currentCommand].ExecuteCommand(ref tape);
                if (currentCommand == 0 || currentCommand == -1)
                    break;
            }
        }
    }
}
