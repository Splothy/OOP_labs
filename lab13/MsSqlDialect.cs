namespace lab13;

public class MsSqlDialect : SqlDialect
{
    public override string QuoteIdentifier(string name)
    {
        return "[" + name.Replace("]", "]]") + "]";
    }

    public override string SelectClause(string? limitParameter)
    {
        return limitParameter is null ? "SELECT " : $"SELECT TOP ({limitParameter}) ";
    }

    public override string LimitClause(string? limitParameter)
    {
        return string.Empty;
    }
}
