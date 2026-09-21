namespace lab_2;

public class Cat : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Кіт мявкає: мяу-мяу!");
    }

    public override void Walk()
    {
        Console.WriteLine("Кіт тихо крадеться");
    }
}
