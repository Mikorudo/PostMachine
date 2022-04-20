using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ConsolePostMachine
{
	static class CommandInterpreter
	{
		private enum CommandType {
			left,
			right,
			point,
			erase,
			ifelse,
			stop
		}
		public static List<Command> TxtToCommands(string path)
		{
			List<string> textCommands = new List<string>();
			using (StreamReader reader = new StreamReader(path))
			{
				string? line;
				while ((line = reader.ReadLine()) != null)
				{
					textCommands.Add(line);
				}
			}
			List<Command> commands = new List<Command>();
			for (int i = 0; i < textCommands.Count; i++)
			{
				string[] line = textCommands[i].Split();
				int lineNum;
				if (!int.TryParse(line[0], out lineNum))
					throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: номер команды " + line[0] + " не число");
				if (lineNum != i + 1)
					throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: номер команды " + line[0] + " нарушает порядок команд");
				CommandType commandType;
				switch (line[1])
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
						throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: команда " + line[1] + " не является предусмотренной командой");
				}
				int nextLine1, nextLine2;
				switch (commandType)
				{
					case CommandType.left:
					case CommandType.right:
					case CommandType.point:
					case CommandType.erase:
						{
							if (line.Length == 2)
								nextLine1 = i + 2;
							else if (line.Length == 3)
							{
								if (!int.TryParse(line[2], out nextLine1))
									throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей команде " + line[2] + " не число");
								if (nextLine1 == i + 1)
									throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей команде " + line[2] + " ссылается на эту же команду");
								if (nextLine1 > textCommands.Count)
									throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей команде " + line[2] + " ссылается на несуществующую команду");
							}
							else
								throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: лишний аргумент " + line[3]);
							break;
						}
					case CommandType.ifelse:
						{
							if (line.Length == 4)
							{
								if (!int.TryParse(line[2], out nextLine1))
									throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей команде " + line[2] + " не число");
								if (nextLine1 == i + 1)
									throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей команде " + line[2] + " ссылается на эту же команду");
								if (nextLine1 > textCommands.Count)
									throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: переход к следующей команде " + line[2] + " ссылается на несуществующую команду");
							}
							else if (line.Length)
								throw new Exception("Строка (" + textCommands[i] + ") содержит ошибку: лишний аргумент " + line[3]);
							break;
						}
					case CommandType.stop:
						break;
					default:
						break;
				}
			}
		}
	}
}
