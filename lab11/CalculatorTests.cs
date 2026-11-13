using Xunit;

namespace lab11;

public class CalculatorTests
{
    [Theory]
    [InlineData(7, 2, 9)]
    [InlineData(-7, 2, -5)]
    [InlineData(0, 0, 0)]
    public void AddIntegers(int left, int right, int expected)
    {
        // Arrange — готуємо калькулятор.
        var calculator = new Calculator<int>();

        // Act — виконуємо додавання.
        int result = calculator.Add(left, right);

        // Assert — перевіряємо результат.
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(7, 2, 5)]
    [InlineData(2, 7, -5)]
    [InlineData(7, 7, 0)]
    public void SubtractIntegers(int left, int right, int expected)
    {
        var calculator = new Calculator<int>();
        int result = calculator.Subtract(left, right);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(8, 2, 4)]
    [InlineData(-8, 2, -4)]
    [InlineData(0, 2, 0)]
    [InlineData(7, 2, 3)]
    public void DivideIntegers(int left, int right, int expected)
    {
        var calculator = new Calculator<int>();
        int result = calculator.Divide(left, right);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(7, 2, 14)]
    [InlineData(-7, 2, -14)]
    [InlineData(7, 0, 0)]
    public void MultiplyIntegers(int left, int right, int expected)
    {
        var calculator = new Calculator<int>();
        int result = calculator.Multiply(left, right);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(2, 3, 8)]
    [InlineData(-2, 3, -8)]
    [InlineData(-2, 2, 4)]
    [InlineData(7, 0, 1)]
    [InlineData(0, 3, 0)]
    [InlineData(0, 0, 1)]
    [InlineData(2, -3, 0)]
    [InlineData(1, int.MinValue, 1)]
    public void PowIntegers(int value, int exponent, int expected)
    {
        var calculator = new Calculator<int>();
        int result = calculator.Pow(value, exponent);
        Assert.Equal(expected, result);
    }

    
    [Theory]
    [InlineData(1.5, 2.25, 3.75)]
    [InlineData(-1.5, 1.5, 0.0)]
    public void AddFractions(double left, double right, double expected)
    {
        var floatCalculator = new Calculator<float>();
        var doubleCalculator = new Calculator<double>();
        var decimalCalculator = new Calculator<decimal>();

        float floatResult = floatCalculator.Add((float)left, (float)right);
        double doubleResult = doubleCalculator.Add(left, right);
        decimal decimalResult = decimalCalculator.Add((decimal)left, (decimal)right);

        Assert.Equal((float)expected, floatResult);
        Assert.Equal(expected, doubleResult);
        Assert.Equal((decimal)expected, decimalResult);
    }

    [Theory]
    [InlineData(1.5, 2.25, -0.75)]
    [InlineData(2.5, 2.5, 0.0)]
    public void SubtractFractions(double left, double right, double expected)
    {
        var floatCalculator = new Calculator<float>();
        var doubleCalculator = new Calculator<double>();
        var decimalCalculator = new Calculator<decimal>();

        float floatResult = floatCalculator.Subtract((float)left, (float)right);
        double doubleResult = doubleCalculator.Subtract(left, right);
        decimal decimalResult = decimalCalculator.Subtract((decimal)left, (decimal)right);

        Assert.Equal((float)expected, floatResult);
        Assert.Equal(expected, doubleResult);
        Assert.Equal((decimal)expected, decimalResult);
    }

    [Theory]
    [InlineData(7.5, 2.5, 3.0)]
    [InlineData(7.0, 2.0, 3.5)]
    [InlineData(-7.5, 2.5, -3.0)]
    [InlineData(0.0, 2.5, 0.0)]
    public void DivideFractions(double left, double right, double expected)
    {
        var floatCalculator = new Calculator<float>();
        var doubleCalculator = new Calculator<double>();
        var decimalCalculator = new Calculator<decimal>();

        float floatResult = floatCalculator.Divide((float)left, (float)right);
        double doubleResult = doubleCalculator.Divide(left, right);
        decimal decimalResult = decimalCalculator.Divide((decimal)left, (decimal)right);

        Assert.Equal((float)expected, floatResult);
        Assert.Equal(expected, doubleResult);
        Assert.Equal((decimal)expected, decimalResult);
    }

