namespace lab_4;

public class CoffeeMachine : ICoffeeMachine
{
    private int water;
    private int beans;
    private bool isWaterHeated;

    public int Water => water;
    public int Beans => beans;
    public bool IsWaterHeated => isWaterHeated;

    public void AddWater(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Кількість води має бути більшою за нуль.", nameof(amount));
        }

        water = checked(water + amount);
        isWaterHeated = false;
    }

    public void AddBeans(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Кількість зерен має бути більшою за нуль.", nameof(amount));
        }

        beans = checked(beans + amount);
    }

    private void HeatWater()
    {
        if (water <= 0)
        {
            throw new InvalidOperationException("Немає води для нагрівання.");
        }

        if (!isWaterHeated)
        {
            isWaterHeated = true;
            Console.WriteLine("Воду нагріто.");
        }
    }

    private void GrindBeans(int amount)
    {
        if (beans < amount)
        {
            throw new InvalidOperationException("Недостатньо кавових зерен.");
        }

        beans -= amount;
        Console.WriteLine($"Змелено {amount} г зерен.");
    }

    public void MakeEspresso()
    {
        MakeCoffee(20, 50, "Еспресо");
    }

    public void MakeLatte()
    {
        MakeCoffee(25, 150, "Лате");
    }

    private void MakeCoffee(int requiredBeans, int requiredWater, string name)
    {
        if (water < requiredWater)
        {
            throw new InvalidOperationException("Недостатньо води.");
        }

        if (beans < requiredBeans)
        {
            throw new InvalidOperationException("Недостатньо кавових зерен.");
        }

        HeatWater();
        GrindBeans(requiredBeans);
        water -= requiredWater;

        if (water == 0)
        {
            isWaterHeated = false;
        }

        Console.WriteLine($"{name} готове!");
    }
}
