using System.Linq.Expressions;

namespace lab13;

public static class SqlBuilder
{
    public static SqlBuilder<T> Select<T>(SqlDialect? dialect = null)
    {
        return new SqlBuilder<T>(dialect ?? new PostgreSqlDialect());
    }
}

public class SqlBuilder<T>
{
    private readonly SqlDialect dialect;
    private readonly List<string> orderBy = new();
    private int? take;

    internal SqlBuilder(SqlDialect dialect)
    {
        this.dialect = dialect;
    }

    public SqlBuilder<T> OrderBy<TKey>(Expression<Func<T, TKey>> property)
    {
        orderBy.Add($"{dialect.QuoteIdentifier(GetPropertyName(property))} ASC");
        return this;
    }

    public SqlBuilder<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> property)
    {
        orderBy.Add($"{dialect.QuoteIdentifier(GetPropertyName(property))} DESC");
        return this;
    }

    public SqlBuilder<T> Take(int count)
    {
        take = count;
        return this;
    }

    public SqlQuery Build()
    {
        var parameters = new Dictionary<string, object>();
        string? limitParameter = take.HasValue ? "@p0" : null;

        if (take.HasValue)
            parameters.Add("@p0", take.Value);
        
        string sql = dialect.SelectClause(limitParameter)
            + $"* FROM {dialect.QuoteIdentifier(typeof(T).Name)}";

        if (orderBy.Count > 0)
            sql += " ORDER BY " + string.Join(", ", orderBy);

        sql += dialect.LimitClause(limitParameter);

        return new SqlQuery(sql + ";", parameters);
    }

    private static string GetPropertyName<TKey>(Expression<Func<T, TKey>> property)
    {
        if (property.Body is MemberExpression member && member.Expression == property.Parameters[0])
            return member.Member.Name;

        throw new ArgumentException("Вкажіть властивість моделі, наприклад: user => user.RegistrationDate.", nameof(property));
    }

}
