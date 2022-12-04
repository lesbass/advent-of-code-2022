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
            First.Sections.Count > Second.Sections.Count
                ? CheckCompleteOverlap(Second.Sections, First.Sections)
                : CheckCompleteOverlap(First.Sections, Second.Sections);

        public bool PartiallyOverlaps =>
            First.Sections.Intersect(Second.Sections).Any();

        private static bool CheckCompleteOverlap(IReadOnlyCollection<int> smallList, IEnumerable<int> bigList)
        {
            return smallList.Intersect(bigList).Count() == smallList.Count;
        }

        public static ElvesPair FromString(string row)
        {
            var elves = row.Split(',').Select(Elf.FromString).ToArray();
            return new ElvesPair(elves[0], elves[1]);
        }
    }
}