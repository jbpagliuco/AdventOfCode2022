using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2022
{
	class Day08Cell
	{
		public int height;
		public bool visible = false;
		public int ssLeft = 0;
		public int ssRight = 0;
		public int ssUp = 0;
		public int ssDown = 0;

		public Day08Cell(int height)
		{
			this.height = height;
		}

		public void SetVisibility(int maxTreeHeight)
		{
			if (!visible)
			{
				visible = height > maxTreeHeight;
			}
		}
	}

	class Day08Grid
	{
		Day08Cell[,] cells;

		public Day08Grid(string[] lines)
		{
			cells = new Day08Cell[lines[0].Length, lines.Length];

			for (int i = 0; i < lines[0].Length; ++i)
			{
				for (int j = 0; j < lines.Length; ++j)
				{
					int height = int.Parse(lines[j][i].ToString());
					cells[i, j] = new Day08Cell(height);
				}
			}
		}

		public int CalculateVisiblity()
		{
			int gridWidth = cells.GetLength(0);
			int gridHeight = cells.GetLength(1);

			for (int y = 0; y < gridHeight; ++y)
			{
				// Go over cells from left to right
				int maxTreeHeight = -1;
				for (int x = 0; x < gridWidth; ++x)
				{
					cells[x, y].SetVisibility(maxTreeHeight);
					maxTreeHeight = Math.Max(maxTreeHeight, cells[x, y].height);
				}

				// Go over cells from right to left
				maxTreeHeight = -1;
				for (int x = gridWidth - 1; x >= 0; --x)
				{
					cells[x, y].SetVisibility(maxTreeHeight);
					maxTreeHeight = Math.Max(maxTreeHeight, cells[x, y].height);
				}
			}

			for (int x = 0; x < gridWidth; ++x)
			{
				// Go over cells from top to bottom
				int maxTreeHeight = -1;
				for (int y = 0; y < gridHeight; ++y)
				{
					cells[x, y].SetVisibility(maxTreeHeight);
					maxTreeHeight = Math.Max(maxTreeHeight, cells[x, y].height);
				}

				// Go over cells from bottom to top
				maxTreeHeight = -1;
				for (int y = gridHeight - 1; y >= 0; --y)
				{
					cells[x, y].SetVisibility(maxTreeHeight);
					maxTreeHeight = Math.Max(maxTreeHeight, cells[x, y].height);
				}
			}

			int numVisible = 0;
			foreach (Day08Cell cell in cells)
			{
				if (cell.visible)
				{
					++numVisible;
				}
			}

			return numVisible;
		}

		public int CalculateScenicScore()
		{
			int gridWidth = cells.GetLength(0);
			int gridHeight = cells.GetLength(1);



			for (int y = 1; y < gridHeight - 1; ++y)
			{
				// Go over cells from left to right
				for (int x = 1; x < gridWidth - 1; ++x)
				{
					Day08Cell currCell = cells[x, y];
					for (int xx = x - 1; xx >= 0; --xx)
					{
						++currCell.ssLeft;
						if (currCell.height <= cells[xx, y].height)
						{
							break;
						}
					}
				}

				// Go over cells from right to left
				for (int x = gridWidth - 2; x >= 1; --x)
				{
					Day08Cell currCell = cells[x, y];
					for (int xx = x + 1; xx < gridWidth; ++xx)
					{
						++currCell.ssRight;
						if (currCell.height <= cells[xx, y].height)
						{
							break;
						}
					}
				}
			}

			for (int x = 1; x < gridWidth - 1; ++x)
			{
				// Go over cells from left to right
				for (int y = 1; y < gridHeight - 1; ++y)
				{
					Day08Cell currCell = cells[x, y];
					for (int yy = y - 1; yy >= 0; --yy)
					{
						++currCell.ssUp;
						if (currCell.height <= cells[x, yy].height)
						{
							break;
						}
					}
				}

				// Go over cells from right to left
				for (int y = gridHeight - 2; y >= 1; --y)
				{
					Day08Cell currCell = cells[x, y];
					for (int yy = y + 1; yy < gridHeight; ++yy)
					{
						++currCell.ssDown;
						if (currCell.height <= cells[x, yy].height)
						{
							break;
						}
					}
				}
			}

			int maxSceneScore = 0;
			foreach (Day08Cell cell in cells)
			{
				maxSceneScore = Math.Max(maxSceneScore, cell.ssLeft * cell.ssRight * cell.ssUp * cell.ssDown);
			}

			return maxSceneScore;
		}
	}

	class Day08
	{
		public static void Run()
		{
			const string INPUT_FILENAME = "Day08.input.txt";
			List<string> input = Util.ReadInputFileLines(INPUT_FILENAME);

			Day08Grid grid = new Day08Grid(input.ToArray());

			int part1Answer = grid.CalculateVisiblity();
			Console.WriteLine($"Part 1: {part1Answer}"); // 1647

			int part2Answer = grid.CalculateScenicScore();
			Console.WriteLine($"Part 2: {part2Answer}"); // 392080
		}
	}
}
