namespace lab_2;

public class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Собака гавкає: гав-гав!");
    }

    public override void Walk()
    {
        Console.WriteLine("Собака біжить");
    }
}
