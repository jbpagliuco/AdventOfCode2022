using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	using InputType = Day03InputType;

	struct Day03InputType
	{
		public string bag;
		public string compartment1;
		public string compartment2;

		public Day03InputType(string items)
		{
			bag = items;

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
		// Input filename
		static string INPUT_FILENAME = "Day03.input.txt";


		// Read input file
		static List<InputType> ReadInput()
		{
			List<InputType> output = new List<InputType>();

			var lines = Util.ReadInputFileLines(INPUT_FILENAME);
			foreach (string line in lines)
			{
				InputType bag = new InputType(line);
				output.Add(bag);
			}

			return output;
		}

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

		static char FindDuplicateItem(InputType bag1, InputType bag2, InputType bag3)
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
			List<InputType> input = ReadInput();

			int totalPriority1 = input.Select(x => GetItemPriority(x.GetDuplicateItem())).Sum();
			Console.WriteLine($"Part 1: {totalPriority1}"); // 8185

			int totalPriority2 = 0;
			for (int i = 0; i < input.Count; i += 3)
			{
				InputType bag1 = input[i];
				InputType bag2 = input[i + 1];
				InputType bag3 = input[i + 2];

				char duplicateItem = FindDuplicateItem(bag1, bag2, bag3);
				totalPriority2 += GetItemPriority(duplicateItem);
			}
			Console.WriteLine($"Part 2: {totalPriority2}"); // 2817
		}
	}
}
