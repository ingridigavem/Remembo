using Dapper;
using MySqlConnector;
using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;
using System.Transactions;

namespace Remembo.Data.Repositories.Remembo;
public class ReviewRepository(MySqlConnection connection) : IReviewRepository {
    public async Task<bool> UpdateCurrentReviewAsync(Guid currentReviewId, Guid contentId) {
        var updateCurrentReview = @"UPDATE `Remembo`.`Reviews` 
                                        SET `IsReviewed` = 1 
                                    WHERE `Id` = @CurrentReviewId; ";

        var updateContentReviewNumber = @"UPDATE `Remembo`.`Contents`
                                            SET `ReviewNumber` = `ReviewNumber` + 1
                                          WHERE `Id` = @ContentId; ";

        using MySqlTransaction transaction = await connection.BeginTransactionAsync();
        try {
            var updateCurrentAffectedRows = await connection.ExecuteAsync(updateCurrentReview, new { CurrentReviewId = currentReviewId }, transaction);
            if (updateCurrentAffectedRows == 0) throw new TransactionAbortedException();

            var updateContentAffectedRows = await connection.ExecuteAsync(updateContentReviewNumber, new { ContentId = contentId }, transaction);
            if (updateContentAffectedRows == 0) throw new TransactionAbortedException();

            await transaction.CommitAsync();
            return true;

        } catch (Exception) {
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> InsertNextReviewAsync(Review nextReview) {
        var sqlInsertReview = @"INSERT INTO `Remembo`.`Reviews`
                                    (`Id`, `ContentId`, `ScheduleReviewDate`, `IsReviewed`)               
                                VALUES
                                    (@Id, @ContentId, @ScheduleReviewDate, 0); ";

        var insertedReviewRows = await connection.ExecuteAsync(
                sqlInsertReview,
                new { nextReview.Id, ContentId = nextReview.Id, nextReview.ScheduleReviewDate }
        );
        if (insertedReviewRows == 0) return false;

        return true;
    }

    public async Task<ReviewContentDto> GetContentIdAndReviewNumberByReviewIdAsync(Guid currentReviewId) {
        var sql = @"SELECT r.`ContentId`,  c.`ReviewNumber`
	                    FROM `Remembo`.`Reviews` r
	                INNER JOIN `Remembo`.`Contents` c
                        ON (r.`ContentId` = c.`Id`)
                    WHERE r.`Id` = @Id; ";

        return await connection.QuerySingleAsync<ReviewContentDto>(sql, new { Id = currentReviewId });
    }

    public async Task<IList<Review>> GetAllNotReviewedByUserIdAsync(Guid userId) {
        var sql = @"SELECT r.`Id`, r.`ContentId`, r.`ScheduleReviewDate`, r.`IsReviewed`
                        FROM `Remembo`.`Reviews` r
                    INNER JOIN `Remembo`.`Contents` c
                        ON (r.`ContentId` = c.`Id`)
                    INNER JOIN `Remembo`.`Matters` m
                        ON (c.`MatterId` = m.`Id`)
                    INNER JOIN `Remembo`.`Users` u
	                    ON (m.`UserId` = u.`Id`)    
                    WHERE r.`IsReviewed` = 0 AND u.`Id` = @UserId;";

        var result = await connection.QueryAsync<Review>(sql, new { UserId = userId });
        return result.ToList();
    }
}
