using lab12.Models;
namespace lab12.Interfaces;

public interface IBookObserver
{
    void Update(Book book, string change);
}
