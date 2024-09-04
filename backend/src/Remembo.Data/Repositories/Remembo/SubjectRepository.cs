using Dapper;
using MySqlConnector;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;

namespace Remembo.Data.Repositories.Remembo;
public class SubjectRepository(MySqlConnection connection) : ISubjectRepository {
    public async Task<bool> InsertAsync(Subject entity) {
        var sql = @"INSERT INTO `Remembo`.`Subjects`
                        (`Id`, `Name`, `UserId`)
                    VALUES
                        (@Id, @Name, @UserId); ";

        var affectedRows = await connection.ExecuteAsync(sql, new { entity.Id, entity.Name, entity.UserId });
        if (affectedRows == 0) return false;

        return true;
    }

    public async Task<IList<Subject>> GetAllByUserIdAsync(Guid userId) {
        var sql = @"SELECT `Id`, `Name`, `UserId` 
                        FROM `Remembo`.`Subjects` 
                    WHERE `UserId` = @UserId; ";

        var result = await connection.QueryAsync<Subject>(sql, new { UserId = userId });
        return result.ToList();
    }

    public async Task<Subject> SelectByIdAsync(Guid id, Guid userId) {
        var sql = @"SELECT `Id`, `Name`, `UserId` 
                        FROM `Remembo`.`Subjects` 
                    WHERE `Id` = @Id AND `UserId` = @UserId; ";

        return await connection.QuerySingleAsync<Subject>(sql, new { Id = id, UserId = userId });
    }
}
