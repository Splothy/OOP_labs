namespace lab_6;

public interface ICarDealer
{
    string Name { get; }
    decimal Balance { get; }
    IReadOnlyList<Car> Cars { get; }

    Car? FindCar(string manufacturer, string model);
    void Exchange(ICarDealer otherDealer, Car carToGive, string manufacturer, string model);
    void ReceiveExchangedCar(Car incomingCar, Car outgoingCar, decimal compensation);
}
