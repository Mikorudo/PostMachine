using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace ConsolePostMachine
{
	delegate void Command(int firstLine, int secondLine);
	class FormatException : Exception
	{
		public FormatException(string message) : base(message) { }
	}
	class NumerationException : Exception
	{
		public NumerationException(string message) : base(message) { }
	}
	class CommandInterpreter
	{
		private List<string> commands;
		private int commandCount;
		public IEnumerable<string> GetCommandText(int max)
		{
			for (int i = 0; i < max; i++)
			{
				if (i == commands.Count)
				{

				}
			}
		}
		public CommandInterpreter(string path)
		{
			commandCount = 0;
			using (StreamReader reader = new StreamReader(path))
			{
				string? line;
				while ((line = reader.ReadLine()) != null)
				{
					commands.Add(line);
					commandCount++;
				}
			}
		}

	}
}
