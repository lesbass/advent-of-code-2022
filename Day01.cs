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

    private static IEnumerable<int> GetElves(IReadOnlyCollection<string> input)
    {
        var separators = input.Select((item, index) => string.IsNullOrEmpty(item) ? index : -1).Where(i => i > -1).ToList();
        separators.Add(input.Count);
        
        var currentStart = 0;
        foreach (var currentSeparatorPosition in separators)
        {
            var items = input.Skip(currentStart).Take(currentSeparatorPosition - currentStart);
            var currentCalories = items.Sum(item => int.TryParse(item, out var val) ? val : 0);
            currentStart = currentSeparatorPosition + 1;

            yield return currentCalories;
        }
    }
}