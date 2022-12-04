namespace advent_of_code_2022;

public static class Day03
{
    public const string TestFileName = "Day03_test";
    public const string ProductionFileName = "Day03";

    public static int Part1(IEnumerable<string> input)
    {
        var rucksacks = input.Select(Rucksack.FromString);
        var wrongPlacements = rucksacks.SelectMany(item => item.WrongPlacements.Distinct()).ToList();
        return wrongPlacements.Sum(GetValueForChar);
    }

    public static int Part2(IEnumerable<string> input)
    {
        var rucksacks = input.Select(Rucksack.FromString);
        var badges = rucksacks.Chunk(3).SelectMany(FindBadge);
        return badges.Sum(GetValueForChar);
    }

    private static int GetValueForChar(char c)
    {
        const string values = "%abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return values.IndexOf(c);
    }

    private static List<char> FindBadge(IEnumerable<Rucksack> rucksacks)
    {
        var uniqueItemsPerSack = rucksacks.Select(item => item.AllItems.Distinct()).ToList();
        return uniqueItemsPerSack.Aggregate(
            uniqueItemsPerSack.FirstOrDefault()?.ToList() ?? new List<char>(),
            (acc, chars) => (from a in acc
                join b in chars on a equals b
                select a).ToList());
    }

    public record Rucksack(char[] Compartment1, char[] Compartment2, char[] AllItems)
    {
        public IEnumerable<char> WrongPlacements =>
            (from a in Compartment1
                join b in Compartment2
                    on a equals b
                select a).ToArray();

        public static Rucksack FromString(string s)
        {
            var chars = s.Trim().ToCharArray();
            var midPoint = (int)Math.Floor(chars.Length / 2d);
            var compartments = chars.Chunk(midPoint).ToArray();
            return new Rucksack(compartments[0], compartments[1], chars.ToArray());
        }
    }
}