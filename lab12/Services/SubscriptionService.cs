using lab12.Interfaces;
using lab12.Models;
namespace lab12.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly IUserService users;

    public SubscriptionService(IUserService users)
    {
        this.users = users;
    }

    public bool Subscribe(int userId, string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);
        return users.Get(userId).Subscribe(category.Trim());
    }

    public bool Unsubscribe(int userId, string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);
        return users.Get(userId).Unsubscribe(category.Trim());
    }

    public IReadOnlyList<User> GetSubscribers(string category)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(category);
        return users.GetAll().Where(user => user.IsSubscribed(category.Trim())).ToArray();
    }
}
