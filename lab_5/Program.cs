using System.Text;
using lab_5;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("=== Лабораторна робота 5. DigitalWallet ===");

Console.Write("Введіть логін (gmail або телефон): ");
string login = Console.ReadLine() ?? "";
Console.Write("Введіть пароль гаманця: ");
string password = Console.ReadLine() ?? "";

var digitalWallet = new DigitalWallet(login, password);
IDigitalWallet wallet = digitalWallet;

Console.WriteLine("Оберіть провайдера автентифікації:");
Console.WriteLine("1 — Gmail");
Console.WriteLine("2 — Privat24");
Console.Write("Ваш вибір: ");

ILoginProvider authProvider;
if (Console.ReadLine() == "1")
{
    Console.Write("Gmail: ");
    string gmail = Console.ReadLine() ?? "";
    Console.Write("Пароль Gmail: ");
    string gmailPassword = Console.ReadLine() ?? "";
    authProvider = new GmailAuthProvider(gmail, gmailPassword);
    login = gmail;
    password = gmailPassword;
}
else
{
    Console.Write("Телефон: ");
    string phone = Console.ReadLine() ?? "";
    Console.Write("Пароль банкінгу: ");
    string bankingPassword = Console.ReadLine() ?? "";
    authProvider = new Privat24AuthProvider(phone, bankingPassword);
    login = phone;
    password = bankingPassword;
}

wallet.SetAuthProvider(authProvider);
Console.Write("Пароль гаманця: ");
string walletPassword = Console.ReadLine() ?? "";

try
{
    digitalWallet.Authenticate(login, password, walletPassword);
    Console.WriteLine("Автентифікацію пройдено.");
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine(ex.Message);
    return;
}

while (true)
{
    Console.WriteLine("\n=== DigitalWallet ===");
    Console.WriteLine("1 — Перевірити баланс");
    Console.WriteLine("2 — Поповнити гаманець");
    Console.WriteLine("3 — Зняти кошти");
    Console.WriteLine("4 — Журнал транзакцій");
    Console.WriteLine("0 — Вийти");
    Console.Write("Ваш вибір: ");

    string? choice = Console.ReadLine();
    if (choice == "0" || choice == null)
    {
        break;
    }

    try
    {
        switch (choice)
        {
            case "1":
                Console.WriteLine($"Баланс: {wallet.CheckBalance():F2} грн.");
                break;
            case "2":
                Console.Write("Сума поповнення: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal depositAmount))
                {
                    wallet.Deposit(depositAmount);
                }
                break;
            case "3":
                Console.Write("Сума зняття: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                {
                    Console.WriteLine(wallet.Withdraw(withdrawAmount)
                        ? "Кошти знято."
                        : "Недостатньо коштів або некоректна сума.");
                }
                break;
            case "4":
                foreach (string transaction in wallet.GetTransactionLog())
                {
                    Console.WriteLine(transaction);
                }
                break;
            default:
                Console.WriteLine("Невідомий пункт меню.");
                break;
        }
    }
    catch (UnauthorizedAccessException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
