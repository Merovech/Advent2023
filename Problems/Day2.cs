namespace AdventOfCode2023.Problems
{
	internal class Day2 : DayBase<int>
	{
		public Day2() : base(2) { }

		public override int FirstProblem()
		{
			int idSum = 0;
			foreach (GameRecord record in ParseInputFile())
			{
				idSum += record.IsValid ? record.Id : 0;
			}

			return idSum;
		}

		public override int SecondProblem()
		{
			int finalSum = 0;
			foreach (GameRecord record in ParseInputFile())
			{
				var cubes = record.GetFewestCubesForValidGame();
				finalSum += (cubes.Red * cubes.Green * cubes.Blue);
			}

			return finalSum;
		}

		private IEnumerable<GameRecord> ParseInputFile()
		{
			using (StreamReader reader = new(InputFileName))
			{
				while (!reader.EndOfStream)
				{
					var line = reader.ReadLine()!;
					yield return ParseLine(line);
				}
			}
		}

		private GameRecord ParseLine(string line)
		{
			GameRecord record = new();

			// ID format is always "Game X:, so the ID always starts at 5.
			int colonPos = line.IndexOf(':');
			record.Id = int.Parse(line.Substring(5, colonPos - 5));

			string playsStr = line.Substring(colonPos + 2);
			var plays = playsStr.Split("; ");

			foreach (var play in plays)
			{
				var pairs = play.Split(", ");
				GamePlay gamePlay = new();

				foreach (var pair in pairs)
				{
					var parts = pair.Split(' ');
					switch (parts[1])
					{
						case "red":
							gamePlay.Red = int.Parse(parts[0]);
							break;

						case "green":
							gamePlay.Green = int.Parse(parts[0]);
							break;

						case "blue":
							gamePlay.Blue = int.Parse(parts[0]);
							break;
					}
				}

				record.Plays.Add(gamePlay);
			}

			return record;
		}

		#region Data Classes
		internal class GameRecord
		{
			private readonly int _maxRed = 12;
			private readonly int _maxGreen = 13;
			private readonly int _maxBlue = 14;

			public GameRecord()
			{
				Plays = new();
			}

			public int Id
			{
				get; set;
			}

			public List<GamePlay> Plays
			{
				get; private set;
			}

			public bool IsValid
			{
				get
				{
					foreach (var play in Plays)
					{
						if (play.Red > _maxRed || play.Green > _maxGreen || play.Blue > _maxBlue)
						{
							return false;
						}
					}

					return true;
				}
			}

			public (int Red, int Green, int Blue) GetFewestCubesForValidGame()
			{
				int maxRed = int.MinValue;
				int maxGreen = int.MinValue;
				int maxBlue = int.MinValue;

				foreach (var play in Plays)
				{
					maxRed = Math.Max(maxRed, play.Red);
					maxGreen = Math.Max(maxGreen, play.Green);
					maxBlue = Math.Max(maxBlue, play.Blue);
				}

				return (maxRed, maxGreen, maxBlue);
			}
		}

		internal struct GamePlay
		{
			public int Red;
			public int Blue;
			public int Green;
		}
		#endregion
	}
}
