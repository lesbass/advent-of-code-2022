using Xunit;
using static advent_of_code_2022.Day02;

namespace advent_of_code_2022.tests;

public class Day02Tests
{
    [Fact]
    public void Part1_Test()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(12276, Part1(input));
    }

    [Fact]
    public void Part2_Test()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(9975, Part2(input));
    }
}