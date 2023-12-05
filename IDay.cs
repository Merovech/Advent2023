namespace AdventOfCode2023
{
    public interface IDay
    {
        public int DayNumber { get; }

        public object? FirstProblem();

        public object? SecondProblem();

        public void RunDay();
    }
}