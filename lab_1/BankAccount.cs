namespace lab_1;

public abstract class BankAccount
{
    public string AccountNumber { get; }
    public string Owner { get; }
    public decimal Balance { get; protected set; }

    protected BankAccount(string accountNumber, string owner, decimal balance)
    {
        if (string.IsNullOrWhiteSpace(accountNumber))
        {
            throw new ArgumentException("Номер рахунку не може бути порожнім.", nameof(accountNumber));
        }

        if (string.IsNullOrWhiteSpace(owner))
        {
            throw new ArgumentException("Ім'я власника не може бути порожнім.", nameof(owner));
        }

        if (balance < 0)
        {
            throw new ArgumentException("Початковий баланс не може бути від'ємним.", nameof(balance));
        }

        AccountNumber = accountNumber;
        Owner = owner;
        Balance = balance;
    }
    public abstract void DisplayBalance();
    public abstract void Deposit(decimal amount);
    public abstract void Withdraw(decimal amount);
}
