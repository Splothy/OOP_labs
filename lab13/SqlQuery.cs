namespace lab13;

public class SqlQuery
{
    public string CommandText { get; }
    public IReadOnlyDictionary<string, object> CommandParameters { get; }

    public SqlQuery(string commandText, IReadOnlyDictionary<string, object> commandParameters)
    {
        CommandText = commandText;
        CommandParameters = commandParameters;
    }
}
