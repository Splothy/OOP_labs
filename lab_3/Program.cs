using System.Text;
using lab_3;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("=== Лабораторна робота 3. Обробка платежів ===");

var processor = new PaymentProcessor();

IPaymentAccount[] accounts =
{
    new BankAccount(),
    new PayoneerAccount(),
    new WiseAccount()
};

foreach (IPaymentAccount account in accounts)
{
    processor.ProcessPayment(account, 500m);
}

Console.WriteLine("\n=== Перевірка некоректної суми ===");
try
{
    processor.ProcessPayment(accounts[0], 0m);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}
