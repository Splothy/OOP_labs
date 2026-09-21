namespace lab_3;

public class BankAccount : IPaymentAccount
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Платіж через банківський рахунок: {amount:F2} грн.");
    }
}
