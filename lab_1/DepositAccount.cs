namespace lab_1;

public class DepositAccount : BankAccount
{
    public decimal InterestRate { get; }

    public DepositAccount(string accountNumber, string owner, decimal balance, decimal interestRate)
        : base(accountNumber, owner, balance)
    {
        if (interestRate < 0)
        {
            throw new ArgumentException("Ставка не може бути від'ємною.", nameof(interestRate));
        }

        InterestRate = interestRate;
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"Депозитний рахунок {AccountNumber}, власник: {Owner}, баланс: {Balance:F2} грн.");
    }

    public override void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума поповнення має бути більшою за нуль.", nameof(amount));
        }

        Balance += amount;
    }

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Сума зняття має бути більшою за нуль.", nameof(amount));
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Недостатньо коштів на депозитному рахунку.");
        }

        Balance -= amount;
    }

    public void AddInterest()
    {
        Balance += Balance * InterestRate / 100;
    }
}
