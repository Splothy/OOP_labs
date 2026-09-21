namespace lab_3;

public class PaymentProcessor
{
    public void ProcessPayment(IPaymentAccount account, decimal amount)
    {
        if (account == null)
        {
            throw new ArgumentNullException(nameof(account), "Рахунок не вказано.");
        }

        if (amount <= 0)
        {
            throw new ArgumentException("Сума платежу має бути більшою за нуль.", nameof(amount));
        }
        
        account.Pay(amount);
    }
}
