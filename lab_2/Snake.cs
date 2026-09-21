namespace lab_2;

public class Snake : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Змія шипить: ш-ш-ш!");
    }

    public override void Walk()
    {
        Console.WriteLine("Змія повзає по землі.");
    }
}
