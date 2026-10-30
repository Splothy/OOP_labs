namespace lab_6;

public class CurrentAccount
{
    public decimal Balance { get; private set; }

    public CurrentAccount(decimal initialBalance = 0)
    {
        if (initialBalance < 0)
        {
            throw new ArgumentException("Початковий баланс не може бути від'ємним.", nameof(initialBalance));
        }

        Balance = initialBalance;
    }

    public void Add(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума має бути більшою за нуль.", nameof(amount));
        }

        Balance += amount;
    }

    public void Pay(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума має бути більшою за нуль.", nameof(amount));
        }

        if (Balance < amount)
        {
            throw new InvalidOperationException("Недостатньо коштів на рахунку.");
        }

        Balance -= amount;
    }
}
