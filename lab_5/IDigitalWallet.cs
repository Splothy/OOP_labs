namespace lab_5;

public interface IDigitalWallet
{
    List<string> GetTransactionLog();
    decimal CheckBalance();
    bool Withdraw(decimal amount);
    void Deposit(decimal amount);
    void SetAuthProvider(ILoginProvider authProvider);
}
