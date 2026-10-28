namespace lab_5;

public class GmailAuthProvider : ILoginProvider
{
    private readonly string gmail;
    private readonly string password;

    public GmailAuthProvider(string gmail, string password)
    {
        this.gmail = gmail;
        this.password = password;
    }

    public bool Validate(string login, string password)
    {
        return gmail == login && this.password == password;
    }
}
