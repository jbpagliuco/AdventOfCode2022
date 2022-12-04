using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	class Day01
	{
		static List<int> ReadInput()
		{
			int lastIndex = 0;
			List<int> output = new List<int> { 0 };

			var lines = Util.ReadInputFileLines("Day01.input.txt");
			foreach (string line in lines)
			{
				if (line.Length > 0)
				{
					output[lastIndex] += int.Parse(line);
				}
				else
				{
					output.Add(0);
					++lastIndex;
				}
			}

			return output;
		}

		public static void Run()
		{
			List<int> input = ReadInput();
			input = input.OrderByDescending(i => i).ToList();

			Console.WriteLine($"Part 1: {input[0]}"); // 67622
			Console.WriteLine($"Part 2: {input[0] + input[1] + input[2]}"); // 201491
		}
	}
}
