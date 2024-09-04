using Dapper;
using MySqlConnector;
using Remembo.Domain;
using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Repositories;

namespace Remembo.Data.Repositories.Remembo;
public class ContentRepository : IContentRepository {
    public async Task<bool> InsertContentAndFirstReviewAsync(Content content, Review review) {

        var sqlInsertContent = @"INSERT INTO `Remembo`.`Contents`
                                    (`Id`, `SubjectId`, `Name`, `Note`, `ReviewNumber`)                
                                 VALUES
                                    (@Id, @SubjectId, @Name, @Note, @ReviewNumber); ";


        var sqlInsertReview = @"INSERT INTO `Remembo`.`Reviews`
                                    (`Id`, `ContentId`, `ScheduleReviewDate`, `IsReviewed`)               
                                VALUES
                                    (@Id, @ContentId, @ScheduleReviewDate, 0); ";

        using var connection = new MySqlConnection(Configuration.Database.ConnectionString);
        connection.Open();
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

    public async Task<IList<Content>> GetAllBySubjectIdAsync(Guid subjectId) {
        var sql = @"SELECT `Id`, `SubjectId`, `Name`, `Note`, `ReviewNumber` 
                        FROM `Remembo`.`Contents` 
                    WHERE `SubjectId` = @SubjectId; ";

        using var connection = new MySqlConnection(Configuration.Database.ConnectionString);
        connection.Open();
        var result = await connection.QueryAsync<Content>(sql, new { SubjectId = subjectId });
        return result.ToList();
    }

    public async Task<Content> SelectByIdAsync(Guid contentId) {
        var sql = @"SELECT `Id`, `SubjectId`, `Name`, `Note`, `ReviewNumber` 
                        FROM `Remembo`.`Contents` 
                    WHERE `Id` = @Id; ";

        using var connection = new MySqlConnection(Configuration.Database.ConnectionString);
        connection.Open();
        return await connection.QuerySingleAsync<Content>(sql, new { Id = contentId });
    }

    public async Task<DetailedContentDto?> GetContentDetailsAsync(Guid contentId, Guid reviewId) {
        var sql = @"SELECT 
	                    m.`Id` as SubjectId, m.`Name` as SubjectName,
	                    c.`Id` as ContentId, c.`Name` as ContentName, c.`Note` as Note, c.`ReviewNumber` as ReviewNumber, 
	                    r.`Id` as ReviewId, r.`ScheduleReviewDate`, r.`IsReviewed`
                    FROM `Remembo`.`Reviews` r
                    INNER JOIN `Remembo`.`Contents` c
                        ON (r.`ContentId` = c.`Id`)
                    INNER JOIN `Remembo`.`Subjects` m
                        ON (c.`SubjectId` = m.`Id`)
                    WHERE r.`Id` = @ReviewId AND c.`Id` = @ContentId ;";

        using var connection = new MySqlConnection(Configuration.Database.ConnectionString);
        connection.Open();
        var result = await connection.QueryAsync<SubjectWithContentDto, ContentDetailDto, ReviewDetailDto, DetailedContentDto>(
                sql,
                (subject, content, review) => {
                    var currentReview = new ReviewDetailDto(review.ReviewId, review.ScheduleReviewDate, review.IsReviewed);
                    var contentDetail = new ContentDetailDto(content.ContentId, content.ContentName, content.Note, content.ReviewNumber, currentReview);
                    var subjectContent = new SubjectWithContentDto(subject.SubjectId, subject.SubjectName, contentDetail);
                    return new DetailedContentDto(subjectContent);
                },
                param: new { ContentId = contentId, ReviewId = reviewId },
                splitOn: "ContentId, ReviewId"
            );

        return result.FirstOrDefault();
    }
}