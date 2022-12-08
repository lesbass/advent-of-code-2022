namespace advent_of_code_2022;

public static class Day08
{
    public const string TestFileName = "Day08_test";
    public const string ProductionFileName = "Day08";

    public static int Part1(List<string> input)
    {
        var treeMap = Map.FromRawData(input);

        return treeMap.VisibleTrees.Count();
    }

    public static int Part2(List<string> input)
    {
        var treeMap = Map.FromRawData(input);

        return treeMap.ScenicScores.Max();
    }

    private static int FindScore(this List<Spot> spots, int height)
    {
        var score = spots.FindIndex(it => it.Height >= height);
        return score < 0 ? spots.Count : score + 1;
    }

    private record Spot(int X, int Y, int Height);

    private record Map(List<Spot> Spots)
    {
        public IEnumerable<Spot> VisibleTrees => Spots.Where(IsVisible);

        public IEnumerable<int> ScenicScores => Spots.Select(GetScenicScore);

        public static Map FromRawData(IEnumerable<string> inputs)
        {
            var spots = inputs
                .SelectMany((row, y) => row
                    .Select((it, x) => new Spot(x, y, int.Parse(it.ToString()))))
                .ToList();

            return new Map(spots);
        }

        private bool IsVisible(Spot spot)
        {
            var leftElements = Spots.Where(it => it.Y == spot.Y && it.X < spot.X && it.Height >= spot.Height);
            var rightElements = Spots.Where(it => it.Y == spot.Y && it.X > spot.X && it.Height >= spot.Height);
            var topElements = Spots.Where(it => it.X == spot.X && it.Y < spot.Y && it.Height >= spot.Height);
            var bottomElements = Spots.Where(it => it.X == spot.X && it.Y > spot.Y && it.Height >= spot.Height);

            return !leftElements.Any() || !rightElements.Any() || !topElements.Any() || !bottomElements.Any();
        }

        private int GetScenicScore(Spot spot)
        {
            var leftScore = Spots
                .Where(it => it.Y == spot.Y && it.X < spot.X)
                .OrderByDescending(it => it.X).ToList().FindScore(spot.Height);

            var rightScore = Spots
                .Where(it => it.Y == spot.Y && it.X > spot.X)
                .OrderBy(it => it.X).ToList().FindScore(spot.Height);

            var topScore = Spots
                .Where(it => it.X == spot.X && it.Y < spot.Y)
                .OrderByDescending(it => it.Y).ToList().FindScore(spot.Height);

            var bottomScore = Spots
                .Where(it => it.X == spot.X && it.Y > spot.Y)
                .OrderBy(it => it.X).ToList().FindScore(spot.Height);

            return leftScore * rightScore * topScore * bottomScore;
        }
    }
}