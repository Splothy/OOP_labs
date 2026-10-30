using System.Text;
using lab_6;

Console.OutputEncoding = Encoding.UTF8;

var dealer = new CarDealer("Наш автосалон", 10);
dealer.AddCar(new Car("Toyota", "Corolla", 2022, 20000));
dealer.AddCar(new Car("Ford", "Focus", 2021, 16000));
dealer.AddCar(new Car("BMW", "X3", 2023, 40000));
dealer.AddCar(new Car("Honda", "Civic", 2022, 22000));

var otherDealers = new List<ICarDealer>
{
    new CarDealer("Автосалон 2", 10),
    new CarDealer("Автосалон 3", 10)
};
((CarDealer)otherDealers[0]).AddCar(new Car("Mazda", "CX-5", 2023, 28000));
((CarDealer)otherDealers[1]).AddCar(new Car("Audi", "A4", 2022, 32000));

while (true)
{
    Console.WriteLine("\n=== CarDealer ===");
    Console.WriteLine("1 — Меню користувача");
    Console.WriteLine("2 — Меню адміністратора");
    Console.WriteLine("0 — Вийти");
    Console.Write("Ваш вибір: ");

    string? choice = Console.ReadLine();
    if (choice == "0" || choice == null)
    {
        break;
    }

    if (choice == "1")
    {
        UserMenu(dealer, otherDealers);
    }
    else if (choice == "2")
    {
        AdminMenu(dealer);
    }
}

static void UserMenu(CarDealer dealer, List<ICarDealer> otherDealers)
{
    while (true)
    {
        Console.WriteLine("\n1 — Переглянути автомобілі");
        Console.WriteLine("2 — Купити автомобіль");
        Console.WriteLine("3 — Обміняти автомобіль");
        Console.WriteLine("4 — Знайти в інших автосалонах");
        Console.WriteLine("0 — Назад");
        Console.Write("Ваш вибір: ");
        string? choice = Console.ReadLine();
        if (choice == "0" || choice == null) return;

        if (choice == "1")
        {
            ShowCars(dealer.Cars);
        }
        else if (choice == "2")
        {
            Console.Write("Виробник: ");
            string manufacturer = Console.ReadLine() ?? "";
            Console.Write("Модель: ");
            string model = Console.ReadLine() ?? "";
            try
            {
                dealer.SellToCustomer(manufacturer, model);
                Console.WriteLine("Автомобіль придбано.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else if (choice == "3")
        {
            Console.Write("Виробник авто цього автосалону для обміну: ");
            string ownManufacturer = Console.ReadLine() ?? "";
            Console.Write("Модель авто цього автосалону: ");
            string ownModel = Console.ReadLine() ?? "";
            Console.Write("Виробник бажаного авто: ");
            string desiredManufacturer = Console.ReadLine() ?? "";
            Console.Write("Модель бажаного авто: ");
            string desiredModel = Console.ReadLine() ?? "";
            Console.Write("Назва іншого автосалону (1 або 2): ");
            string otherDealerName = Console.ReadLine() ?? "";
            ICarDealer? otherDealer = otherDealers.FirstOrDefault(item => item.Name.EndsWith(otherDealerName));
            try
            {
                if (otherDealer == null) throw new InvalidOperationException("Автосалон не знайдено.");
                Car? carToGive = dealer.FindCar(ownManufacturer, ownModel);
                if (carToGive == null) throw new InvalidOperationException("Автомобіль цього салону не знайдено.");
                dealer.Exchange(otherDealer, carToGive, desiredManufacturer, desiredModel);
                Console.WriteLine("Обмін виконано.");
            }
            catch (Exception ex) when (ex is InvalidOperationException or ArgumentException)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else if (choice == "4")
        {
            Console.Write("Виробник: ");
            string manufacturer = Console.ReadLine() ?? "";
            Console.Write("Модель: ");
            string model = Console.ReadLine() ?? "";
            foreach (ICarDealer otherDealer in otherDealers)
            {
                Car? car = otherDealer.FindCar(manufacturer, model);
                if (car != null) Console.WriteLine($"{otherDealer.Name}: {car}");
            }
        }
    }
}

static void AdminMenu(CarDealer dealer)
{
    while (true)
    {
        Console.WriteLine("\n1 — Переглянути автомобілі");
        Console.WriteLine("2 — Додати автомобіль");
        Console.WriteLine("3 — Змінити націнку (%)");
        Console.WriteLine("4 — Баланс автосалону");
        Console.WriteLine("0 — Назад");
        Console.Write("Ваш вибір: ");
        string? choice = Console.ReadLine();
        if (choice == "0" || choice == null) return;

        if (choice == "1")
        {
            ShowCars(dealer.Cars);
        }
        else if (choice == "2")
        {
            Console.Write("Виробник: ");
            string manufacturer = Console.ReadLine() ?? "";
            Console.Write("Модель: ");
            string model = Console.ReadLine() ?? "";
            Console.Write("Рік: ");
            int.TryParse(Console.ReadLine(), out int year);
            Console.Write("Ціна: ");
            decimal.TryParse(Console.ReadLine(), out decimal price);
            dealer.AddCar(new Car(manufacturer, model, year, price));
        }
        else if (choice == "3")
        {
            Console.Write("Нова націнка (%): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal markup)) dealer.Markup = markup;
        }
        else if (choice == "4")
        {
            Console.WriteLine($"Баланс: {dealer.Balance:F2} грн.");
        }
    }
}

static void ShowCars(IReadOnlyList<Car> cars)
{
    foreach (Car car in cars) Console.WriteLine(car);
}
