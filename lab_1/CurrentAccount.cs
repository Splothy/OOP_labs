namespace lab_1;

public class CurrentAccount : BankAccount
{
    public decimal CreditLimit { get; private set; }

    public CurrentAccount(string accountNumber, string owner, decimal balance)
        : base(accountNumber, owner, balance)
    {
    }

    public override void DisplayBalance()
    {
        Console.WriteLine($"Поточний рахунок {AccountNumber}, власник: {Owner}, баланс: {Balance:F2} грн.");
        Console.WriteLine($"Кредитний ліміт: {CreditLimit:F2} грн.");
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
        
        if (Balance - amount < -CreditLimit)
        {
            throw new InvalidOperationException("Недостатньо коштів з урахуванням кредитного ліміту.");
        }

        Balance -= amount;
    }

    public void SetCreditLimit(decimal creditLimit)
    {
        if (creditLimit < 0)
        {
            throw new ArgumentException("Кредитний ліміт не може бути від'ємним.", nameof(creditLimit));
        }

        if (Balance < -creditLimit)
        {
            throw new InvalidOperationException("Новий ліміт не може бути меншим за наявний борг.");
        }

        CreditLimit = creditLimit;
    }
}
