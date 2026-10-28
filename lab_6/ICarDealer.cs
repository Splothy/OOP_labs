namespace lab_6;

public interface ICarDealer
{
    string Name { get; }
    Car? FindCar(string manufacturer, string model);
    bool SellCar(Car car, decimal amount);
    bool BuyCar(Car car, decimal amount);
}
