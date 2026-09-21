namespace lab_3;

public class WiseAccount : IPaymentAccount
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Платіж через Wise: {amount:F2} грн.");
    }
}
