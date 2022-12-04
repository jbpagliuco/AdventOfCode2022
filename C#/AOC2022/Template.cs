using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	class DayXXInput
	{
		public DayXXInput(string line)
		{
		}
	}

	class DayXX
	{
		public static void Run()
		{
			const string INPUT_FILENAME = "DayXX.input.txt";
			List<DayXXInput> input = Util.ReadInputFile<DayXXInput>(INPUT_FILENAME);

			int part1Answer = 0;
			Console.WriteLine($"Part 1: {part1Answer}"); // insert answer here

			int part2Answer = 0;
			Console.WriteLine($"Part 2: {part2Answer}"); // insert answer here
		}
	}
}
