namespace lab_6;

public interface ICarDealer
{
    string Name { get; }
    Car? FindCar(string manufacturer, string model);
    void ValidateExchange(Car carToGive, decimal cashChange);
    void CompleteExchange(Car carToReceive, Car carToGive, decimal cashChange);
}
