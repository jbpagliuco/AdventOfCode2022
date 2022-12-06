using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2022
{
	class Day05Crates
	{
		private List<Stack<char>> stacks = new List<Stack<char>>();

		public Day05Crates(List<string> lines)
		{
			int numStacks = lines[0].Length / 4 + 1;
			for (int i = 0; i < numStacks; ++i)
			{
				stacks.Add(new Stack<char>());
			}

			for (int lineIndex = lines.Count - 1; lineIndex >= 0; --lineIndex)
			{
				string line = lines[lineIndex];

				for (int i = 0; i < line.Length; i += 4)
				{
					char crateId = line[i + 1];
					if (crateId != ' ')
					{
						int stack = i / 4;
						stacks[stack].Push(crateId);
					}
				}
			}
		}

		public void RunInstruction(Day05Instruction instruction, bool moveOneAtATime)
		{
			if (moveOneAtATime)
			{
				for (int i = 0; i < instruction.move; ++i)
				{
					char crateId = stacks[instruction.from - 1].Pop();
					stacks[instruction.to - 1].Push(crateId);
				}
			}
			else
			{
				Stack<char> tempStack = new Stack<char>();
				for (int i = 0; i < instruction.move; ++i)
				{
					tempStack.Push(stacks[instruction.from - 1].Pop());
				}

				for (int i = 0; i < instruction.move; ++i)
				{
					stacks[instruction.to - 1].Push(tempStack.Pop());
				}
			}
		}

		public string GetTopLevelCrates()
		{
			string topLevelCrates = "";
			foreach (var stack in stacks)
			{
				topLevelCrates += stack.Peek();
			}

			return topLevelCrates;
		}
	}

	class Day05Instruction
	{
		private static Regex LINE_REGEX = new Regex(@"move (\d+) from (\d+) to (\d+)");

		public int move;
		public int from;
		public int to;

		public Day05Instruction(string line)
		{
			Match match = LINE_REGEX.Match(line);
			move = int.Parse(match.Groups[1].Value);
			from = int.Parse(match.Groups[2].Value);
			to = int.Parse(match.Groups[3].Value);
		}
	}

	class Day05InstructionList
	{
		public List<Day05Instruction> instructions = new List<Day05Instruction>();

		public Day05InstructionList(List<string> lines)
		{
			foreach (string line in lines)
			{
				instructions.Add(new Day05Instruction(line));
			}
		}
	}

	class Day05
	{
		static void ReadInputFile(out Day05Crates crates, out Day05InstructionList instructions)
		{
			const string INPUT_FILENAME = "Day05.input.txt";
			List<List<string>> lines = Util.ReadInputFileSplit(INPUT_FILENAME, "");

			crates = new Day05Crates(lines[0].Take(lines[0].Count - 1).ToList());
			instructions = new Day05InstructionList(lines[1]);
		}

		public static void Run()
		{
			Day05Crates crates;
			Day05InstructionList instructions;

			ReadInputFile(out crates, out instructions);
			foreach (Day05Instruction instruction in instructions.instructions)
			{
				crates.RunInstruction(instruction, true);
			}
			string part1Answer = crates.GetTopLevelCrates();
			Console.WriteLine($"Part 1: {part1Answer}"); // SHQWSRBDL

			ReadInputFile(out crates, out instructions);
			foreach (Day05Instruction instruction in instructions.instructions)
			{
				crates.RunInstruction(instruction, false);
			}
			string part2Answer = crates.GetTopLevelCrates();
			Console.WriteLine($"Part 2: {part2Answer}"); // CDTQZHBRS
		}
	}
}
