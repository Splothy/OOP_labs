namespace lab_5;

public class Privat24AuthProvider : ILoginProvider
{
    private readonly string phone;
    private readonly string password;

    public Privat24AuthProvider(string phone, string password)
    {
        this.phone = phone;
        this.password = password;
    }

    public bool Validate(string login, string password)
    {
        return phone == login && this.password == password;
    }
}
