namespace lab13;

public class PostgreSqlDialect : SqlDialect
{
    public override string QuoteIdentifier(string name)
    {
        return "\"" + name.Replace("\"", "\"\"") + "\"";
    }

    public override string SelectClause(string? limitParameter)
    {
        return "SELECT ";
    }

    public override string LimitClause(string? limitParameter)
    {
        return limitParameter is null ? string.Empty : $" LIMIT {limitParameter}";
    }
}
