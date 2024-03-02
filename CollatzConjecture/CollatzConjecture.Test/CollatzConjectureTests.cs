using CollatzConjecture.Logic;

namespace CollatzConjecture.Test;

public class CollatzConjectureTests
{
    [Fact]
    public void IsSequenceValid_EveryNumberIsValid_ReturnsTrue()
    {
        var service = new CollatzConjectureServiceImpl();
        for (var i = 1; i < 100; i++)
        {
            var result = service.IsSequenceValid(i);
            Assert.True(result);
        }
    }
    
    [Fact]
    public void IsSequenceValid_EveryBigNumberIsValid_ReturnsTrue()
    {
        var service = new CollatzConjectureServiceImpl();
        var testNumbers = new List<string>
        {
            "10000000000000",
            "42183901823981203",
            "85395823904820941",
            "9402810938129082",
            "3821038672806112"
        };
        
        foreach (var number in testNumbers)
        {
            var result = service.IsSequenceValid(number);
            Assert.True(result);
        }
    }
    
    [Fact]
    public void GetSequence_InvalidInput_ReturnsEmptyList()
    {
        var service = new CollatzConjectureServiceImpl();
        var result = service.GetSequence("abc");
        Assert.Empty(result);
    }
}