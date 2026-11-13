namespace lab10;
using Xunit;
public class StringCalculatorTests
{
    [Theory]
    [InlineData("",0)]
    [InlineData("7",7)]
    [InlineData("1,2,3,4",10)]
    [InlineData("1\n2,3", 6)]
    [InlineData("1000,1001,2", 1002)]
    [InlineData("//[*][%]\n1*2%3", 6)]
    [InlineData("//[***][%%]\n1***2%%3", 6)]
    public void AddNumbers(string input, int expected)
    {
        var calculator = new StringCalculator();
        var result = calculator.Add(input);
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void ReportsAllNegativeNumbers()
    {
        var calculator = new StringCalculator();
        var error =  Assert.Throws<ArgumentException>(() => calculator.Add("2,-4,-5"));
        
        Assert.Equal("negative", error.Message);
    }
}