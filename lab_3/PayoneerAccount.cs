namespace lab_3;

public class PayoneerAccount : IPaymentAccount
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Платіж через Payoneer: {amount:F2} грн.");
    }
}
