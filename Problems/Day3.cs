using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode2023.Problems
{
	internal class Day3 : DayBase<int>
	{
		public Day3() : base(3) { }

		public override int FirstProblem()
		{
			int answer = 0;
			string[] grid = ParseInputFile();
			int currentRow = 0;
			while (currentRow < grid.Length)
			{
				int startIdx = 0;
				int endIdx;

				while (startIdx < grid[currentRow].Length)
				{
					while (startIdx < grid[currentRow].Length && !char.IsDigit(grid[currentRow][startIdx]))
					{
						startIdx++;
					}

					if (startIdx == grid[currentRow].Length)
					{
						break;
					}

					endIdx = startIdx;

					// Found a digit.  Now get the number.
					while (endIdx < grid[currentRow].Length && char.IsDigit(grid[currentRow][endIdx]))
					{
						endIdx++;
					}

					// We have a number, so check for validity.
					if (IsNumberValid(grid, currentRow, startIdx, endIdx))
					{
						int number = int.Parse(grid[currentRow].Substring(startIdx, endIdx - startIdx));
						answer += number;
					}

					startIdx = endIdx;
				}

				currentRow++;
			}

			return answer;
		}

		public override int SecondProblem()
		{
			int answer = 0;
			string[] grid = ParseInputFile();
			int currentRow = 0;
			while (currentRow < grid.Length)
			{
				int currentIdx = 0;
				while (currentIdx < grid[currentRow].Length)
				{
					while (currentIdx < grid[currentRow].Length && grid[currentRow][currentIdx] != '*')
					{
						currentIdx++;
					}

					if (currentIdx != grid.Length)
					{
						answer += FindGearRatio(currentIdx, currentRow, grid);
					}

					currentIdx++;
				}

				currentRow++;
			}


			return answer;
		}

		private int FindGearRatio(int gearIndex, int rowNumber, string[] grid)
		{
			var locations = GetGearLocations(gearIndex, rowNumber, grid);
			if (locations.First == null || locations.Second == null)
			{
				return 0;
			}

			int first = GetGear(locations.First.Value.Column, locations.First.Value.Row, grid);
			int second = GetGear(locations.Second.Value.Column, locations.Second.Value.Row, grid);

			return first * second;
		}

		private int GetGear(int index, int rowNumber, string[] grid)
		{
			int start = index;
			int end = index;
			while (start > 0 && char.IsDigit(grid[rowNumber][start]))
			{
				start--;
			}

			if (start > 0 || !char.IsDigit(grid[rowNumber][start]))
			{
				// Fix start if it was found past position 0.
				start++;
			}

			while (end < grid[rowNumber].Length && char.IsDigit(grid[rowNumber][end]))
			{
				end++;
			}

			int gear = int.Parse(grid[rowNumber].Substring(start, end - start));
			return gear;
		}

		private ((int Row, int Column)? First, (int Row, int Column)? Second) GetGearLocations(int gearIndex, int rowNumber, string[] grid)
		{

			(int Row, int Column)? firstFoundDigit = null;
			(int Row, int Column)? secondFoundDigit = null;
			if (rowNumber > 0)
			{
				// Above
				if (gearIndex > 0)
				{
					if (char.IsDigit(grid[rowNumber - 1][gearIndex - 1]))
					{
						if (firstFoundDigit == null)
						{
							firstFoundDigit = (rowNumber - 1, gearIndex - 1);
						}
						else if (secondFoundDigit == null)
						{
							secondFoundDigit = (rowNumber - 1, gearIndex - 1);
							return (firstFoundDigit, secondFoundDigit);
						}
					}
				}

				// Special case: two numbers above, separated by a . in the middle
				if (gearIndex < grid[rowNumber - 1].Length && grid[rowNumber - 1][gearIndex] == '.')
				{
					if (gearIndex < grid[rowNumber - 1].Length && char.IsDigit(grid[rowNumber - 1][gearIndex]))
					{
						if (firstFoundDigit == null)
						{
							firstFoundDigit = (rowNumber - 1, gearIndex);
						}
						else
						{
							secondFoundDigit = (rowNumber - 1, gearIndex);
							return (firstFoundDigit, secondFoundDigit);
						}
					}
				}

				if (gearIndex < grid[rowNumber - 1].Length && grid[rowNumber - 1][gearIndex] == '.')
				{
					if (gearIndex < grid[rowNumber - 1].Length - 1)
					{
						if (char.IsDigit(grid[rowNumber - 1][gearIndex + 1]))
						{
							if (firstFoundDigit == null)
							{
								firstFoundDigit = (rowNumber - 1, gearIndex + 1);
							}
							else
							{
								secondFoundDigit = (rowNumber - 1, gearIndex + 1);
								return (firstFoundDigit, secondFoundDigit);
							}
						}
					}
				}

			}

			// Either side
			if (gearIndex > 0)
			{
				if (char.IsDigit(grid[rowNumber][gearIndex - 1]))
				{
					if (firstFoundDigit == null)
					{
						firstFoundDigit = (rowNumber, gearIndex - 1);
					}
					else
					{
						secondFoundDigit = (rowNumber, gearIndex - 1);
						return (firstFoundDigit, secondFoundDigit);
					}
				}
			}

			if (secondFoundDigit == null)
			{
				if (gearIndex < grid[rowNumber].Length - 1)
				{
					if (char.IsDigit(grid[rowNumber][gearIndex + 1]))
					{
						if (firstFoundDigit == null)
						{
							firstFoundDigit = (rowNumber, gearIndex + 1);
						}
						else
						{
							secondFoundDigit = (rowNumber, gearIndex + 1);
							return (firstFoundDigit, secondFoundDigit);
						}
					}
				}
			}

			// Below
			if (rowNumber < grid.Length - 1)
			{
				if (gearIndex > 0)
				{
					if (char.IsDigit(grid[rowNumber + 1][gearIndex - 1]))
					{
						if (firstFoundDigit == null)
						{
							firstFoundDigit = (rowNumber + 1, gearIndex - 1);
						}
						else
						{
							secondFoundDigit = (rowNumber + 1, gearIndex - 1);
							return (firstFoundDigit, secondFoundDigit);
						}
					}
				}
				if (secondFoundDigit == null)
				{
					if (gearIndex < grid[rowNumber + 1].Length && char.IsDigit(grid[rowNumber + 1][gearIndex]))
					{
						if (firstFoundDigit == null)
						{
							firstFoundDigit = (rowNumber + 1, gearIndex);
						}
						else
						{
							secondFoundDigit = (rowNumber + 1, gearIndex);
							return (firstFoundDigit, secondFoundDigit);
						}
					}

					if (secondFoundDigit == null)
					{
						if (gearIndex < grid[rowNumber + 1].Length)
						{
							if (char.IsDigit(grid[rowNumber + 1][gearIndex + 1]))
							{
								if (firstFoundDigit == null)
								{
									firstFoundDigit = (rowNumber + 1, gearIndex + 1);
								}
								else
								{
									secondFoundDigit = (rowNumber + 1, gearIndex + 1);
									return (firstFoundDigit, secondFoundDigit);
								}
							}
						}
					}
				}
			}

			return (null, null);
		}

		private bool IsNumberValid(string[] grid, int currentRow, int startIdx, int endIdx)
		{
			int startCheck = startIdx == 0 ? 0 : startIdx - 1;
			int endCheck = endIdx == grid[currentRow].Length ? grid[currentRow].Length - 1 : endIdx;

			// Above
			if (currentRow > 0)
			{
				for (int i = startCheck; i <= endCheck; i++)
				{
					if (grid[currentRow - 1][i] != '.' && !char.IsDigit(grid[currentRow - 1][i]))
					{
						return true;
					}
				}
			}

			// Below
			if (currentRow < grid.Length - 1)
			{
				for (int i = startCheck; i <= endCheck; i++)
				{
					if (grid[currentRow + 1][i] != '.' && !char.IsDigit(grid[currentRow + 1][i]))
					{
						return true;
					}
				}
			}

			if (startIdx > 0 && !char.IsDigit(grid[currentRow][startCheck]) && grid[currentRow][startCheck] != '.')
			{
				return true;
			}

			if (endIdx < grid[currentRow].Length - 1 && !char.IsDigit(grid[currentRow][endCheck]) && grid[currentRow][endCheck] != '.')
			{
				return true;
			}

			return false;

		}

		private string[] ParseInputFile()
		{
			return File.ReadAllLines(InputFileName);
		}
	}
}
