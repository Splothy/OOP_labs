using lab12.Interfaces;
using lab12.Models;
namespace lab12.Services;


public class BookCatalog : IBookCatalog
{
    private readonly IBookObserver[] observers;
    private readonly List<Book> books = new();
    private int nextId = 1;

    public BookCatalog(IEnumerable<IBookObserver> observers)
    {
        this.observers = observers.ToArray();
    }

    public IReadOnlyList<Book> GetAll() => books.ToArray();

    public Book Add(string title, string author, string category)
    {
        Validate(title, author, category);
        var book = new Book(nextId++, title.Trim(), author.Trim(), category.Trim());
        books.Add(book);
        Notify(book, "Додано книгу");
        return book;
    }

    public void Edit(int id, string title, string author, string category)
    {
        var book = Get(id);
        Validate(title, author, category);
        var updated = new Book(id, title.Trim(), author.Trim(), category.Trim());
        books[books.IndexOf(book)] = updated;
        Notify(updated, "Відредаговано книгу");
    }

    public void Delete(int id)
    {
        var book = Get(id);
        books.Remove(book);
        Notify(book, "Видалено книгу");
    }

    private Book Get(int id) => books.FirstOrDefault(book => book.Id == id)
        ?? throw new ArgumentException("Книгу не знайдено.");

    private void Notify(Book book, string change)
    {
        foreach (var observer in observers)
            observer.Update(book, change);
    }

    private static void Validate(string title, string author, string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentException.ThrowIfNullOrWhiteSpace(author);
        ArgumentException.ThrowIfNullOrWhiteSpace(category);
    }
}
