namespace lab_6;

public class CarDealer : ICarDealer
{
    private readonly Inventory inventory;
    private readonly CurrentAccount account;

    public string Name { get; }
    public decimal Markup { get; set; }
    public IReadOnlyList<Car> Cars => inventory.Cars;
    public decimal Balance => account.Balance;

    public CarDealer(string name, decimal markup)
    {
        Name = name;
        Markup = markup;
        inventory = new Inventory();
        account = new CurrentAccount();
    }

    public Car? FindCar(string manufacturer, string model)
    {
        return inventory.Find(manufacturer, model);
    }

    public bool SellCar(Car car, decimal amount)
    {
        Car? availableCar = inventory.Find(car.Manufacturer, car.Model);
        if (availableCar == null || !account.Pay(amount))
        {
            return false;
        }

        inventory.Remove(availableCar);
        return true;
    }

    public bool BuyCar(Car car, decimal amount)
    {
        if (!inventory.Remove(car))
        {
            return false;
        }

        account.Add(amount);
        return true;
    }

    public void AddCar(Car car)
    {
        inventory.Add(car);
    }

    public bool SellToCustomer(string manufacturer, string model)
    {
        Car? car = inventory.Find(manufacturer, model);
        if (car == null)
        {
            return false;
        }

        inventory.Remove(car);
        account.Add(car.Price * (1 + Markup / 100));
        return true;
    }

    public bool Trade(Car customerCar, string manufacturer, string model)
    {
        Car? dealerCar = inventory.Find(manufacturer, model);
        if (dealerCar == null)
        {
            return false;
        }

        decimal compensation = dealerCar.Price - customerCar.Price;
        if (compensation > 0 && !account.Pay(compensation))
        {
            return false;
        }

        inventory.Remove(dealerCar);
        inventory.Add(customerCar);
        if (compensation < 0)
        {
            account.Add(-compensation);
        }

        return true;
    }
}
