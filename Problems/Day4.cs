using System.Reflection.PortableExecutable;

namespace AdventOfCode2023.Problems
{
	internal class Day4 : DayBase<double>
	{
		public Day4() : base(4) { }

		public override double FirstProblem()
		{
			double score = 0;
			foreach (var line in ParseInputFile())
			{
				var item = GetNumbers(line);
				var winnerCount = GetWinnerCount(item.CardNumbers, item.WinningNumbers);
				var cardScore = winnerCount > 0 ? Math.Pow(2, winnerCount - 1) : 0;
				score += cardScore;
			}

			return score;
		}

		public override double SecondProblem()
		{
			Dictionary<int, int> cardCounts = new();
			List<int> winCounts = new();

			int currentCard = 1;
			foreach (var line in ParseInputFile())
			{
				var item = GetNumbers(line);
				winCounts.Add(GetWinnerCount(item.CardNumbers, item.WinningNumbers));
				cardCounts.Add(currentCard, 1);
				currentCard++;
			}

			foreach (var kvp in cardCounts)
			{
				var cardCount = kvp.Value;
				while (cardCount > 0)
				{
					var wins = winCounts[kvp.Key - 1];
					for (int i = 0; i < wins; i++)
					{
						cardCounts[kvp.Key + 1 + i]++;
					}

					cardCount--;
				}
			}

			return cardCounts.Sum(kvp => kvp.Value);
		}

		private int GetWinnerCount(List<int> cardNumbers, List<int> winningNumbers)
		{
			var winners = cardNumbers.Intersect(winningNumbers);
			var winnerCount = winners.Count();

			return winnerCount;
		}

		private IEnumerable<string> ParseInputFile()
		{
			using (StreamReader reader = new(InputFileName))
			{
				while (!reader.EndOfStream)
				{
					yield return reader.ReadLine()!;
				}
			}
		}

		private (List<int> CardNumbers, List<int> WinningNumbers) GetNumbers(string line)
		{
			List<int> cardNumbers = new();
			List<int> winningNumbers = new();
			List<int> listToUse = cardNumbers;
			
			var colonLocation = line!.IndexOf(':');
			int currentIdx = colonLocation + 2;
			while (currentIdx < line.Length - 1)
			{
				if (line[currentIdx] == '|')
				{
					listToUse = winningNumbers;
					currentIdx += 2;
				}
				else
				{
					int number = int.Parse(line[currentIdx].ToString() + line[currentIdx + 1].ToString());
					listToUse.Add(number);
					currentIdx += 3;
				}
			}

			return (cardNumbers, winningNumbers);
		}
	}
}
