using System.Text;
using lab_6;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;

var dealer = new CarDealer("Наш автосалон", 10, 150000);
dealer.AddCar(new Car("Toyota", "Corolla", 2022, 20000, "Седан"));
dealer.AddCar(new Car("Ford", "Focus", 2021, 16000, "Хетчбек"));
dealer.AddCar(new Car("BMW", "X3", 2023, 40000, "Кросовер"));
dealer.AddCar(new Car("Honda", "Civic", 2022, 22000, "Седан"));

var dealer2 = new CarDealer("Автосалон 2", 10, 100000);
dealer2.AddCar(new Car("Mazda", "CX-5", 2023, 28000, "Кросовер"));
dealer2.AddCar(new Car("Volkswagen", "Golf", 2020, 15000, "Хетчбек"));

var dealer3 = new CarDealer("Автосалон 3", 10, 80000);
dealer3.AddCar(new Car("Audi", "A4", 2022, 32000, "Седан"));
dealer3.AddCar(new Car("Skoda", "Octavia", 2021, 18000, "Універсал"));

var allDealers = new List<ICarDealer> { dealer, dealer2, dealer3 };
var otherDealers = new List<ICarDealer> { dealer2, dealer3 };

while (true)
{
    Console.WriteLine("\n=== CarDealer ===");
    Console.WriteLine("1 — Меню користувача");
    Console.WriteLine("2 — Меню адміністратора");
    Console.WriteLine("0 — Вийти");
    Console.Write("Ваш вибір: ");

    string? choice = Console.ReadLine();
    if (choice == "0" || choice == null) break;
    if (choice == "1") UserMenu(dealer, otherDealers);
    else if (choice == "2") AdminMenu(dealer, otherDealers);
}

static void UserMenu(CarDealer dealer, List<ICarDealer> otherDealers)
{
    while (true)
    {
        Console.WriteLine("\n--- Меню користувача ---");
        Console.WriteLine("1 — Переглянути автомобілі в наявності");
        Console.WriteLine("2 — Купити автомобіль");
        Console.WriteLine("3 — Знайти авто в інших автосалонах");
        Console.WriteLine("4 — Обміняти автомобіль між салонами");
        Console.WriteLine("0 — Назад");
        Console.Write("Ваш вибір: ");

        string? choice = Console.ReadLine();
        if (choice == "0" || choice == null) return;
        if (choice == "1") ShowUserCars(dealer);
        else if (choice == "2") UserBuyCar(dealer);
        else if (choice == "3") UserSearchOtherDealers(dealer, otherDealers);
        else if (choice == "4") UserExchangeCars(dealer, otherDealers);
    }
}

static void ShowUserCars(CarDealer dealer)
{
    Console.WriteLine($"\nАвтомобілі в '{dealer.Name}' (націнка {dealer.Markup}% включена):");
    if (dealer.Cars.Count == 0)
    {
        Console.WriteLine("Немає автомобілів у наявності.");
        return;
    }

    foreach (Car car in dealer.Cars)
    {
        decimal customerPrice = dealer.GetCustomerPrice(car);
        Console.WriteLine($"- {car.Manufacturer} {car.Model} ({car.Year}), {car.BodyType} — {customerPrice:F2} грн.");
    }
}

static void UserBuyCar(CarDealer dealer)
{
    string manufacturer = PromptString("Виробник: ");
    string model = PromptString("Модель: ");

    try
    {
        decimal salePrice = dealer.SellToCustomer(manufacturer, model);
        Console.WriteLine($"Успішно! Ви придбали {manufacturer} {model} за {salePrice:F2} грн.");
    }
    catch (Exception ex) when (ex is InvalidOperationException or ArgumentException)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}

static void UserSearchOtherDealers(CarDealer dealer, List<ICarDealer> otherDealers)
{
    string manufacturer = PromptString("Виробник для пошуку: ");
    string model = PromptString("Модель для пошуку: ");

    var foundCars = dealer.FindInOtherDealers(otherDealers, manufacturer, model);
    if (foundCars.Count == 0)
    {
        Console.WriteLine("Автомобіль не знайдено в інших автосалонах.");
        return;
    }

    Console.WriteLine("Знайдено в інших салонах:");
    foreach (var (otherDealer, car) in foundCars)
    {
        Console.WriteLine($"- Салон '{otherDealer.Name}': {car}");
    }
}

