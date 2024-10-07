namespace UserService.Repositories;

public class DbConnectionString : IDbConnectionString
{
    public string Connst { get; set; } = "Host=127.0.0.1;Database=postgres;Username=postgres;Port=5432;Password=";
}