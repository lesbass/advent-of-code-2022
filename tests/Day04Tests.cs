using Xunit;
using static advent_of_code_2022.Day04;

namespace advent_of_code_2022.tests;

public class Day04Tests
{
    [Fact]
    public void Part1_Test()
    {
        var input = Utils.ReadInput(TestFileName);

        Assert.Equal(2, Part1(input));
    }

    [Fact]
    public void Part1_Production()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResult("DAY04_PART1"), Part1(input));
    }

    [Fact]
    public void Part2_Test()
    {
        var input = Utils.ReadInput(TestFileName);

        Assert.Equal(4, Part2(input));
    }

    [Fact]
    public void Part2_Production()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(Utils.GetResult("DAY04_PART2"), Part2(input));
    }
}