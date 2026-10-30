using System.Globalization;

const string input = "a, 1, 2, f, -1, 0, 4, 10, 4,f, 4f, 8, 9, 3";

var sum = input
    .Split(',')
    .Select(fragment => int.TryParse(
        fragment.Trim(),
        NumberStyles.Integer,
        CultureInfo.InvariantCulture,
        out var number)
        ? (int?)number
        : null)
    .Where(number => number.HasValue)
    .Select(number => number!.Value)
    .OrderBy(number => number)
    .Skip(3)
    .Sum();

Console.WriteLine($"Сума чисел без трьох найменших: {sum}");
