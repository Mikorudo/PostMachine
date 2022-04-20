using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ConsolePostMachine
{
	class FormatException : Exception
	{
		public FormatException(string message) : base(message) { }
	}
	class NumerationException : Exception
	{
		public NumerationException(string message) : base(message) { }
	}
	static class CommandInterpreter
	{
		
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
		}
	}
}
