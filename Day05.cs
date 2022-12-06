namespace advent_of_code_2022;

public static class Day05
{
    public const string TestFileName = "Day05_test";
    public const string ProductionFileName = "Day05";

    public static string Part1(List<string> input)
    {
        var game = Game.FromInput(input);
        return game.ApplyCommands().TopCrates;
    }

    public static string Part2(List<string> input)
    {
        var game = Game.FromInput(input);
        return game.ApplyCommands(false).TopCrates;
    }

    public record Game(List<Stack> Stacks, List<Command> Commands)
    {
        public string TopCrates => string.Join("", Stacks.OrderBy(it => it.Id).Select(it => it.Crates.Last().Value));

        public Game ApplyCommands(bool reverse = true)
        {
            if (!Commands.Any()) return this;
            var currentCommand = Commands.First();

            var fromStack = Stacks.First(it => it.Id == currentCommand.FromStack);
            var toStack = Stacks.First(it => it.Id == currentCommand.ToStack);
            var crates = fromStack.Crates.Skip(fromStack.Crates.Count - currentCommand.Quantity).ToList();
            if (reverse) crates.Reverse();

            toStack.Crates.AddRange(crates);
            fromStack = fromStack with
            {
                Crates = fromStack.Crates.Take(fromStack.Crates.Count - currentCommand.Quantity).ToList()
            };

            var newStacks = Stacks.Where(it => !new List<int>
                { currentCommand.FromStack, currentCommand.ToStack }.Contains(it.Id)).ToList();

            newStacks.Add(fromStack);
            newStacks.Add(toStack);

            return new Game(newStacks, Commands.Skip(1).ToList()).ApplyCommands(reverse);
        }

        public static Game FromInput(List<string> input)
        {
            var limiter = input.IndexOf(string.Empty);
            var stacks = input.Take(limiter).ToList();
            var commands = input.Skip(limiter + 1).Select(Command.FromString).ToList();

            return new Game(ParseStacks(stacks), commands);
        }

        private static List<Stack> ParseStacks(List<string> stacks)
        {
            stacks.Reverse();
            var stacksCount = int.Parse(stacks[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last());

            var stacksOfCrate = Enumerable.Range(0, stacksCount).Select(_ => new List<Crate>()).ToList();

            foreach (var row in stacks.Skip(1))
            {
                var chunks = row.Chunk(4).ToList();
                for (var i = 0; i < chunks.Count; i++)
                {
                    var item = chunks[i];
                    var crate = string.Join("", item).Trim();
                    if (!string.IsNullOrEmpty(crate)) stacksOfCrate[i].Add(Crate.FromString(crate));
                }
            }

            return stacksOfCrate.Select((it, id) => new Stack(id + 1, it)).ToList();
        }
    }

    public record Crate(string Value)
    {
        public static Crate FromString(string s)
        {
            return new Crate(s.Replace("[", "").Replace("]", ""));
        }
    }

    public record Stack(int Id, List<Crate> Crates);

    public record Command(int Quantity, int FromStack, int ToStack)
    {
        public static Command FromString(string s)
        {
            var sSplit = s.Split(' ').Select(it => int.TryParse(it, out var val) ? val : -1)
                .Where(it => it > -1).ToArray();
            return new Command(sSplit[0], sSplit[1], sSplit[2]);
        }
    }
}