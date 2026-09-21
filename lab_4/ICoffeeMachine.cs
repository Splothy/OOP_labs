namespace lab_4;

public interface ICoffeeMachine
{
    int Water { get; }
    int Beans { get; }
    bool IsWaterHeated { get; }

    void AddWater(int amount);
    void AddBeans(int amount);
    void MakeEspresso();
    void MakeLatte();
}
