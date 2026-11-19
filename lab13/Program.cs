using lab13;

Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.WriteLine("Лабораторна робота 13. SqlBuilder (Builder)\n");

var postgresQuery = SqlBuilder.Select<User>()
    .OrderBy(user => user.RegistrationDate)
    .Take(10)
    .Build();

var msSqlQuery = SqlBuilder.Select<User>(new MsSqlDialect())
    .OrderBy(user => user.RegistrationDate)
    .Take(10)
    .Build();

var descendingQuery = SqlBuilder.Select<User>()
    .OrderByDescending(user => user.RegistrationDate)
    .OrderBy(user => user.Name)
    .Build();

ShowQuery("PostgreSQL", postgresQuery);
ShowQuery("MS SQL", msSqlQuery);
ShowQuery("Сортування за кількома стовпцями без обмеження кількості", descendingQuery);

static void ShowQuery(string title, SqlQuery query)
{
    Console.WriteLine(title);
    Console.WriteLine($"CommandText: {query.CommandText}");
    Console.WriteLine("CommandParameters:");

    foreach (var parameter in query.CommandParameters)
        Console.WriteLine($"  {parameter.Key} = {parameter.Value}");

    Console.WriteLine();
}
