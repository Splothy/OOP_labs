namespace lab12;

public interface IUserService
{
    IReadOnlyList<User> GetAll();
    User Get(int id);
    User Register(string name, string email);
    void Edit(int id, string name, string email);
    void Delete(int id);
}
