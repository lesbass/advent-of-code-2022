using Xunit;
using static advent_of_code_2022.Day03;

namespace advent_of_code_2022.tests;

public class Day03Tests
{
    [Fact]
    public void Part1_Test()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(7824, Part1(input));
    }

    [Fact]
    public void Part2_Test()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(2798, Part2(input));
    }
}