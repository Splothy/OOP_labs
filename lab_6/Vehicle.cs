namespace lab_6;

public abstract class Vehicle
{
    public string Manufacturer { get; }
    public string Model { get; }
    public int Year { get; }
    public decimal Price { get; }

    protected Vehicle(string manufacturer, string model, int year, decimal price)
    {
        if (string.IsNullOrWhiteSpace(manufacturer))
        {
            throw new ArgumentException("Виробник не може бути порожнім.", nameof(manufacturer));
        }

        if (string.IsNullOrWhiteSpace(model))
        {
            throw new ArgumentException("Модель не може бути порожньою.", nameof(model));
        }

        if (year < 1886)
        {
            throw new ArgumentException("Рік випуску не може бути ранішим за 1886.", nameof(year));
        }

        if (price < 0)
        {
            throw new ArgumentException("Ціна не може бути від'ємною.", nameof(price));
        }

        Manufacturer = manufacturer;
        Model = model;
        Year = year;
        Price = price;
    }

    public override string ToString()
    {
        return $"{Manufacturer} {Model}, {Year}, {Price:F2} грн.";
    }
}
