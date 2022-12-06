namespace advent_of_code_2022;

public static class Day02
{
    public const string TestFileName = "Day02_test";
    public const string ProductionFileName = "Day02";

    public static int Part1(List<string> input)
    {
        var matches = ParseMatches(input);
        return matches.Sum(m => m.Result);
    }

    public static int Part2(List<string> input)
    {
        var matches = ParseResults(input);
        return matches.Sum(m => m.Result);
    }

    private static List<Match> ParseMatches(IEnumerable<string> input)
    {
        return input.Select(ParseMatch).ToList();
    }

    private static Match ParseMatch(string row)
    {
        var rowSplit = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var p1 = rowSplit[0][0];
        var p2 = rowSplit[1][0];

        var secretDict = new Dictionary<char, char>
        {
            { 'X', 'A' },
            { 'Y', 'B' },
            { 'Z', 'C' }
        };

        return Match.FromStrings(p1, secretDict[p2]);
    }

    private static List<Match> ParseResults(IEnumerable<string> input)
    {
        return input.Select(ParseResult).ToList();
    }

    private static Match ParseResult(string row)
    {
        var rowSplit = row.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var p1 = rowSplit[0][0];
        var result = rowSplit[1][0];

        return Match.FromResult(p1, result);
    }

    private enum Results
    {
        Draw = 3,
        Win = 6
    }

    public abstract record Bet
    {
        public abstract char Letter { get; }
        protected abstract int Value { get; }

        public abstract Bet WinsAgainst { get; }
        public abstract Bet LosesAgainst { get; }

        public int Play(Bet other)
        {
            if (other.GetType() == GetType()) return Value + (int)Results.Draw;
            if (other.GetType() == WinsAgainst.GetType()) return Value + (int)Results.Win;
            return Value;
        }
    }

    private record Rock : Bet
    {
        public override char Letter => 'A';
        protected override int Value => 1;

        public override Bet WinsAgainst => new Scissors();
        public override Bet LosesAgainst => new Paper();
    }

    private record Paper : Bet
    {
        public override char Letter => 'B';
        protected override int Value => 2;

        public override Bet WinsAgainst => new Rock();
        public override Bet LosesAgainst => new Scissors();
    }

    private record Scissors : Bet
    {
        public override char Letter => 'C';
        protected override int Value => 3;

        public override Bet WinsAgainst => new Paper();
        public override Bet LosesAgainst => new Rock();
    }

    public record Match(Bet Player1, Bet Player2)
    {
        public int Result => Player2.Play(Player1);

        public static Match FromStrings(char p1, char p2)
        {
            var bets = new List<Bet> { new Scissors(), new Paper(), new Rock() }
                .ToDictionary(item => item.Letter, item => item);

            var player1 = bets[p1];
            var player2 = bets[p2];
            return new Match(player1, player2);
        }

        public static Match FromResult(char p1, char result)
        {
            var bets = new List<Bet> { new Scissors(), new Paper(), new Rock() }
                .ToDictionary(item => item.Letter, item => item);

            var player1 = bets[p1];

            var player2 = result switch
            {
                'X' => player1.WinsAgainst,
                'Z' => player1.LosesAgainst,
                _ => player1
            };

            return new Match(player1, player2);
        }
    }
}