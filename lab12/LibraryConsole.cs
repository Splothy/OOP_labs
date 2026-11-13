namespace lab12;

public class LibraryConsole
{
    private readonly IUserService users;
    private readonly IBookCatalog books;
    private readonly ISubscriptionService subscriptions;

    public LibraryConsole(IUserService users, IBookCatalog books, ISubscriptionService subscriptions)
    {
        this.users = users;
        this.books = books;
        this.subscriptions = subscriptions;
    }

    public void Run()
    {
        Console.WriteLine("Лабораторна робота 12. Бібліотека (SOLID + IoC)");
        while (true)
        {
            Console.WriteLine("\n1 — Користувачі\n2 — Зареєструвати користувача" +
                "\n3 — Редагувати користувача\n4 — Видалити користувача" +
                "\n5 — Каталог за категоріями\n6 — Додати книгу" +
                "\n7 — Редагувати книгу\n8 — Видалити книгу" +
                "\n9 — Підписатися на категорію\n10 — Відписатися від категорії\n0 — Вийти");
            Console.Write("Ваш вибір: ");
            string? choice = Console.ReadLine();
            if (choice is null or "0")
                return;

            try
            {
                switch (choice)
                {
                    case "1":
                        ShowUsers();
                        break;
                    case "2":
                        var user = users.Register(ReadText("Ім'я"), ReadText("Email"));
                        Console.WriteLine($"Користувача зареєстровано. ID: {user.Id}.");
                        break;
                    case "3":
                        users.Edit(ReadId(), ReadText("Нове ім'я"), ReadText("Новий email"));
                        Console.WriteLine("Користувача відредаговано.");
                        break;
                    case "4":
                        users.Delete(ReadId());
                        Console.WriteLine("Користувача та його підписки видалено.");
                        break;
                    case "5":
                        ShowBooks();
                        break;
                    case "6":
                        var book = books.Add(ReadText("Назва"), ReadText("Автор"), ReadText("Категорія"));
                        Console.WriteLine($"Книгу додано. ID: {book.Id}.");
                        break;
                    case "7":
                        books.Edit(ReadId(), ReadText("Нова назва"), ReadText("Новий автор"),
                            ReadText("Категорія"));
                        Console.WriteLine("Книгу відредаговано.");
                        break;
                    case "8":
                        books.Delete(ReadId());
                        Console.WriteLine("Книгу видалено.");
                        break;
                    case "9":
                        Console.WriteLine(subscriptions.Subscribe(ReadId(), ReadText("Категорія"))
                            ? "Підписку додано." : "Така підписка вже існує.");
                        break;
                    case "10":
                        Console.WriteLine(subscriptions.Unsubscribe(ReadId(), ReadText("Категорія"))
                            ? "Підписку скасовано." : "Такої підписки немає.");
                        break;
                    default:
                        Console.WriteLine("Невідомий пункт меню.");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
            catch (EndOfStreamException)
            {
                return;
            }
        }
    }

    private void ShowUsers()
    {
        var all = users.GetAll();
        if (all.Count == 0)
            Console.WriteLine("Користувачів немає.");

        foreach (var user in all)
            Console.WriteLine($"{user.Id}: {user.Name}, {user.Email}. " +
                              $"Підписки: {string.Join(", ", user.Categories)}");
    }

    private void ShowBooks()
    {
        var all = books.GetAll();
        if (all.Count == 0)
            Console.WriteLine("Каталог порожній.");

        foreach (var category in all.GroupBy(book => book.Category, StringComparer.OrdinalIgnoreCase))
        {
            Console.WriteLine($"\nКатегорія: {category.Key}");
            foreach (var book in category)
                Console.WriteLine($"{book.Id}: «{book.Title}», {book.Author}");
        }
    }

    private static string ReadText(string prompt)
    {
        Console.Write($"{prompt}: ");
        string value = Console.ReadLine() ?? throw new EndOfStreamException();
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Значення не може бути порожнім.");
        return value.Trim();
    }

    private static int ReadId()
    {
        if (!int.TryParse(ReadText("ID"), out int id) || id <= 0)
            throw new ArgumentException("ID має бути додатним цілим числом.");
        return id;
    }
}
