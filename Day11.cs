namespace advent_of_code_2022;

public static class Day11
{
    public const string TestFileName = "Day11_test";
    public const string ProductionFileName = "Day11";

    public static int Part1(IEnumerable<string> input)
    {
        var monkeys = ParseData(input).ToList();
        const int rounds = 20;
        Enumerable.Range(1, rounds).ToList()
            .ForEach(_ =>
            {
                foreach (var monkey in monkeys)
                    ProcessMonkey(monkey, monkeys);
            });

        return monkeys.OrderByDescending(it => it.ProcessedItems).Take(2)
            .Aggregate(1, (acc, monkey) => acc * monkey.ProcessedItems);
    }

    public static int Part2(List<string> input)
    {
        return 0;
    }

    private static void ProcessMonkey(Monkey monkey, List<Monkey> monkeys)
    {
        Enumerable.Range(1, monkey.Items.Count).ToList()
            .ForEach(_ =>
            {
                var worryLevel = (int)Math.Floor(monkey.Operation(monkey.Items.First()) / 3m);
                MoveItem(
                    worryLevel % monkey.Test.DivisibleBy == 0 ? monkey.Test.IfTrue : monkey.Test.IfFalse,
                    worryLevel,
                    monkeys);
                monkey.Items = monkey.Items.Skip(1).ToList();
                monkey.ProcessedItems++;
            });
    }

    private static void MoveItem(int monkeyId, int item, IEnumerable<Monkey> monkeys)
    {
        monkeys.First(it => it.Id == monkeyId).Items.Add(item);
    }

    private static IEnumerable<Monkey> ParseData(IEnumerable<string> input)
    {
        return input.Chunk(7).Select(Monkey.FromData).ToList();
    }

    public class Monkey
    {
        public int ProcessedItems { get; set; }
        public int Id { get; set; }
        public List<int> Items { get; set; } = new();
        public Func<int, int> Operation { get; set; } = it => it;
        public Test Test { get; set; } = new();

        public static Monkey FromData(IEnumerable<string> rawData)
        {
            var data = rawData.ToList();
            var id = int.Parse(data[0].Split(' ')[1].Split(':')[0]);
            var items = data[1].Split(':')[1].Trim().Split(',').Select(it => int.Parse(it.Trim())).ToList();
            var operationSplit = data[2].Split('=')[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Skip(1).ToList();
            var divisibleBy = int.Parse(data[3].Split(' ').Last());
            var ifTrue = int.Parse(data[4].Split(' ').Last());
            var ifFalse = int.Parse(data[5].Split(' ').Last());

            var operation = operationSplit[0] switch
            {
                "+" => it => it + int.Parse(operationSplit[1]),
                "-" => it => it - int.Parse(operationSplit[1]),
                "*" when operationSplit[1] == "old" => it => it * it,
                "*" => it => it * int.Parse(operationSplit[1]),
                _ => new Func<int, int>(it => it)
            };

            return new Monkey
            {
                Id = id,
                Items = items,
                Operation = operation,
                Test = new Test
                {
                    DivisibleBy = divisibleBy,
                    IfTrue = ifTrue,
                    IfFalse = ifFalse
                }
            };
        }
    }

    public record Test
    {
        public int DivisibleBy { get; set; }
        public int IfTrue { get; set; }
        public int IfFalse { get; set; }
    }
}