using System.Numerics;

namespace lab11;

public class Calculator<T> where T : INumber<T>
{
    public T Add(T left, T right) => left + right;

    public T Subtract(T left, T right) => left - right;

    public T Divide(T left, T right)
    {
        if (right == T.Zero)
            throw new DivideByZeroException("Ділення на нуль неможливе.");

        return left / right;
    }

    public T Multiply(T left, T right) => left * right;
    
    public T Pow(T value, int exponent)
    {
        T result = T.One;
        
        long power = exponent;
        if (power < 0)
        {
            value = Divide(T.One, value);
            power = -power;
        }

        while (power > 0)
        {
            if (power % 2 == 1)
                result = Multiply(result, value);

            power /= 2;
            if (power > 0)
                value = Multiply(value, value);
        }

        return result;
    }
}
