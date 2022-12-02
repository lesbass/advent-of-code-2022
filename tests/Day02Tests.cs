using Xunit;
using static advent_of_code_2022.Day02;

namespace advent_of_code_2022.tests;

public class Day02Tests
{
    [Fact]
    public void Part1()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(12276, Day02.Part1(input));
    }

    [Fact]
    public void Part2()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(209914, Day02.Part2(input));
    }
}