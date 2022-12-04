using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	class Util
	{
		// Read a file line by line
		public static List<string> ReadInputFileLines(string filename)
		{
			return System.IO.File.ReadLines($"{System.AppContext.BaseDirectory}\\..\\..\\..\\..\\..\\{filename}").ToList();
		}

		// Read a file line by line, and parse it into the given input type.
		public static List<InputType> ReadInputFile<InputType>(string filename) where InputType : class
		{
			List<InputType> output = new List<InputType>();

			var lines = Util.ReadInputFileLines(filename);
			foreach (string line in lines)
			{
				InputType input = System.Activator.CreateInstance(typeof(InputType), new object[] { line }) as InputType;
				output.Add(input);
			}

			return output;
		}
	}
}
