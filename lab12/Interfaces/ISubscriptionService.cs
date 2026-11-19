using lab12.Models;
namespace lab12.Interfaces;

public interface ISubscriptionService
{
    bool Subscribe(int userId, string category);
    bool Unsubscribe(int userId, string category);
    IReadOnlyList<User> GetSubscribers(string category);
}
