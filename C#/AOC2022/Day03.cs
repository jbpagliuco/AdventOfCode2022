using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	class Day03Input
	{
		public string bag;
		public string compartment1;
		public string compartment2;

		public Day03Input(string line)
		{
			bag = line;

			int compartmentSize = bag.Length / 2;
			compartment1 = bag.Substring(0, compartmentSize);
			compartment2 = bag.Substring(compartmentSize);
		}

		public char GetDuplicateItem()
		{
			foreach (char item1 in compartment1)
			{
				foreach (char item2 in compartment2)
				{
					if (item1 == item2)
					{
						return item1;
					}
				}
			}

			throw new Exception("Unreachable");
		}
	}

	class Day03
	{
		static int GetItemPriority(char item)
		{
			if (item >= 'a' && item <= 'z')
			{
				return item - 'a' + 1;
			}
			else if (item >= 'A' && item <= 'Z')
			{
				return item - 'A' + 27;
			}

			throw new Exception("Unreachable");
		}

		static char FindDuplicateItem(Day03Input bag1, Day03Input bag2, Day03Input bag3)
		{
			foreach (char item1 in bag1.bag)
			{
				foreach (char item2 in bag2.bag)
				{
					foreach (char item3 in bag3.bag)
					{
						if (item1 == item2 && item2 == item3)
						{
							return item1;
						}
					}
				}
			}

			throw new Exception("Unreachable");
		}


		public static void Run()
		{
			const string INPUT_FILENAME = "Day03.input.txt";
			List<Day03Input> input = Util.ReadInputFile<Day03Input>(INPUT_FILENAME);

			int part1Answer = input.Select(x => GetItemPriority(x.GetDuplicateItem())).Sum();
			Console.WriteLine($"Part 1: {part1Answer}"); // 8185

			int part2Answer = 0;
			for (int i = 0; i < input.Count; i += 3)
			{
				Day03Input bag1 = input[i];
				Day03Input bag2 = input[i + 1];
				Day03Input bag3 = input[i + 2];

				char duplicateItem = FindDuplicateItem(bag1, bag2, bag3);
				part2Answer += GetItemPriority(duplicateItem);
			}
			Console.WriteLine($"Part 2: {part2Answer}"); // 2817
		}
	}
}
