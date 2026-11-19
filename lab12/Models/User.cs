namespace lab12.Models;

public class User
{
    private readonly HashSet<string> categories = new(StringComparer.OrdinalIgnoreCase);

    public int Id { get; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public IReadOnlyCollection<string> Categories => categories.ToArray();

    internal User(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }

    internal void Update(string name, string email)
    {
        Name = name;
        Email = email;
    }

    internal bool Subscribe(string category) => categories.Add(category);
    internal bool Unsubscribe(string category) => categories.Remove(category);
    internal bool IsSubscribed(string category) => categories.Contains(category);
}
