using Dapper;
using MySqlConnector;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;

namespace Remembo.Data.Repositories.Remembo;
public class MatterRepository(MySqlConnection connection) : IMatterRepository {
    public async Task<bool> InsertAsync(Matter entity) {
        var sql = @"INSERT INTO `Remembo`.`Matters`
                        (`Id`, `Name`, `UserId`)
                    VALUES
                        (@Id, @Name, @UserId); ";

        var affectedRows = await connection.ExecuteAsync(sql, new { entity.Id, entity.Name, entity.UserId });
        if (affectedRows == 0) return false;

        return true;
    }

    public async Task<IList<Matter>> GetAllByUserIdAsync(Guid userId) {
        var sql = @"SELECT `Id`, `Name`, `UserId` 
                        FROM `Remembo`.`Matters` 
                    WHERE `UserId` = @UserId; ";

        var result = await connection.QueryAsync<Matter>(sql, new { UserId = userId });
        return result.ToList();
    }

    public async Task<Matter> SelectByIdAsync(Guid id, Guid userId) {
        var sql = @"SELECT `Id`, `Name`, `UserId` 
                        FROM `Remembo`.`Matters` 
                    WHERE `Id` = @Id AND `UserId` = @UserId; ";

        return await connection.QuerySingleAsync<Matter>(sql, new { Id = id, UserId = userId });
    }
}
