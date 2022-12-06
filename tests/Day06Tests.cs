using Xunit;
using static advent_of_code_2022.Day06;

namespace advent_of_code_2022.tests;

public class Day06Tests
{
    [Fact]
    public void Part1_Test()
    {
        var input = Utils.ReadInput(TestFileName);

        Assert.Equal(11, Part1(input));
    }

    [Fact]
    public void Part1_Production()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResult("DAY06_PART1"), Part1(input));
    }

    [Fact]
    public void Part2_Test()
    {
        var input = Utils.ReadInput(TestFileName);

        Assert.Equal(26, Part2(input));
    }

    [Fact]
    public void Part2_Production()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResult("DAY06_PART2"), Part2(input));
    }
}