using Dapper;
using MySqlConnector;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;

namespace Remembo.Data.Repositories.Remembo;
public class ContentRepository(MySqlConnection connection) : IContentRepository {
    public async Task<bool> InsertContentAndFirstReviewAsync(Content content, Review review) {

        var sqlInsertContent = @"INSERT INTO `Remembo`.`Contents`
                                    (`Id`, `MatterId`, `Name`, `Note`, `ReviewNumber`)                
                                 VALUES
                                    (@Id, @MatterId, @Name, @Note, @ReviewNumber); ";


        var sqlInsertReview = @"INSERT INTO `Remembo`.`Reviews`
                                    (`Id`, `ContentId`, `ScheduleReviewDate`, `IsReviewed`)               
                                VALUES
                                    (@Id, @ContentId, @ScheduleReviewDate, 0); ";

        using MySqlTransaction transaction = await connection.BeginTransactionAsync();
        try {
            var insertedContentRows = await connection.ExecuteAsync(sqlInsertContent, content, transaction);
            if (insertedContentRows == 0) throw new Exception();

            var insertedReviewRows = await connection.ExecuteAsync(
                sqlInsertReview,
                new { review.Id, ContentId = content.Id, review.ScheduleReviewDate },
                transaction
            );
            if (insertedReviewRows == 0) throw new Exception();

            await transaction.CommitAsync();
            return true;
        } catch (Exception) {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<IList<Content>> GetAllByMatterIdAsync(Guid matterId) {
        var sql = @"SELECT `Id`, `MatterId`, `Name`, `Note`, `ReviewNumber` 
                        FROM `Remembo`.`Contents` 
                    WHERE `MatterId` = @MatterId; ";

        var result = await connection.QueryAsync<Content>(sql, new { MatterId = matterId });
        return result.ToList();
    }

    public async Task<Content> SelectByIdAsync(Guid contentId) {
        var sql = @"SELECT `Id`, `MatterId`, `Name`, `Note`, `ReviewNumber` 
                        FROM `Remembo`.`Contents` 
                    WHERE `Id` = @Id; ";

        return await connection.QuerySingleAsync<Content>(sql, new { Id = contentId });
    }
}