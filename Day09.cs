using LanguageExt;

namespace advent_of_code_2022;

public static class Day09
{
    public const string TestFileName = "Day09_test";
    public const string ProductionFileName = "Day09";

    public static int Part1(IEnumerable<string> input)
    {
        var commands = ParseCommands(input);
        return PlayRope(2, commands);
    }

    public static int Part2(List<string> input)
    {
        var commands = ParseCommands(input);
        return PlayRope(10, commands);
    }

    private static int PlayRope(int length, List<Command> commands)
    {
        var rope = Enumerable.Range(0, length).Select(Spot.Create).ToList();
        foreach (var command in commands)
            for (var i = 0; i < command.Steps; i++)
                rope = rope.Aggregate(Lst<Spot>.Empty,
                        (acc, spot) =>
                            acc.Add(!acc.Any() ? spot.MoveHead(command.Direction) : spot.MoveTail(acc.Last())))
                    .ToList();
        return rope.Last().PositionLog.Distinct().Count();
    }

    private static List<Command> ParseCommands(IEnumerable<string> input)
    {
        return input.Select(it => it.Split(' '))
            .Select(it => new Command(it[0], int.Parse(it[1])))
            .ToList();
    }

    private record Command(string Direction, int Steps);

    private record Spot(int X, int Y, Lst<string> PositionLog)
    {
        public static Spot Create(int i)
        {
            return new Spot(0, 0, Lst<string>.Empty.Add("0,0"));
        }

        public Spot MoveHead(string direction)
        {
            return direction switch
            {
                "R" => MoveRight(),
                "L" => MoveLeft(),
                "U" => MoveUp(),
                _ => MoveDown()
            };
        }

        public Spot MoveTail(Spot head)
        {
            if (Math.Abs(head.X - X) < 2 && Math.Abs(head.Y - Y) < 2) return this;
            if (head.Y == Y) return head.X < X ? MoveLeft() : MoveRight();
            if (head.X == X) return head.Y < Y ? MoveDown() : MoveUp();

            var newSpot = this with
            {
                X = X + (head.X > X ? 1 : -1), Y = Y + (head.Y > Y ? 1 : -1)
            };
            return newSpot with { PositionLog = AddPositionToLog(newSpot) };
        }

        private Spot MoveLeft()
        {
            var newSpot = this with { X = X - 1 };
            return newSpot with { PositionLog = AddPositionToLog(newSpot) };
        }

        private Spot MoveRight()
        {
            var newSpot = this with { X = X + 1 };
            return newSpot with { PositionLog = AddPositionToLog(newSpot) };
        }

        private Spot MoveUp()
        {
            var newSpot = this with { Y = Y + 1 };
            return newSpot with { PositionLog = AddPositionToLog(newSpot) };
        }

        private Spot MoveDown()
        {
            var newSpot = this with { Y = Y - 1 };
            return newSpot with { PositionLog = AddPositionToLog(newSpot) };
        }

        private Lst<string> AddPositionToLog(Spot newSpot)
        {
            return PositionLog.Add($"{newSpot.X},{newSpot.Y}");
        }
    }
}