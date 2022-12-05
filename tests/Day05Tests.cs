using Xunit;
using static advent_of_code_2022.Day05;

namespace advent_of_code_2022.tests;

public class Day05Tests
{
    [Fact]
    public void Part1_Test()
    {
        var input = Utils.ReadInput(TestFileName);

        Assert.Equal("CMZ", Part1(input));
    }

    [Fact]
    public void Part1_Production()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResultRaw("DAY05_PART1"), Part1(input));
    }

    [Fact]
    public void Part2_Test()
    {
        var input = Utils.ReadInput(TestFileName);

        Assert.Equal("MCD", Part2(input));
    }

    [Fact]
    public void Part2_Production()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResultRaw("DAY05_PART2"), Part2(input));
    }
}