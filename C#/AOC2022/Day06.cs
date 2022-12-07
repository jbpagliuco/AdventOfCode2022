using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	class Day06
	{
		static int GetCharIndex(char c)
		{
			return c - 'a';
		}

		static int GetStartOfPacketMarker(string input, int length)
		{
			int[] charCounts = new int[26];

			for (int i = 0; i < input.Length; ++i)
			{
				++charCounts[GetCharIndex(input[i])];
				if (i >= length)
				{
					--charCounts[GetCharIndex(input[i - length])];

					bool isUnique = charCounts.All(x => x <= 1);
					if (isUnique)
					{
						return i + 1;
					}
				}
			}

			throw new Exception();
		}

		public static void Run()
		{
			const string INPUT_FILENAME = "Day06.input.txt";
			string input = Util.ReadInputFile(INPUT_FILENAME);

			int part1Answer = GetStartOfPacketMarker(input, 4);
			Console.WriteLine($"Part 1: {part1Answer}"); // 1282

			int part2Answer = GetStartOfPacketMarker(input, 14);
			Console.WriteLine($"Part 2: {part2Answer}"); // 3513
		}
	}
}
