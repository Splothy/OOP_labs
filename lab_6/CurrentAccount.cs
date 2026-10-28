namespace lab_6;

public class CurrentAccount
{
    public decimal Balance { get; private set; }

    public void Add(decimal amount)
    {
        Balance += amount;
    }

    public bool Pay(decimal amount)
    {
        if (amount < 0 || Balance < amount)
        {
            return false;
        }

        Balance -= amount;
        return true;
    }
}
