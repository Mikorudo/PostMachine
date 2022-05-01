using System;
using System.Collections.Generic;
using System.Text;
using AbstractPostMachine;

namespace ConsolePostMachine
{
    internal sealed class ConsoleMachine : Machine
    {
        public override void ExecuteCommands()
        {
            int currentCommand = 1;
            Console.WriteLine("Машина Поста начала выполнение команд");
            PrintTape();
            while (true)
            {
                Console.WriteLine("Строка #" + currentCommand + ": " + commands[currentCommand].GetInfo());
                currentCommand = commands[currentCommand].ExecuteCommand(ref tape);
                PrintTape();
                if (currentCommand == 0)
                {
                    Console.WriteLine("Машина Поста закончила выполнение команд успешно");
                    break;
                }
                if (currentCommand == -1)
                {
                    Console.WriteLine("Достигнута невыполнимая команда");
                    break;
                }
            }
        }
        public void PrintTape()
        {
            int[] indexes;
            bool[] marks;
            tape.GetCellsAroundCurrent(out indexes, out marks);
            for (int i = 0; i < 5; i++)
            {
                Console.Write("{0, 4}", indexes[i]);
            }
            Console.Write(" |{0, -4}| ", indexes[5]);
            for (int i = 6; i < 11; i++)
            {
                Console.Write("{0, -4}", indexes[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                Console.Write("{0, 4}", marks[i] ? 1 : 0);
            }
            Console.Write(" |{0, -4}| ", marks[5] ? 1 : 0);
            for (int i = 6; i < 11; i++)
            {
                Console.Write("{0, -4}", marks[i] ? 1 : 0);
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