static void UserExchangeCars(CarDealer dealer, List<ICarDealer> otherDealers)
{
    string ownManufacturer = PromptString("Виробник авто нашого салону для обміну: ");
    string ownModel = PromptString("Модель авто нашого салону: ");
    string desiredManufacturer = PromptString("Виробник бажаного авто: ");
    string desiredModel = PromptString("Модель бажаного авто: ");
    string targetDealerName = PromptString("Назва або номер іншого салону: ");

    ICarDealer? targetDealer = otherDealers.FirstOrDefault(d =>
        d.Name.Contains(targetDealerName, StringComparison.OrdinalIgnoreCase));

    if (targetDealer == null)
    {
        Console.WriteLine("Вказаний автосалон не знайдено.");
        return;
    }

    try
    {
        Car? ownCar = dealer.FindCar(ownManufacturer, ownModel);
        if (ownCar == null)
        {
            Console.WriteLine("Автомобіль нашого салону не знайдено.");
            return;
        }

        dealer.Exchange(targetDealer, ownCar, desiredManufacturer, desiredModel);
        Console.WriteLine($"Обмін між '{dealer.Name}' та '{targetDealer.Name}' успішно виконано!");
    }
    catch (Exception ex) when (ex is InvalidOperationException or ArgumentException)
    {
        Console.WriteLine($"Помилка обміну: {ex.Message}");
    }
}

static void AdminMenu(CarDealer dealer, List<ICarDealer> otherDealers)
{
    while (true)
    {
        Console.WriteLine("\n--- Меню адміністратора ---");
        Console.WriteLine("1 — Переглянути інвентар");
        Console.WriteLine("2 — Купити автомобіль (списання з рахунку салону)");
        Console.WriteLine("3 — Додати автомобіль (надходження)");
        Console.WriteLine("4 — Змінити націнку (%)");
        Console.WriteLine("5 — Поповнити рахунок автосалону");
        Console.WriteLine("6 — Переглянути баланс");
        Console.WriteLine("0 — Назад");
        Console.Write("Ваш вибір: ");

        string? choice = Console.ReadLine();
        if (choice == "0" || choice == null) return;
        if (choice == "1") AdminShowInventory(dealer);
        else if (choice == "2") AdminBuyCar(dealer);
        else if (choice == "3") AdminAddCar(dealer);
        else if (choice == "4") AdminChangeMarkup(dealer);
        else if (choice == "5") AdminDeposit(dealer);
        else if (choice == "6") Console.WriteLine($"Поточний баланс: {dealer.Balance:F2} грн.");
    }
}

static void AdminShowInventory(CarDealer dealer)
{
    Console.WriteLine($"\nІнвентар '{dealer.Name}' (базова вартість):");
    if (dealer.Cars.Count == 0)
    {
        Console.WriteLine("Інвентар порожній.");
        return;
    }

    foreach (Car car in dealer.Cars)
    {
        Console.WriteLine($"- {car}");
    }
}

static void AdminBuyCar(CarDealer dealer)
{
    Car? car = PromptNewCar();
    if (car == null) return;

    try
    {
        dealer.BuyCar(car);
        Console.WriteLine($"Автомобіль придбано в інвентар за {car.Price:F2} грн. Залишок: {dealer.Balance:F2} грн.");
    }
    catch (Exception ex) when (ex is InvalidOperationException or ArgumentException)
    {
        Console.WriteLine($"Помилка: {ex.Message}");
    }
}

static void AdminAddCar(CarDealer dealer)
{
    Car? car = PromptNewCar();
    if (car == null) return;

    dealer.AddCar(car);
    Console.WriteLine($"Автомобіль {car.Manufacturer} {car.Model} успішно додано до інвентарю.");
}

static void AdminChangeMarkup(CarDealer dealer)
{
    Console.Write($"Поточна націнка: {dealer.Markup}%. Нова націнка (%): ");
    if (decimal.TryParse(Console.ReadLine(), out decimal markup) && markup >= 0)
    {
        dealer.Markup = markup;
        Console.WriteLine($"Націнку змінено на {markup}%.");
    }
    else
    {
        Console.WriteLine("Некоректне значення націнки.");
    }
}

static void AdminDeposit(CarDealer dealer)
{
    Console.Write("Сума поповнення: ");
    if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
    {
        dealer.Deposit(amount);
        Console.WriteLine($"Рахунок поповнено на {amount:F2} грн. Баланс: {dealer.Balance:F2} грн.");
    }
    else
    {
        Console.WriteLine("Некоректна сума.");
    }
}

static Car? PromptNewCar()
{
    string manufacturer = PromptString("Виробник: ");
    string model = PromptString("Модель: ");
    string bodyType = PromptString("Тип кузова (Седан/Кросовер/Хетчбек тощо): ");
    Console.Write("Рік випуску: ");
    if (!int.TryParse(Console.ReadLine(), out int year) || year < 1886)
    {
        Console.WriteLine("Некоректний рік випуску.");
        return null;
    }

    Console.Write("Ціна: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
    {
        Console.WriteLine("Некоректна ціна.");
        return null;
    }

    return new Car(manufacturer, model, year, price, bodyType);
}

static string PromptString(string message)
{
    Console.Write(message);
    return Console.ReadLine()?.Trim() ?? "";
}
