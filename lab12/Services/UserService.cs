using lab12.Interfaces;
using lab12.Models;
namespace lab12.Services;

public class UserService : IUserService
{
    private readonly IEmailValidator emailValidator;
    private readonly List<User> users = new();
    private int nextId = 1;

    public UserService(IEmailValidator emailValidator)
    {
        this.emailValidator = emailValidator;
    }

    public IReadOnlyList<User> GetAll() => users.ToArray();

    public User Get(int id) => users.FirstOrDefault(user => user.Id == id)
        ?? throw new ArgumentException("Користувача не знайдено.");

    public User Register(string name, string email)
    {
        Validate(name, email);
        var user = new User(nextId++, name.Trim(), email.Trim());
        users.Add(user);
        return user;
    }

    public void Edit(int id, string name, string email)
    {
        var user = Get(id);
        Validate(name, email, id);
        user.Update(name.Trim(), email.Trim());
    }

    public void Delete(int id) => users.Remove(Get(id));

    private void Validate(string name, string email, int? exceptId = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        if (!emailValidator.IsValid(email.Trim()))
            throw new ArgumentException("Некоректна email-адреса.");

        if (users.Any(user => user.Id != exceptId &&
                              user.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase)))
            throw new ArgumentException("Користувач із такою email-адресою вже існує.");
    }
}
