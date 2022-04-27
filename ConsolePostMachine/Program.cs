using System;
using System.IO;
using System.Collections.Generic;
using PostMachineLib;

namespace ConsolePostMachine
{
    class Program
	{
		static void Main(string[] args)
		{
			var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
			var filePath = Path.Combine(desktopPath, "commands.txt");
			List<Command> commands = CommandInterpreter.TxtToCommands(filePath);
			ConsoleMachine machine = new ConsoleMachine();
			machine.LoadCommands(commands);
			machine.ExecuteCommands();
		}
	}
}
