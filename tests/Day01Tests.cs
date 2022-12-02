using Xunit;
using static advent_of_code_2022.Day01;

namespace advent_of_code_2022.tests;

public class Day01Tests
{
    [Fact]
    public void Part1()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(74198, Day01.Part1(input));
    }

    [Fact]
    public void Part2()
    {
        var input = Utils.ReadInput(ProductionFileName);

        Assert.Equal(209914, Day01.Part2(input));
    }
}