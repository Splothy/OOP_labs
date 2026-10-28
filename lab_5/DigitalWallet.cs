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
    private bool isAuthenticated;

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

    public void Authenticate(string providerLogin, string providerPassword, string walletPassword)
    {
        string enteredHash = Convert.ToBase64String(
            SHA256.HashData(Encoding.UTF8.GetBytes(walletPassword)));

        if (authProvider == null || enteredHash != passwordHash ||
            !authProvider.Validate(providerLogin, providerPassword))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }

        isAuthenticated = true;
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
        if (!isAuthenticated)
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
    }
}
