using System.Data;
using UserService.DomainService.Domain;
using UserService.DomainService.Repositories.Abstractions;
using Dapper;
using Npgsql;

namespace UserService.Repositories;

public class UserRepository: IUserRepository
{
    private readonly IDbConnection _dbConnection;

    public UserRepository(IDbConnectionString connectionString)
    {
        
        this._dbConnection = new NpgsqlConnection(connectionString.Connst);
    }

    public async Task<bool> CreateUserAsync(User user)
    {
        var sql = "CALL create_user(@Login, @Password, @Name, @Surname, @Age)";
        var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Login = user.Login, Password = user.Password, Name = user.Name, Surname = user.Surname, Age = user.Age });
        return rowsAffected > 0;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        var sql = "SELECT * FROM get_user_by_id(@Id)";
        return await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
    }

    public async Task<User?> GetUserByLoginAsync(string login)
    {
        var sql = "SELECT * FROM get_user_by_login(@Login)";
        return await _dbConnection.QuerySingleOrDefaultAsync<User>(sql, new { Login = login });
    }

    public async Task<IEnumerable<User>> GetUsersByNameAsync(string name, string surname)
    {
        var sql = "SELECT * FROM get_user_by_name_surname(@Name, @Surname)";
        return await _dbConnection.QueryAsync<User>(sql, new { Name = name, Surname = surname });
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        var sql = "CALL update_user_data(@Id, @Password, @Name, @Surname, @Age)";
        var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Id = user.Id, Password = user.Password, Name = user.Name, Surname = user.Surname, Age = user.Age });
        return rowsAffected > 0;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var sql = "CALL delete_user_by_id(@Id)";
        var rowsAffected = await _dbConnection.ExecuteAsync(sql, new { Id = id });
        return rowsAffected > 0;
    }
}