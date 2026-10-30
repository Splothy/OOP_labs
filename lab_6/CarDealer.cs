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

    public void AddCar(Car car)
    {
        inventory.Add(car);
    }

    public void SellToCustomer(string manufacturer, string model)
    {
        Car? car = inventory.Find(manufacturer, model);
        if (car == null)
        {
            throw new InvalidOperationException("Автомобіль не знайдено.");
        }

        decimal salePrice = car.Price * (1 + Markup / 100);
        if (account.Balance < salePrice)
        {
            throw new InvalidOperationException("На рахунку недостатньо коштів для продажу.");
        }

        inventory.Remove(car);
        account.Add(salePrice);
    }

    public void Exchange(ICarDealer otherDealer, Car carToGive, string manufacturer, string model)
    {
        if (otherDealer == null)
        {
            throw new ArgumentNullException(nameof(otherDealer));
        }

        if (carToGive == null || carToGive.Price <= 0 || string.IsNullOrWhiteSpace(carToGive.Manufacturer) ||
            string.IsNullOrWhiteSpace(carToGive.Model) || carToGive.Year <= 0)
        {
            throw new ArgumentException("Дані автомобіля для обміну некоректні.", nameof(carToGive));
        }

        Car? carToReceive = otherDealer.FindCar(manufacturer, model);
        if (carToReceive == null)
        {
            throw new InvalidOperationException("Автомобіль іншого автосалону не знайдено.");
        }

        decimal cashChange = carToGive.Price - carToReceive.Price;
        ValidateExchange(carToGive, cashChange);
        otherDealer.ValidateExchange(carToReceive, -cashChange);

        if (!inventory.Remove(carToGive))
        {
            throw new InvalidOperationException("Автомобіль цього автосалону більше недоступний для обміну.");
        }

        inventory.Add(carToReceive);
        if (cashChange > 0)
        {
            account.Add(cashChange);
        }
        else if (cashChange < 0)
        {
            account.Pay(-cashChange);
        }

        otherDealer.CompleteExchange(carToGive, carToReceive, -cashChange);
    }

    public void ValidateExchange(Car carToGive, decimal cashChange)
    {
        if (inventory.Find(carToGive.Manufacturer, carToGive.Model) == null)
        {
            throw new InvalidOperationException("Автомобіль для обміну відсутній у цьому автосалоні.");
        }

        if (cashChange < 0 && account.Balance < -cashChange)
        {
            throw new InvalidOperationException("В іншого автосалону недостатньо коштів для компенсації.");
        }
    }

    public void CompleteExchange(Car carToReceive, Car carToGive, decimal cashChange)
    {
        ValidateExchange(carToGive, cashChange);
        Car? existingCar = inventory.Find(carToGive.Manufacturer, carToGive.Model);
        if (existingCar == null || !inventory.Remove(existingCar))
        {
            throw new InvalidOperationException("Автомобіль більше недоступний для обміну.");
        }

        inventory.Add(carToReceive);
        if (cashChange < 0)
        {
            account.Pay(-cashChange);
        }
        else if (cashChange > 0)
        {
            account.Add(cashChange);
        }
    }
}
