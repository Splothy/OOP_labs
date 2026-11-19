using lab12.Models;
namespace lab12.Interfaces;

public interface IUserService
{
    IReadOnlyList<User> GetAll();
    User Get(int id);
    User Register(string name, string email);
    void Edit(int id, string name, string email);
    void Delete(int id);
}
