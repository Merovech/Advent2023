using System.Diagnostics;

namespace AdventOfCode2023
{
	public abstract class DayBase<T> : IDay
	{
		private bool _useSampleData = false;		// Change to use live data

		public int DayNumber
		{
			get; private set;
		}

		protected string InputFileName
		{
			get; private set;
		}

		public DayBase(int dayNumber)
		{
			DayNumber = dayNumber;
			InputFileName = $"InputFiles\\{(_useSampleData ? "SampleData" : "ProblemData")}\\Day{DayNumber}.txt";
		}

		public abstract T FirstProblem();

		public abstract T SecondProblem();

		public void RunDay()
		{
			Console.WriteLine($"Day {DayNumber}");
			Console.WriteLine("------");

			Stopwatch sw = new();
			try
			{
				sw.Start();
				T result = FirstProblem();
				sw.Stop();
				Console.WriteLine($"1: {result} ({string.Format("{0:N0}", sw.Elapsed.Ticks)} ticks)");
			}
			catch (NotImplementedException)
			{
				sw.Stop();
				Console.WriteLine($"1: Not Implemented");
			}

			sw.Reset();

			try
			{
				sw.Start();
				T result = SecondProblem();
				sw.Stop();
				Console.WriteLine($"2: {result} ({string.Format("{0:N0}", sw.Elapsed.Ticks)} ticks)");
			}
			catch (NotImplementedException)
			{
				sw.Stop();
				Console.WriteLine($"2: Not Implemented");
			}
		}

		#region IDay
		int IDay.DayNumber => DayNumber;

		object? IDay.FirstProblem()
		{
			return FirstProblem();
		}

		object? IDay.SecondProblem()
		{
			return SecondProblem();
		}

		void IDay.RunDay()
		{
			RunDay();
		}
		#endregion
	}
}