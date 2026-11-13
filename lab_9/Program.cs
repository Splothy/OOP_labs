using lab_9;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("================================================================================");
Console.WriteLine("      Лабораторна робота 9: Бульбашкове сортування через делегат");
Console.WriteLine("================================================================================\n");

IBubbleSorter sorter = new BubbleSorter();


// 1. Сортування за зростанням (іменований метод як делегат SortComparison)
Console.WriteLine("--- 1. Сортування за зростанням (делегат BubbleSorter.Ascending) ---");
int[] numbersAsc = [64, -5, 25, 0, 12, 22, -15, 11, 90, 25];
Console.WriteLine($"Початковий масив:     [{string.Join(", ", numbersAsc)}]");

// Створення екземпляра власного делегата SortComparison
SortComparison ascendingStrategy = BubbleSorter.Ascending;
sorter.Sort(numbersAsc, ascendingStrategy);
Console.WriteLine($"Відсортовано (^):      [{string.Join(", ", numbersAsc)}]\n");


// 2. Сортування за спаданням (іменований метод як делегат SortComparison)
Console.WriteLine("--- 2. Сортування за спаданням (делегат BubbleSorter.Descending) ---");
int[] numbersDesc = [64, -5, 25, 0, 12, 22, -15, 11, 90, 25];
Console.WriteLine($"Початковий масив:     [{string.Join(", ", numbersDesc)}]");

SortComparison descendingStrategy = BubbleSorter.Descending;
sorter.Sort(numbersDesc, descendingStrategy);
Console.WriteLine($"Відсортовано (v):      [{string.Join(", ", numbersDesc)}]\n");


// 3. Використання лямбда-виразів (анонімних функцій)
Console.WriteLine("--- 3. Сортування за допомогою лямбда-виразів ---");
int[] numbersLambda1 = [42, 17, -8, 99, 0, 3];
Console.WriteLine($"Початковий масив:     [{string.Join(", ", numbersLambda1)}]");

sorter.Sort(numbersLambda1, (left, right) => left > right);
Console.WriteLine($"Лямбда (зростання):    [{string.Join(", ", numbersLambda1)}]");

int[] numbersLambda2 = [42, 17, -8, 99, 0, 3];
sorter.Sort(numbersLambda2, (left, right) => left < right);
Console.WriteLine($"Лямбда (спадання):     [{string.Join(", ", numbersLambda2)}]\n");


// 4.нестандартні критерії сортування
Console.WriteLine("--- 4. Кастомні стратегії сортування через делегат ---");
int[] mixedNumbers = [-18, 5, -2, 14, 0, -30, 7, 22, -9];
Console.WriteLine($"Початковий масив:     [{string.Join(", ", mixedNumbers)}]");

var sortedByAbs = sorter.SortCopy(mixedNumbers, BubbleSorter.ByAbsoluteValueAscending);
Console.WriteLine($"За модулем |x|:       [{string.Join(", ", sortedByAbs)}]");

var sortedEvenFirst = sorter.SortCopy(mixedNumbers, BubbleSorter.EvenFirst);
Console.WriteLine($"Парні спочатку:       [{string.Join(", ", sortedEvenFirst)}]\n");


// 5. Безпечне сортування без зміни оригіналу (SortCopy)
Console.WriteLine("--- 5. Сортування копії масиву (SortCopy) ---");
int[] original = [9, 1, 8, 2, 7, 3];
int[] sortedCopy = sorter.SortCopy(original, BubbleSorter.Ascending);
Console.WriteLine($"Оригінальний масив:   [{string.Join(", ", original)}] (не змінився)");
Console.WriteLine($"Новий відсортований:  [{string.Join(", ", sortedCopy)}]\n");


// 6. Дослідження ефективності та оптимізації (ранній вихід)
Console.WriteLine("--- 6. Статистика роботи алгоритму (оптимізація swapped) ---");
var concreteSorter = new BubbleSorter();

int[] alreadySorted = [1, 2, 3, 4, 5, 6, 7, 8];
var statsAlreadySorted = concreteSorter.SortWithStatistics(alreadySorted, BubbleSorter.Ascending);
Console.WriteLine($"Вже відсортований:    порівнянь = {statsAlreadySorted.Comparisons}, перестановок = {statsAlreadySorted.Swaps} (O(n))");

int[] reverseSorted = [8, 7, 6, 5, 4, 3, 2, 1];
var statsReverse = concreteSorter.SortWithStatistics(reverseSorted, BubbleSorter.Ascending);
Console.WriteLine($"У зворотному порядку: порівнянь = {statsReverse.Comparisons}, перестановок = {statsReverse.Swaps}\n");


// 7. Граничні випадки (Edge cases)
Console.WriteLine("--- 7. Перевірка граничних випадків ---");

int[] emptyArray = [];
sorter.Sort(emptyArray, BubbleSorter.Ascending);
Console.WriteLine($"Порожній масив:       [{string.Join(", ", emptyArray)}]");

int[] singleElement = [42];
sorter.Sort(singleElement, BubbleSorter.Ascending);
Console.WriteLine($"Один елемент:         [{string.Join(", ", singleElement)}]");

int[] identicalElements = [7, 7, 7, 7];
sorter.Sort(identicalElements, BubbleSorter.Ascending);
Console.WriteLine($"Однакові елементи:    [{string.Join(", ", identicalElements)}]\n");


// 8. Перевірка валідації та винятків
Console.WriteLine("--- 8. Обробка помилок (валідація null-аргументів) ---");

try
{
    sorter.Sort(null!, BubbleSorter.Ascending);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Перехоплено виняток для null-масиву: {ex.ParamName}");
}

try
{
    sorter.Sort([1, 2, 3], (SortComparison)null!);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine($"Перехоплено виняток для null-делегата: {ex.ParamName}\n");
}


// 9. Узагальнене (generic) сортування довільних типів
Console.WriteLine("--- 9. Узагальнене сортування (Generic BubbleSort) ---");
string[] words = ["банан", "яблуко", "апельсин", "груша", "вишня"];
Console.WriteLine($"Масив рядків:         [{string.Join(", ", words)}]");

concreteSorter.Sort(words, (a, b) => string.Compare(a, b, StringComparison.Ordinal) > 0);
Console.WriteLine($"Відсортовані рядки:   [{string.Join(", ", words)}]");

Console.WriteLine("\nВсі тести та демонстрації успішно виконано!");
