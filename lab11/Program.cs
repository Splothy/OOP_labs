using lab11;
using System.Numerics;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Лабораторна робота 11. Узагальнений Calculator\n");

ShowIntegerCalculator(new Calculator<int>(), 7, 2);
ShowFractionalCalculator(new Calculator<float>(), 7.5f, 2.5f);
ShowFractionalCalculator(new Calculator<double>(), 7.5, 2.5);
ShowFractionalCalculator(new Calculator<decimal>(), 7.5m, 2.5m);

// Перший generic-метод приймає цілочисельний калькулятор.
static void ShowIntegerCalculator<T>(Calculator<T> calculator, T left, T right)
    where T : INumber<T>
{
    Console.WriteLine($"Цілочисельний калькулятор ({typeof(T).Name}):");
    Console.WriteLine($"{left} + {right} = {calculator.Add(left, right)}");
    Console.WriteLine($"{left} - {right} = {calculator.Subtract(left, right)}");
    Console.WriteLine($"{left} / {right} = {calculator.Divide(left, right)}");
    Console.WriteLine($"{left} * {right} = {calculator.Multiply(left, right)}");
    Console.WriteLine($"{left} ^ 3 = {calculator.Pow(left, 3)}\n");
}

// Другий generic-метод приймає дробові калькулятори.
static void ShowFractionalCalculator<T>(Calculator<T> calculator, T left, T right)
    where T : INumber<T>
{
    Console.WriteLine($"Дробовий калькулятор ({typeof(T).Name}):");
    Console.WriteLine($"{left} + {right} = {calculator.Add(left, right)}");
    Console.WriteLine($"{left} - {right} = {calculator.Subtract(left, right)}");
    Console.WriteLine($"{left} / {right} = {calculator.Divide(left, right)}");
    Console.WriteLine($"{left} * {right} = {calculator.Multiply(left, right)}");
    Console.WriteLine($"{left} ^ 2 = {calculator.Pow(left, 2)}\n");
}
