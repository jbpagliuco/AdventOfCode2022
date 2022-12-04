using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	struct Day04Section
	{
		public int start;
		public int end;

		public Day04Section(string line)
		{
			string[] split = line.Split('-');
			start = int.Parse(split[0]);
			end = int.Parse(split[1]);
		}

		public bool IsFullyContained(Day04Section section)
		{
			return start >= section.start && end <= section.end;
		}

		public bool IsOverlapped(Day04Section section)
		{
			return end >= section.start && start <= section.end;
		}
	}

	class Day04Input
	{
		public Day04Section section1;
		public Day04Section section2;

		public Day04Input(string line)
		{
			string[] split = line.Split(',');
			section1 = new Day04Section(split[0]);
			section2 = new Day04Section(split[1]);
		}

		public bool IsOneSectionFullyContained()
		{
			return section1.IsFullyContained(section2) || section2.IsFullyContained(section1);
		}

		public bool DoSectionsOverlap()
		{
			return section1.IsOverlapped(section2) || section2.IsOverlapped(section1);
		}
	}

	class Day04
	{
		public static void Run()
		{
			const string INPUT_FILENAME = "Day04.input.txt";
			List<Day04Input> input = Util.ReadInputFile<Day04Input>(INPUT_FILENAME);

			int part1Answer = input.Select(x => x.IsOneSectionFullyContained() ? 1 : 0).Sum();
			Console.WriteLine($"Part 1: {part1Answer}"); // 573

			int part2Answer = input.Select(x => x.DoSectionsOverlap() ? 1 : 0).Sum();
			Console.WriteLine($"Part 2: {part2Answer}"); // 867
		}
	}
}
