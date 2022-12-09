using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	enum Day09Direction
	{ 
		UP = 'U',
		DOWN = 'D',
		LEFT = 'L',
		RIGHT = 'R'
	}

	class Day09Instruction
	{
		public Day09Direction direction;
		public int steps;

		public Day09Instruction(string line)
		{
			direction = (Day09Direction)line[0];
			steps = int.Parse(line.Substring(2));
		}
	}

	class Day09Position
	{
		public int x = 0;
		public int y = 0;
		
		public ValueTuple<int, int> GetHash()
		{
			return new ValueTuple<int, int>(x, y);
		}

		public bool IsNextTo(Day09Position other)
		{
			int dX = other.x - x;
			int dY = other.y - y;

			return Math.Abs(dX) <= 1 && Math.Abs(dY) <= 1;
		}

		public void Move(Day09Direction direction)
		{
			switch (direction)
			{
				case Day09Direction.UP:
					++y;
					break;

				case Day09Direction.DOWN:
					--y;
					break;

				case Day09Direction.LEFT:
					--x;
					break;

				case Day09Direction.RIGHT:
					++x;
					break;
			}
		}

		public void MoveTowards(Day09Position other)
		{
			int dX = other.x - x;
			int dY = other.y - y;

			bool isNextTo = Math.Abs(dX) <= 1 && Math.Abs(dY) <= 1;
			if (isNextTo)
			{
				return;
			}

			x += Math.Sign(dX);
			y += Math.Sign(dY);
		}
	}

	class Day09
	{
		static int GetUniqueTailPositions(Day09Instruction[] instructions, int ropeLength)
		{
			HashSet<ValueTuple<int, int>> visitedPositions = new HashSet<ValueTuple<int, int>>();

			Day09Position[] rope = new Day09Position[ropeLength];
			for (int i = 0; i < ropeLength; ++i)
			{
				rope[i] = new Day09Position();
			}

			foreach (Day09Instruction instruction in instructions)
			{
				for (int i = 0; i <  instruction.steps; ++i)
				{
					// Move the head
					rope[0].Move(instruction.direction);

					for (int ropeIndex = 1; ropeIndex < ropeLength; ++ropeIndex)
					{
						// move this knot
						rope[ropeIndex].MoveTowards(rope[ropeIndex - 1]);
					}

					// track the tail
					visitedPositions.Add(rope[ropeLength - 1].GetHash());
				}
			}

			return visitedPositions.Count;
		}

		public static void Run()
		{
			const string INPUT_FILENAME = "Day09.input.txt";
			List<Day09Instruction> instructions = Util.ReadInputFile<Day09Instruction>(INPUT_FILENAME);

			int part1Answer = GetUniqueTailPositions(instructions.ToArray(), 2);
			Console.WriteLine($"Part 1: {part1Answer}"); // 6339

			int part2Answer = GetUniqueTailPositions(instructions.ToArray(), 10);
			Console.WriteLine($"Part 2: {part2Answer}"); // 2541
		}
	}
}
