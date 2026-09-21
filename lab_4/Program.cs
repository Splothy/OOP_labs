using System.Text;
using lab_4;

Console.OutputEncoding = Encoding.UTF8;
ICoffeeMachine machine = new CoffeeMachine();

while (true)
{
    Console.WriteLine("\n=== Кавова машина ===");
    Console.WriteLine("1 — Переглянути стан");
    Console.WriteLine("2 — Додати воду (мл)");
    Console.WriteLine("3 — Додати зерна (г)");
    Console.WriteLine("4 — Приготувати еспресо (20 г зерен, 50 мл води)");
    Console.WriteLine("5 — Приготувати лате (25 г зерен, 150 мл води)");
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
                Console.WriteLine($"Вода: {machine.Water} мл");
                Console.WriteLine($"Зерна: {machine.Beans} г");
                Console.WriteLine($"Вода нагріта: {(machine.IsWaterHeated ? "так" : "ні")}");
                break;
            case "2":
                Console.Write("Введіть кількість води в мл: ");
                if (int.TryParse(Console.ReadLine(), out int water))
                {
                    machine.AddWater(water);
                    Console.WriteLine("Воду додано.");
                }
                else
                {
                    Console.WriteLine("Потрібно ввести ціле число.");
                }
                break;
            case "3":
                Console.Write("Введіть кількість зерен у г: ");
                if (int.TryParse(Console.ReadLine(), out int beans))
                {
                    machine.AddBeans(beans);
                    Console.WriteLine("Зерна додано.");
                }
                else
                {
                    Console.WriteLine("Потрібно ввести ціле число.");
                }
                break;
            case "4":
                machine.MakeEspresso();
                break;
            case "5":
                machine.MakeLatte();
                break;
            default:
                Console.WriteLine("Невідомий пункт меню.");
                break;
        }
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (InvalidOperationException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (OverflowException)
    {
        Console.WriteLine("Завелика загальна кількість ресурсу.");
    }
}
