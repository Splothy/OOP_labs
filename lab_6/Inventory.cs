namespace lab_6;

public class Inventory
{
    private readonly List<Car> cars = new();

    public IReadOnlyList<Car> Cars => cars;

    public void Add(Car car)
    {
        ArgumentNullException.ThrowIfNull(car);
        cars.Add(car);
    }

    public bool Remove(Car car)
    {
        ArgumentNullException.ThrowIfNull(car);
        return cars.Remove(car);
    }

    public Car? Find(string manufacturer, string model)
    {
        return cars.FirstOrDefault(car =>
            car.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase) &&
            car.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
    }
}
