namespace AdventOfCode2023.Problems
{
	internal class Day1 : DayBase<int>
	{
		private readonly Dictionary<string, string> wordMap = new()
		{
			{ "one", "o1e" },
			{ "two", "t2o" },
			{ "three", "t3e" },
			{ "four", "f4r" },
			{ "five", "f5e" },
			{ "six", "s6x" },
			{ "seven", "s7n" },
			{ "eight", "e8t" },
			{ "nine", "n9e" }
		};

		public Day1() : base(1) { }

		public override int FirstProblem()
		{
			int result = 0;
			foreach (var line in ParseInputFile())
			{
				result += FindCalibrationValue(line);
			}

			return result;
		}

		public override int SecondProblem()
		{
			int result = 0;
			foreach (var line in ParseInputFile())
			{
				int calibrationValue = FindCalibrationValueWithWords(line);
				result += calibrationValue;
			}

			return result;
		}

		private int FindCalibrationValueWithWords(string str)
		{
			foreach ((var key, var value) in wordMap)
			{
				str = str.Replace(key, value);
			}

			return FindCalibrationValue(str);
		}

		private int FindCalibrationValue(string str)
		{
			int left = 0;
			int right = str.Length - 1;

			while (left <= right)
			{
				if (char.IsDigit(str[left]) && char.IsDigit(str[right]))
				{
					return int.Parse(str[left].ToString() + str[right].ToString());
				}

				if (!char.IsDigit(str[left]))
				{
					left++;
				}

				if (!char.IsDigit(str[right]))
				{
					right--;
				}
			}

			return 0;
		}

		private IEnumerable<string> ParseInputFile()
		{
			using (StreamReader reader = new(InputFileName))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine();
					if (!string.IsNullOrEmpty(line))
					{
						yield return line;
					}
				}
			}
		}
	}
}
