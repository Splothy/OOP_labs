using System.Text;
using lab_1;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("--Депозитний рахунок--");
var deposit = new DepositAccount("001", "Вася", 1000m, 10m);
deposit.DisplayBalance();

Console.WriteLine("Поповнення на 500 грн:");
deposit.Deposit(500m);
deposit.DisplayBalance();

Console.WriteLine("Зняття 200 грн:");
deposit.Withdraw(200m);
deposit.DisplayBalance();

Console.WriteLine("Нарахування 10%:");
deposit.AddInterest();
deposit.DisplayBalance(); // 1430 грн

try
{
    Console.WriteLine("Спроба зняти 2000 грн:");
    deposit.Withdraw(2000m);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}
deposit.DisplayBalance();


Console.WriteLine("\n--Поточний рахунок--");
var current = new CurrentAccount("002", "Ірина", 500m);
current.SetCreditLimit(1000m);
current.DisplayBalance();

Console.WriteLine("Поповнення на 200 грн:");
current.Deposit(200m);
current.DisplayBalance();

Console.WriteLine("Зняття 1000 грн з використанням кредиту:");
current.Withdraw(1000m);
current.DisplayBalance();

Console.WriteLine("Зняття решти доступних:");
current.Withdraw(700m);
current.DisplayBalance();

try
{
    Console.WriteLine("Спроба зняти ще 1 грн понад ліміт:");
    current.Withdraw(1m);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

try
{
    Console.WriteLine("Спроба зменшити ліміт до 500 грн за наявності боргу 1000 грн:");
    current.SetCreditLimit(500m);
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("Погашення боргу поповненням на 1000 грн:");
current.Deposit(1000m);
current.SetCreditLimit(0m);
current.DisplayBalance();

Console.WriteLine("\nПоліморфізм та перевірка некоректної суми");
BankAccount[] accounts = { deposit, current };
foreach (BankAccount account in accounts)
{
    account.DisplayBalance();
    try
    {
        account.Deposit(-100m);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
