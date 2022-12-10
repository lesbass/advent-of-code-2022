namespace advent_of_code_2022;

public static class Day10
{
    public const string TestFileName = "Day10_test";
    public const string ProductionFileName = "Day10";

    public static int Part1(IEnumerable<string> input)
    {
        var commands = ParseCommands(input);
        var log = new List<int> { 1 };
        foreach (var command in commands)
        {
            log.Add(log.LastOrDefault());
            if (command.Value.HasValue) log.Add(log.Last() + command.Value.Value);
        }

        var total = 0;
        for (var i = 20; i < log.Count; i += 40) total += log[i - 1] * i;

        return total;
    }

    public static int Part2(List<string> input)
    {
        return 0;
    }

    private static List<Command> ParseCommands(IEnumerable<string> input)
    {
        return input.Select(it => it.Split(' '))
            .Select(it => it[0] == "noop" ? new Command(1) : new Command(2, int.Parse(it[1])))
            .ToList();
    }

    private record Command(int Cycles, int? Value = null);
}