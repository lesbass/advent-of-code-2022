namespace advent_of_code_2022;

public static class Day06
{
    public const string TestFileName = "Day06_test";
    public const string ProductionFileName = "Day06";

    public static int Part1(IEnumerable<string> input)
    {
        return FindRepeatedSequences(input.First(), 4);
    }

    public static int Part2(IEnumerable<string> input)
    {
        return FindRepeatedSequences(input.First(), 14);
    }

    private static int FindRepeatedSequences(string signal, int length, int currentPos = 0)
    {
        return signal.Take(length).Distinct().Count() < length
            ? FindRepeatedSequences(signal[1..], length, currentPos + 1)
            : currentPos + length;
    }
}