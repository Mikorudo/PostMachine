using System;
using System.IO;
using System.Collections.Generic;

namespace ConsolePostMachine
{
	class Program
	{
		static void Main(string[] args)
		{
			var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			var filePath = Path.Combine(desktopPath, "commands.txt");
			List<Command> commands = CommandInterpreter.TxtToCommands(filePath);
			Machine machine = new Machine();
			machine.LoadCommands(commands);
			machine.ConsoleExecuteCommands();
		}
	}
}
