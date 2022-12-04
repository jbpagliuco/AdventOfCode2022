using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	class Util
	{
		public static List<string> ReadInputFileLines(string filename)
		{
			return System.IO.File.ReadLines($"X:\\projects\\AdventOfCode\\2022\\{filename}").ToList();
		}

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
