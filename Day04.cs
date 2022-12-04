namespace advent_of_code_2022;

public static class Day04
{
    public const string TestFileName = "Day04_test";
    public const string ProductionFileName = "Day04";

    public static int Part1(IEnumerable<string> input)
    {
        var pairs = input.Select(ElvesPair.FromString).ToList();
        var overlaps = pairs.Where(it => it.CompletelyOverlaps);
        return overlaps.Count();
    }

    public static int Part2(IEnumerable<string> input)
    {
        var pairs = input.Select(ElvesPair.FromString).ToList();
        var overlaps = pairs.Where(it => it.PartiallyOverlaps);
        return overlaps.Count();
    }

    public record Elf(List<int> Sections)
    {
        public static Elf FromString(string assignedSections)
        {
            var minMax = assignedSections.Split('-').Select(int.Parse).ToArray();

            return new Elf(Enumerable.Range(minMax[0], minMax[1] - minMax[0] + 1).ToList());
        }
    }

    public record ElvesPair(Elf First, Elf Second)
    {
        public bool CompletelyOverlaps =>
            Second.Sections.All(n => First.Sections.Contains(n)) ||
            First.Sections.All(n => Second.Sections.Contains(n));

        public bool PartiallyOverlaps =>
            Second.Sections.Any(n => First.Sections.Contains(n));

        public static ElvesPair FromString(string row)
        {
            var elves = row.Split(',').Select(Elf.FromString).ToArray();
            return new ElvesPair(elves[0], elves[1]);
        }
    }
}