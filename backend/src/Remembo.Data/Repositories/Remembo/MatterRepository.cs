using Dapper;
using MySqlConnector;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;

namespace Remembo.Data.Repositories.Remembo;
public class MatterRepository(MySqlConnection connection) : IMatterRepository {
    public async Task<bool> InsertAsync(Matter entity) {
        var sql = @" 
            INSERT INTO `Remembo`.`Matters`
                (`Id`, `Name`, `UserId`)
            VALUES
                (@Id, @Name, @UserId); ";

        var affectedRows = await connection.ExecuteAsync(sql, new { entity.Id, entity.Name, entity.UserId });
        if (affectedRows == 0) return false;

        return true;
    }
    public Task<IList<Matter>> GetAllByUserIdAsync(Guid userId) {
        throw new NotImplementedException();
    }

    public Task<Matter> SelectByIdAsync(Guid id) {
        throw new NotImplementedException();
    }
}
