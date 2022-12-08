using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2022
{
	class Day07File
	{
		public string filename;
		public int size;

		public Day07File(string filename, int size)
		{
			this.filename = filename;
			this.size = size;
		}
	}

	class Day07Directory
	{
		public string name;

		public List<Day07Directory> directories = new List<Day07Directory>();
		public List<Day07File> files = new List<Day07File>();

		public Day07Directory parent = null;

		public Day07Directory(string name, Day07Directory parent = null)
		{
			this.name = name;
			this.parent = parent;
		}
	
		public Day07Directory GetSubdirectory(string name)
		{
			if (name == "..")
			{
				return parent;
			}

			foreach (Day07Directory dir in directories)
			{
				if (dir.name == name)
				{
					return dir;
				}
			}

			throw new Exception();
		}

		public List<Day07Directory> GetSubdirectoriesRecursively()
		{
			List<Day07Directory> allSubDirectories = new List<Day07Directory>(directories);
			
			foreach (Day07Directory dir in directories)
			{
				allSubDirectories.AddRange(dir.GetSubdirectoriesRecursively());
			}

			return allSubDirectories;
		}

		public void AddSubdirectory(string name)
		{
			directories.Add(new Day07Directory(name, this));
		}

		public void AddFile(string name, int size)
		{
			files.Add(new Day07File(name, size));
		}

		public int GetSize()
		{
			int size = 0;

			foreach (Day07File file in files)
			{
				size += file.size;
			}

			foreach (Day07Directory dir in directories)
			{
				size += dir.GetSize();
			}

			return size;
		}
	}

	class Day07Filesystem
	{
		public Day07Directory rootDirectory = new Day07Directory("/");

		private Day07Directory currentDirectory;

		public Day07Filesystem(List<string> lines)
		{
			foreach (string line in lines)
			{
				string lineToParse = line.StartsWith("$") ? line.Substring(2) : line;
				string[] tokens = lineToParse.Split(" ").ToArray();

				ProcessLine(tokens, line.StartsWith("$"));
			}
		}

		void ProcessLine(string[] tokens, bool isCommand)
		{
			if (tokens[0] == "cd")
			{
				if (tokens[1] == "/")
				{
					currentDirectory = rootDirectory;
				}
				else
				{
					currentDirectory = currentDirectory.GetSubdirectory(tokens[1]);
				}
			}
			else if (!isCommand)
			{
				if (tokens[0] == "dir")
				{
					currentDirectory.AddSubdirectory(tokens[1]);
				}
				else
				{
					currentDirectory.AddFile(tokens[1], int.Parse(tokens[0]));
				}
			}
		}
	}

	class Day07
	{
		public static void Run()
		{
			const string INPUT_FILENAME = "Day07.input.txt";
			Day07Filesystem filesystem = new Day07Filesystem(Util.ReadInputFileLines(INPUT_FILENAME));

			List<Day07Directory> allDirectories = filesystem.rootDirectory.GetSubdirectoriesRecursively();

			int part1Answer = allDirectories.Select(x => x.GetSize() <= 100000 ? x.GetSize() : 0).Sum();
			Console.WriteLine($"Part 1: {part1Answer}"); // 1350966

			const int TOTAL_DISK_SPACE = 70000000;
			const int UPDATE_SPACE_REQ = 30000000;
			int rootDirectorySize = filesystem.rootDirectory.GetSize();
			int unusedSpace = TOTAL_DISK_SPACE - rootDirectorySize;
			int spaceNeeded = UPDATE_SPACE_REQ - unusedSpace;

			int part2Answer = allDirectories.Select(x => x.GetSize() > spaceNeeded ? x.GetSize() : int.MaxValue).Min();
			Console.WriteLine($"Part 2: {part2Answer}"); // insert answer here
		}
	}
}
