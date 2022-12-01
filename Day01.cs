using LanguageExt;

namespace advent_of_code_2022;

public class Day01
{
    public const string TestFileName = "Day01_test";
    public const string ProductionFileName = "Day01";

    public static int Part1(List<string> input)
    {
        return GetElves(input).Max();
    }

    public static int Part2(List<string> input)
    {
        return GetElves(input).OrderDescending().Take(3).Sum();
    }

    private static IEnumerable<int> GetElves(IEnumerable<string> input)
    {
        return input.Aggregate(
            Lst<int>.Empty.Add(0),
            (elves, calories) =>
                int.TryParse(calories, out var val)
                    ? elves.SetItem(elves.Count - 1, elves.Last() + val)
                    : elves.Add(0));
    }
}