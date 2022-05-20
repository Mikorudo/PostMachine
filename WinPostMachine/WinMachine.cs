using System;
using AbstractPostMachine;
using System.Threading.Tasks;

namespace WinPostMachine
{
    delegate void TapeUpdateHandler(Tape tape, bool isIfElseCmd);
    delegate void InvokedCommandInfoHandler(string text);
    delegate void FinishAlgoritm(string text);
    internal class WinMachine : Machine
    {
        //private VisualTape visualTape;
        public event TapeUpdateHandler tapeUpdate;
        public event InvokedCommandInfoHandler invokedCommandInfo;
        public event FinishAlgoritm finishAlgoritm;
        public int DelayTime
        {
            get
            {
                return delayTime;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Delay time cannot be less than 0: " + value);
                delayTime = value;
            }
        }
        public WinMachine()
        {
            DelayTime = 0;
        }
        private int delayTime;
        public override async void ExecuteCommands()
        {
            int currentCommand = 1;
            while (true)
            {
                bool isCmdIfElse = commands[currentCommand].GetType() == typeof(IfElseCmd);
                invokedCommandInfo?.Invoke(commands[currentCommand].GetInfo());
                currentCommand = commands[currentCommand].ExecuteCommand(tape);
                tapeUpdate?.Invoke(tape, isCmdIfElse);
                if (currentCommand == 0)
                {
                    finishAlgoritm?.Invoke("Алгоритм успешно завершил свою работу");
                    break;
                }
                if (currentCommand == -1)
                {
                    finishAlgoritm?.Invoke("Выполнение недостижимого кода");
                    break;
                }
                await Task.Delay(DelayTime);
            }
        }
   }
}
