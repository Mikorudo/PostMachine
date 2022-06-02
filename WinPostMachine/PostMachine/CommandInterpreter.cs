using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace WinPostMachine
{
    public static class CommandInterpreter
    {
        private enum CommandType
        {
            left,
            right,
            point,
            erase,
            ifelse,
            stop
        }
        private static List<Command> StringListToCommands(List<string> textCommands)
        {
            List<Command> commands = new List<Command>();
            commands.Add(new StopCmd());
            for (int i = 0; i < textCommands.Count; i++)
            {
                string[] arguments = textCommands[i].Split();
                int lineNum;
                if (!int.TryParse(arguments[0], out lineNum))
                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: номер команды " + arguments[0] + " не число");
                if (lineNum != i + 1)
                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: номер команды " + arguments[0] + " нарушает порядок команд");
                CommandType commandType;
                switch (arguments[1])
                {
                    case ">":
                        commandType = CommandType.right;
                        break;
                    case "<":
                        commandType = CommandType.left;
                        break;
                    case "1":
                        commandType = CommandType.point;
                        break;
                    case "0":
                        commandType = CommandType.erase;
                        break;
                    case "?":
                        commandType = CommandType.ifelse;
                        break;
                    case ".":
                        commandType = CommandType.stop;
                        break;
                    default:
                        throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: команда " + arguments[1] + " не является предусмотренной командой");
                }
                int nextLine1 = -1, nextLine2 = -1;
                switch (commandType)
                {
                    case CommandType.left:
                    case CommandType.right:
                    case CommandType.point:
                    case CommandType.erase:
                        {
                            if (arguments.Length == 2)
                                if (i == textCommands.Count - 1)
                                    nextLine1 = 0;
                                else
                                    nextLine1 = i + 2;
                            else if (arguments.Length == 3)
                            {
                                if (!int.TryParse(arguments[2], out nextLine1))
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей команде " + arguments[2] + " не число");
                                if (nextLine1 == i + 1)
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей команде " + arguments[2] + " ссылается на эту же команду");
                                if (nextLine1 > textCommands.Count)
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей команде " + arguments[2] + " ссылается на несуществующую команду");
                            }
                            else
                                throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: лишний аргумент " + arguments[3]);
                            break;
                        }
                    case CommandType.ifelse:
                        {
                            if (arguments.Length == 4)
                            {
                                if (!int.TryParse(arguments[2], out nextLine1))
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей строке " + arguments[2] + " не число");
                                if (!int.TryParse(arguments[3], out nextLine2))
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей строке " + arguments[3] + " не число");
                                if (nextLine1 == i + 1)
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей строке " + arguments[2] + " ссылается на эту же строку");
                                if (nextLine2 == i + 1)
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей строке " + arguments[3] + " ссылается на эту же строку");
                                if (nextLine1 > textCommands.Count)
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей строке " + arguments[2] + " ссылается на несуществующую строку");
                                if (nextLine2 > textCommands.Count)
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей строке " + arguments[3] + " ссылается на несуществующую строку");
                            }
                            else if (arguments.Length < 4)
                                throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: недостаточно аргументов");
                            else
                                throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: лишний аргумент " + arguments[4]);

                            break;
                        }
                    case CommandType.stop:
                        {
                            if (arguments.Length == 2)
                                break;
                            if (arguments.Length > 2)
                            {
                                if (arguments[2] == "0")
                                {
                                    if (arguments.Length == 3)
                                        break;
                                    else
                                        throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: лишний аргумент " + arguments[3]);
                                }
                                else
                                    throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей строке " + arguments[3] + " ссылается на строку отличную от 0");
                            }
                            break;
                        }
                    default:
                        throw new Exception("Получен непредусмотренный CommandType");
                }
                switch (commandType)
                {
                    case CommandType.left:
                        commands.Add(new MoveLeftCmd(nextLine1));
                        break;
                    case CommandType.right:
                        commands.Add(new MoveRightCmd(nextLine1));
                        break;
                    case CommandType.point:
                        commands.Add(new MarkCellCmd(nextLine1));
                        break;
                    case CommandType.erase:
                        commands.Add(new UnmarkCellCmd(nextLine1));
                        break;
                    case CommandType.ifelse:
                        commands.Add(new IfElseCmd(nextLine1, nextLine2));
                        break;
                    case CommandType.stop:
                        commands.Add(new StopCmd());
                        break;
                    default:
                        throw new Exception("Получен непредусмотренный CommandType");
                }
            }
            return commands;
        }
        public static List<Command> TextToCommands(string text)
        {
            return StringListToCommands(new List<string>(text.Split('\n')));
        }
        public static List<Command> TxtFileToCommands(string path)
        {
            List<string> textCommands = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    textCommands.Add(line);
                }
            }
            return StringListToCommands(textCommands);
        }
    }
}
