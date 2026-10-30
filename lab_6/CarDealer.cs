namespace lab_6;

public class CarDealer : ICarDealer
{
    private readonly Inventory inventory;
    private readonly CurrentAccount account;
    private decimal markup;

    public string Name { get; }
    public IReadOnlyList<Car> Cars => inventory.Cars;
    public decimal Balance => account.Balance;

    public decimal Markup
    {
        get => markup;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Націнка не може бути від'ємною.", nameof(value));
            }
            markup = value;
        }
    }

    public CarDealer(string name, decimal markup, decimal initialBalance = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Назва автосалону не може бути порожньою.", nameof(name));
        }

        Name = name;
        Markup = markup;
        inventory = new Inventory();
        account = new CurrentAccount(initialBalance);
    }

    public Car? FindCar(string manufacturer, string model)
    {
        return inventory.Find(manufacturer, model);
    }

    public void AddCar(Car car)
    {
        inventory.Add(car);
    }

    public void BuyCar(Car car)
    {
        ArgumentNullException.ThrowIfNull(car);
        account.Pay(car.Price);
        inventory.Add(car);
    }

    public decimal GetCustomerPrice(Car car)
    {
        ArgumentNullException.ThrowIfNull(car);
        return car.Price * (1 + Markup / 100);
    }

    public decimal SellToCustomer(string manufacturer, string model)
    {
        Car car = inventory.Find(manufacturer, model)
            ?? throw new InvalidOperationException($"Автомобіль {manufacturer} {model} не знайдено в наявності.");

        decimal salePrice = GetCustomerPrice(car);
        inventory.Remove(car);
        account.Add(salePrice);
        return salePrice;
    }

    public void Deposit(decimal amount)
    {
        account.Add(amount);
    }

    public List<(ICarDealer Dealer, Car Car)> FindInOtherDealers(
        IEnumerable<ICarDealer> otherDealers,
        string manufacturer,
        string model)
    {
        ArgumentNullException.ThrowIfNull(otherDealers);

        var results = new List<(ICarDealer Dealer, Car Car)>();
        foreach (ICarDealer dealer in otherDealers)
        {
            if (ReferenceEquals(dealer, this))
            {
                continue;
            }

            Car? car = dealer.FindCar(manufacturer, model);
            if (car != null)
            {
                results.Add((dealer, car));
            }
        }

        return results;
    }

    public void Exchange(ICarDealer otherDealer, Car carToGive, string manufacturer, string model)
    {
        ValidateExchangeParams(otherDealer, carToGive, manufacturer, model);

        Car ownCar = inventory.Find(carToGive.Manufacturer, carToGive.Model)
            ?? throw new InvalidOperationException("Автомобіль для обміну відсутній у цьому автосалоні.");

        Car otherCar = otherDealer.FindCar(manufacturer, model)
            ?? throw new InvalidOperationException("Автомобіль іншого автосалону не знайдено.");

        decimal priceDiff = otherCar.Price - ownCar.Price;
        ValidateExchangeFunds(otherDealer, priceDiff);

        ApplyExchange(ownCar, otherCar, priceDiff);
        otherDealer.ReceiveExchangedCar(ownCar, otherCar, priceDiff);
    }

    public void ReceiveExchangedCar(Car incomingCar, Car outgoingCar, decimal compensation)
    {
        ArgumentNullException.ThrowIfNull(incomingCar);
        ArgumentNullException.ThrowIfNull(outgoingCar);

        Car carToRelease = inventory.Find(outgoingCar.Manufacturer, outgoingCar.Model)
            ?? throw new InvalidOperationException($"Автомобіль {outgoingCar.Manufacturer} {outgoingCar.Model} не знайдено для передачі.");

        inventory.Remove(carToRelease);
        inventory.Add(incomingCar);

        if (compensation > 0)
        {
            account.Add(compensation);
        }
        else if (compensation < 0)
        {
            account.Pay(-compensation);
        }
    }

    private void ValidateExchangeParams(ICarDealer otherDealer, Car carToGive, string manufacturer, string model)
    {
        ArgumentNullException.ThrowIfNull(otherDealer);
        ArgumentNullException.ThrowIfNull(carToGive);

        if (ReferenceEquals(otherDealer, this))
        {
            throw new ArgumentException("Не можна здійснити обмін з самим собою.", nameof(otherDealer));
        }

        if (string.IsNullOrWhiteSpace(manufacturer))
        {
            throw new ArgumentException("Виробник не може бути порожнім.", nameof(manufacturer));
        }

        if (string.IsNullOrWhiteSpace(model))
        {
            throw new ArgumentException("Модель не може бути порожньою.", nameof(model));
        }
    }

    private void ValidateExchangeFunds(ICarDealer otherDealer, decimal priceDiff)
    {
        if (priceDiff > 0 && Balance < priceDiff)
        {
            throw new InvalidOperationException(
                $"Недостатньо коштів для доплати за обмін. Необхідно: {priceDiff:F2} грн, на балансі: {Balance:F2} грн.");
        }

        if (priceDiff < 0 && otherDealer.Balance < -priceDiff)
        {
            throw new InvalidOperationException(
                $"В іншого автосалону недостатньо коштів для доплати ({-priceDiff:F2} грн).");
        }
    }

    private void ApplyExchange(Car outgoingCar, Car incomingCar, decimal priceDiff)
    {
        inventory.Remove(outgoingCar);
        inventory.Add(incomingCar);

        if (priceDiff > 0)
        {
            account.Pay(priceDiff);
        }
        else if (priceDiff < 0)
        {
            account.Add(-priceDiff);
        }
    }
}
