namespace lab11;

public class Calculator<T> where T : struct, IConvertible
{
    public Calculator()
    {
        // Обмеження where не дозволяє перелічити чотири конкретні типи.
        if (typeof(T) != typeof(int) && typeof(T) != typeof(float) &&
            typeof(T) != typeof(double) && typeof(T) != typeof(decimal))
        {
            throw new NotSupportedException("Дозволені лише int, float, double та decimal.");
        }
    }

    public T Add(T left, T right)
    {
        if (typeof(T) == typeof(int))
            return (T)(object)((int)(object)left + (int)(object)right);
        if (typeof(T) == typeof(float))
            return (T)(object)((float)(object)left + (float)(object)right);
        if (typeof(T) == typeof(double))
            return (T)(object)((double)(object)left + (double)(object)right);

        return (T)(object)((decimal)(object)left + (decimal)(object)right);
    }

    public T Subtract(T left, T right)
    {
        if (typeof(T) == typeof(int))
            return (T)(object)((int)(object)left - (int)(object)right);
        if (typeof(T) == typeof(float))
            return (T)(object)((float)(object)left - (float)(object)right);
        if (typeof(T) == typeof(double))
            return (T)(object)((double)(object)left - (double)(object)right);

        return (T)(object)((decimal)(object)left - (decimal)(object)right);
    }

    public T Divide(T left, T right)
    {
        if (right.Equals(default(T)))
            throw new DivideByZeroException("Ділення на нуль неможливе.");

        if (typeof(T) == typeof(int))
            return (T)(object)((int)(object)left / (int)(object)right);
        if (typeof(T) == typeof(float))
            return (T)(object)((float)(object)left / (float)(object)right);
        if (typeof(T) == typeof(double))
            return (T)(object)((double)(object)left / (double)(object)right);

        return (T)(object)((decimal)(object)left / (decimal)(object)right);
    }

    public T Multiply(T left, T right)
    {
        if (typeof(T) == typeof(int))
            return (T)(object)((int)(object)left * (int)(object)right);
        if (typeof(T) == typeof(float))
            return (T)(object)((float)(object)left * (float)(object)right);
        if (typeof(T) == typeof(double))
            return (T)(object)((double)(object)left * (double)(object)right);

        return (T)(object)((decimal)(object)left * (decimal)(object)right);
    }

    // Показник — ціле число. Результат зберігає тип T.
    public T Pow(T value, int exponent)
    {
        T one = (T)Convert.ChangeType(1, typeof(T));
        T result = one;
        // long дозволяє взяти модуль int.MinValue без переповнення.
        long power = exponent;
        if (power < 0)
        {
            value = Divide(one, value);
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