    [Theory]
    [InlineData(1.5, 2.5, 3.75)]
    [InlineData(-1.5, 2.5, -3.75)]
    [InlineData(1.5, 0.0, 0.0)]
    public void MultiplyFractions(double left, double right, double expected)
    {
        var floatCalculator = new Calculator<float>();
        var doubleCalculator = new Calculator<double>();
        var decimalCalculator = new Calculator<decimal>();

        float floatResult = floatCalculator.Multiply((float)left, (float)right);
        double doubleResult = doubleCalculator.Multiply(left, right);
        decimal decimalResult = decimalCalculator.Multiply((decimal)left, (decimal)right);

        Assert.Equal((float)expected, floatResult);
        Assert.Equal(expected, doubleResult);
        Assert.Equal((decimal)expected, decimalResult);
    }

    [Theory]
    [InlineData(1.5, 2, 2.25)]
    [InlineData(-1.5, 3, -3.375)]
    [InlineData(2.0, -3, 0.125)]
    [InlineData(1.5, 0, 1.0)]
    [InlineData(0.0, 3, 0.0)]
    [InlineData(0.0, 0, 1.0)]
    [InlineData(1.0, int.MinValue, 1.0)]
    public void PowFractions(double value, int exponent, double expected)
    {
        var floatCalculator = new Calculator<float>();
        var doubleCalculator = new Calculator<double>();
        var decimalCalculator = new Calculator<decimal>();

        float floatResult = floatCalculator.Pow((float)value, exponent);
        double doubleResult = doubleCalculator.Pow(value, exponent);
        decimal decimalResult = decimalCalculator.Pow((decimal)value, exponent);

        Assert.Equal((float)expected, floatResult);
        Assert.Equal(expected, doubleResult);
        Assert.Equal((decimal)expected, decimalResult);
    }

    [Fact]
    public void DivideByZeroThrows()
    {
        var intCalculator = new Calculator<int>();
        var floatCalculator = new Calculator<float>();
        var doubleCalculator = new Calculator<double>();
        var decimalCalculator = new Calculator<decimal>();

        Assert.Throws<DivideByZeroException>(() => intCalculator.Divide(7, 0));
        Assert.Throws<DivideByZeroException>(() => floatCalculator.Divide(7f, 0f));
        Assert.Throws<DivideByZeroException>(() => doubleCalculator.Divide(7.0, 0.0));
        Assert.Throws<DivideByZeroException>(() => decimalCalculator.Divide(7m, 0m));
    }

    [Fact]
    public void ZeroToNegativePowerThrows()
    {
        var intCalculator = new Calculator<int>();
        var floatCalculator = new Calculator<float>();
        var doubleCalculator = new Calculator<double>();
        var decimalCalculator = new Calculator<decimal>();

        Assert.Throws<DivideByZeroException>(() => intCalculator.Pow(0, -1));
        Assert.Throws<DivideByZeroException>(() => floatCalculator.Pow(0f, -1));
        Assert.Throws<DivideByZeroException>(() => doubleCalculator.Pow(0.0, -1));
        Assert.Throws<DivideByZeroException>(() => decimalCalculator.Pow(0m, -1));
    }

    [Fact]
    public void UnsupportedTypesAreRejected()
    {
        Assert.Throws<NotSupportedException>(() => new Calculator<bool>());
        Assert.Throws<NotSupportedException>(() => new Calculator<char>());
        Assert.Throws<NotSupportedException>(() => new Calculator<long>());
        Assert.Throws<NotSupportedException>(() => new Calculator<DateTime>());
    }

    [Fact]
    public void DecimalOperationsPreservePrecision()
    {
        var calculator = new Calculator<decimal>();

        decimal sum = calculator.Add(0.1m, 0.2m);
        decimal power = calculator.Pow(1.000000000000000000000000001m, 2);

        Assert.Equal(0.3m, sum);
        Assert.Equal(1.000000000000000000000000002m, power);
    }
}
