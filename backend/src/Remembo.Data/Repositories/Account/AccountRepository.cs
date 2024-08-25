using Dapper;
using MySqlConnector;
using Remembo.Domain.Account.Entities;
using Remembo.Domain.Account.Interfaces;

namespace Remembo.Data.Repositories.Account;
public class AccountRepository(MySqlConnection connection) : IAccountRepository {
    public async Task<bool> InsertAsync(User entity) {
        var sql = @" 
            INSERT INTO `Remembo`.`Users`
                (`Id`, `Name`, `Email`, `PasswordHash`)
            VALUES
                (@Id, @Name, @Email, @PasswordHash); ";

        var affectedRows = await connection.ExecuteAsync(sql, new { entity.Id, entity.Name, entity.Email, entity.PasswordHash });
        if (affectedRows == 0) return false;

        return true;
    }

    public async Task<User?> GetUserByEmailAsync(string email) {
        var sql = "SELECT `Id`, `Name`, `Email`, `PasswordHash` FROM `Remembo`.`Users` WHERE `Email` = @Email; ";

        return await connection.QueryFirstOrDefaultAsync<User>(sql, new { Email = email });
    }

    public async Task<int> CheckIfExistsUserByEmailAsync(string email) {
        var sql = "SELECT COUNT(*) FROM `Remembo`.`Users` WHERE `Email` = @Email; ";

        return await connection.ExecuteScalarAsync<int>(sql, new { Email = email });
    }
}
