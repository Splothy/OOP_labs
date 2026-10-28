using System.Security.Cryptography;
using System.Text;

namespace lab_5;

public class DigitalWallet : IDigitalWallet
{
    private readonly string login;
    private readonly string passwordHash;
    private readonly List<string> transactionLog = new();
    private decimal balance;
    private ILoginProvider? authProvider;

    public DigitalWallet(string login, string password)
    {
        this.login = login;
        passwordHash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(password)));
    }

    public void SetAuthProvider(ILoginProvider authProvider)
    {
        this.authProvider = authProvider;
    }

    public List<string> GetTransactionLog()
    {
        ValidateAuthentication();
        return new List<string>(transactionLog);
    }

    public decimal CheckBalance()
    {
        ValidateAuthentication();
        return balance;
    }

    public bool Withdraw(decimal amount)
    {
        ValidateAuthentication();

        if (amount <= 0 || balance < amount)
        {
            return false;
        }

        balance -= amount;
        transactionLog.Add($"Зняття: {amount:F2} грн.");
        return true;
    }

    public void Deposit(decimal amount)
    {
        ValidateAuthentication();

        if (amount <= 0)
        {
            return;
        }

        balance += amount;
        transactionLog.Add($"Поповнення: {amount:F2} грн.");
    }

    private void ValidateAuthentication()
    {
        string currentHash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(passwordHash)));

        if (authProvider == null || !authProvider.Validate(login, currentHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
    }
}
