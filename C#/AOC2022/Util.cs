using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	class Util
	{
		// Read a file
		public static string ReadInputFile(string filename)
		{
			return System.IO.File.ReadAllText($"{System.AppContext.BaseDirectory}\\..\\..\\..\\..\\..\\{filename}");
		}

		// Read a file line by line
		public static List<string> ReadInputFileLines(string filename)
		{
			return System.IO.File.ReadLines($"{System.AppContext.BaseDirectory}\\..\\..\\..\\..\\..\\{filename}").ToList();
		}

		// Read a file line by line, and parse it into the given input type.
		public static List<InputType> ReadInputFile<InputType>(string filename) where InputType : class
		{
			List<InputType> output = new List<InputType>();

			var lines = ReadInputFileLines(filename);
			foreach (string line in lines)
			{
				InputType input = System.Activator.CreateInstance(typeof(InputType), new object[] { line }) as InputType;
				output.Add(input);
			}

			return output;
		}

		public static List<List<string>> ReadInputFileSplit(string filename, string delim)
		{
			List<List<string>> output = new List<List<string>>();
			output.Add(new List<string>());

			List<string> inputLines = ReadInputFileLines(filename);
			foreach (string line in inputLines)
			{
				if (line == delim)
				{
					output.Add(new List<string>());
					continue;
				}

				output.Last().Add(line);
			}

			return output;
		}
	}
}
