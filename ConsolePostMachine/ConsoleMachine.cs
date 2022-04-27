using System;
using System.Collections.Generic;
using System.Text;
using PostMachineLib;

namespace ConsolePostMachine
{
    internal sealed class ConsoleMachine : Machine
    {
        public override void ExecuteCommands()
        {
            int currentCommand = 1;
            PrintTape();
            while (true)
            {
                Console.WriteLine("Строка #" + currentCommand + ": " + commands[currentCommand].GetInfo());
                currentCommand = commands[currentCommand].ExecuteCommand(ref tape);
                PrintTape();
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
        public void PrintTape()
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
