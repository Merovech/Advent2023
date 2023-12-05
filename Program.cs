using System.Reflection;

namespace AdventOfCode2023
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			var dayTypes = asm.GetTypes().Where(t => !t.IsAbstract && !t.IsInterface && t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(DayBase<>)).ToList();
			List<IDay> dayInstances = new();
			foreach (var dayType in dayTypes)
			{
				var instance = Activator.CreateInstance(dayType, dayType.GenericTypeArguments)!;
				dayInstances.Add((IDay)instance);
			}

			dayInstances = dayInstances.OrderBy(d => d.DayNumber).ToList();
			foreach (IDay dayInstance in dayInstances)
			{
				dayInstance.RunDay();
				Console.WriteLine();
			}

		}
	}
}