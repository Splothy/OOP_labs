namespace lab10;

public class StringCalculator
{
    public int Add(string numbers)
    {
        if(numbers == "")
            return 0;
        string[] delimiters = [",", "\n"];

        if (numbers.StartsWith("//"))
        {
            var end = numbers.IndexOf('\n');
            var header = numbers[2..end];

            if (header.StartsWith('['))
            {
                var content = header[1..^1];
                delimiters = content.Split("][");
            }
            else
            {
                delimiters = [header];
            }
            numbers = numbers[(end + 1)..];
        }
        var parts = numbers.Split(delimiters, StringSplitOptions.None);
        var values = parts.Select(int.Parse).ToArray();
        var negatives = values.Where(n => n < 0).ToArray();
                if (negatives.Length > 0)
                {
                    var list = string.Join(",", negatives);
                    throw new ArgumentException($"negative not allowed: {list}");
                    
                }
        return values.Where(n => n <= 1000).Sum();
    }
}