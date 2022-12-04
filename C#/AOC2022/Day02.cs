using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	using InputType = ValueTuple<char, char>;

	class Day02
	{
		// Points for drawing or winning a round
		static int DRAW_POINTS = 3;
		static int WIN_POINTS = 6;

		class Day02Input
		{
			public char entry1;
			public char entry2;

			public Day02Input(string line)
			{
				entry1 = line[0];
				entry2 = line[2];
			}
		}

		static bool IsWinner(char myChoice, char theirChoice)
		{
			return
				(myChoice == 'X' && theirChoice == 'C') ||
				(myChoice == 'Y' && theirChoice == 'A') ||
				(myChoice == 'Z' && theirChoice == 'B');
		}

		static int CalculatePointsA(char myChoice, char theirChoice)
		{
			// Conversion factor for converting from 'their' choice to 'my' choice
			const int CONVERSION_FACTOR = 'X' - 'A';

			int points = 0;

			points += (int)myChoice - 'X' + 1;

			if (myChoice == (theirChoice + CONVERSION_FACTOR))
			{
				points += DRAW_POINTS;
			}
			else if (IsWinner(myChoice, theirChoice))
			{
				points += WIN_POINTS;
			}

			return points;
		}

		static int CalculatePointsB(char theirChoice, char result)
		{
			const char RESULT_LOSE = 'X';
			const char RESULT_DRAW = 'Y';
			const char RESULT_WIN = 'Z';
			int[] RESULT_POINTS = { 0, DRAW_POINTS, WIN_POINTS };
			int[] LOSE_CHOICES = { 'C', 'A', 'B' };
			int[] WIN_CHOICES = { 'B', 'C', 'A' };


			int points = 0;

			// Calculate points from result
			points += RESULT_POINTS[result - 'X'];

			// Calculate points from my choice
			int myChoice = 0;
			switch (result)
			{
				case RESULT_LOSE:
					myChoice = LOSE_CHOICES[theirChoice - 'A'];
					break;

				case RESULT_DRAW:
					myChoice = theirChoice;
					break;

				case RESULT_WIN:
					myChoice = WIN_CHOICES[theirChoice - 'A'];
					break;
			}

			points += myChoice - 'A' + 1;

			return points;
		}

		public static void Run()
		{
			const string INPUT_FILENAME = "Day02.input.txt";
			List<Day02Input> input = Util.ReadInputFile<Day02Input>(INPUT_FILENAME);

			int part1Answer = input.Select(x => CalculatePointsA(x.entry2, x.entry1)).Sum();
			Console.WriteLine($"Part 1: {part1Answer}"); // 13682

			int part2Answer = input.Select(x => CalculatePointsB(x.entry1, x.entry2)).Sum();
			Console.WriteLine($"Part 2: {part2Answer}"); // 12881
		}
	}
}
