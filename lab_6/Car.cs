namespace lab_6;

public class Car : Vehicle
{
    public string BodyType { get; }

    public Car(string manufacturer, string model, int year, decimal price, string bodyType = "Седан")
        : base(manufacturer, model, year, price)
    {
        BodyType = string.IsNullOrWhiteSpace(bodyType) ? "Седан" : bodyType;
    }

    public override string ToString()
    {
        return $"{base.ToString()} [Тип кузова: {BodyType}]";
    }
}
