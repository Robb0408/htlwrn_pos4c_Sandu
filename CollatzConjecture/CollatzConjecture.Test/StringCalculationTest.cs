using CollatzConjecture.Logic;

namespace CollatzConjecture.Test;

public class StringCalculationTest
{
    [Fact]
    public void AddOne_AddOneToLargeNumber_ReturnsNumberPlusOne()
    {
        var service = new CollatzConjectureServiceImpl();
        var result = service.AddOne("50000000000000000");
        Assert.Equal("50000000000000001", result);
    }
    
    [Fact]
    public void AddOne_ChangesPartOfNumber_ReturnsNumberPlusOne()
    {
        var service = new CollatzConjectureServiceImpl();
        var result = service.AddOne("2000099999999999");
        Assert.Equal("2000100000000000", result);
    }

    [Fact]
    public void AddOne_ExtendsLengthOfNumberWithCarryOver_ReturnsNumberPlusOneWithCarryOver()
    {
        var service = new CollatzConjectureServiceImpl();
        var result = service.AddOne("999999999999999999");
        Assert.Equal("1000000000000000000", result);
    }

    [Fact]
    public void DivideByTwo_DivideLargeNumberByTwo_ReturnsNumberDividedByTwo()
    {
        var service = new CollatzConjectureServiceImpl();
        var result = service.DivideByTwo("50000000000000000");
        Assert.Equal("25000000000000000", result);
    }
    
    [Fact]
    public void MultiplyByThree_MultiplyLargeNumberByThree_ReturnsNumberMultipliedByThree()
    {
        var service = new CollatzConjectureServiceImpl();
        var result = service.MultiplyByThree("50000000000000000");
        Assert.Equal("150000000000000000", result);
    }
    
}