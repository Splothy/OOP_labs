using lab_7;

var numbers = new GenericLinkedList<int>();
numbers.AddLast(2);
numbers.AddLast(3);
numbers.AddFirst(1);
numbers.AddLast(3);

Console.WriteLine("Список:");
foreach (var number in numbers)
{
    Console.WriteLine(number);
}

Console.WriteLine($"Містить 2: {numbers.Contains(2)}");
Console.WriteLine($"Перше входження 3: {numbers.Find(3)?.Value}");
Console.WriteLine($"Останнє входження 3: {numbers.FindLast(3)?.Value}");

numbers.Remove(2);
numbers.RemoveFirst();
numbers.RemoveLast();

Console.WriteLine("Після видалення:");
foreach (var number in numbers)
{
    Console.WriteLine(number);
}

numbers.Clear();
Console.WriteLine($"Кількість після Clear: {numbers.Count}");
