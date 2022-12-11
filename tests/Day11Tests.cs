using Xunit;
using static advent_of_code_2022.Day11;

namespace advent_of_code_2022.tests;

public class Day11Tests
{
    [Fact]
    public void Part1_Test()
    {
        var input = Utils.ReadInput(TestFileName);

        Assert.Equal(10605, Part1(input));
    }

    [Fact]
    public void Part1_Production()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResult("DAY11_PART1"), Part1(input));
    }

    [Fact]
    public void Part2_Test()
    {
        var input = Utils.ReadInput(TestFileName);

        Assert.Equal(36, Part2(input));
    }

    [Fact]
    public void Part2_Production()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResult("DAY11_PART2"), Part2(input));
    }
}