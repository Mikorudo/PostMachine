using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WinPostMachine
{
    delegate void CommandMessage(string str);
    delegate void WorkEnd(string str);
    delegate void TapeStatusUpdate(Tape tape, bool isCmdIfElse);
    internal class PostMachine
    {
        public event CommandMessage commandMessage;
        public event WorkEnd workEnd;
        public event TapeStatusUpdate updateTape;
        private Tape tape;
        private List<Command> commands;
        public bool Condition { get; private set; }
        public int Delay
        {
            get { return _delay; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException($"The delay cannot be a negative number ({value}");
                else
                    _delay = value;
            }
        }
        private int _delay;
        public PostMachine()
        {
            commands = null;
            Condition = false;
            tape = new Tape();
            _delay = 0;
        }
        public virtual void LoadCommands(List<Command> commands)
        {
            if (commands == null)
                throw new ArgumentNullException();
            this.commands = commands;
        }
        public virtual void LoadCommands(string path)
        {
            LoadCommands(CommandInterpreter.TxtFileToCommands(path));
        }
        public void Reset()
        {
            tape = new Tape();
        }
        public async void ExecuteCommands()
        {
            if (commands == null)
            {
                workEnd?.Invoke("Список команд не загружен");
                return;
            }
            int currentCommand = 1;
            Condition = true;
            while (true)
            {
                if (!Condition)
                {
                    workEnd?.Invoke("Работа машины остановлена");
                    break;
                }
                bool isCmdIfElse = commands[currentCommand].GetType() == typeof(IfElseCmd);
                commandMessage?.Invoke(commands[currentCommand].GetInfo());
                currentCommand = commands[currentCommand].ExecuteCommand(tape);
                if (currentCommand == 0)
                {
                    commandMessage?.Invoke(commands[0].GetInfo());
                    Condition = false;
                    int[] indexes;
                    bool[] marks;
                    tape.GetCellsAroundCurrent(out indexes, out marks);
                    updateTape?.Invoke(tape, isCmdIfElse);
                    workEnd?.Invoke("Алгоритм успешно завершил свою работу");
                    break;
                } else if (currentCommand == -1)
                {
                    Condition = false;
                    workEnd?.Invoke("Выполнение недостижимого кода");
                    break;
                } else
                {
                    int[] indexes;
                    bool[] marks;
                    tape.GetCellsAroundCurrent(out indexes, out marks);
                    updateTape?.Invoke(tape, isCmdIfElse);
                    await Task.Delay(_delay);
                }
            }
        }
        public void StopExecutingCommands()
        {
            Condition = false;
        }
    }
}
