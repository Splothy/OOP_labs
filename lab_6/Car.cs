namespace lab_6;

public class Car
{
    public string Manufacturer { get; }
    public string Model { get; }
    public int Year { get; }
    public decimal Price { get; }

    public Car(string manufacturer, string model, int year, decimal price)
    {
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
