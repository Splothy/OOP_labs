namespace lab13;

public abstract class SqlDialect
{
    public abstract string QuoteIdentifier(string name);
    public abstract string SelectClause(string? limitParameter);
    public abstract string LimitClause(string? limitParameter);
}
