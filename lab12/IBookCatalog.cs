namespace lab12;

public interface IBookCatalog
{
    IReadOnlyList<Book> GetAll();
    Book Add(string title, string author, string category);
    void Edit(int id, string title, string author, string category);
    void Delete(int id);
}
