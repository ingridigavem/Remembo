using Dapper;
using MySqlConnector;
using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Interfaces.Repositories;

namespace Remembo.Data.Repositories.Remembo;
public class DashboardRepository(MySqlConnection connection) : IDashboardRepository {
    public async Task<StatisticsDto> GetStatisticsAsync(Guid userId) {
        var sql = @"SELECT
	                    COUNT(CASE WHEN r.`IsReviewed` = TRUE THEN 1 END) AS CompletedReviewsTotal, 
	                    COUNT(CASE WHEN c.`IsCompleted` = TRUE THEN 1 END) AS CompletedContentTotal, 
	                    COUNT(CASE WHEN c.`IsCompleted` = FALSE THEN 1 END) AS NotCompletedContentTotal
                    FROM `Remembo`.`Contents` c
                    INNER JOIN `Remembo`.`Subjects` m
                        ON (c.`SubjectId` = m.`Id`)
                    INNER JOIN `Remembo`.`Users` u
                        ON (m.`UserId` = u.`Id`)
                    INNER JOIN `Remembo`.`Reviews` r
                        ON (r.`ContentId` = c.`Id`)
                    WHERE u.`Id` = @UserId; ";

        return await connection.QuerySingleAsync<StatisticsDto>(sql, new { UserId = userId });
    }

    public async Task<IList<SubjectDetailsDto>> GetAllNotReviewedByUserIdAsync(Guid userId) {
        var lookup = new Dictionary<Guid, SubjectDetailsDto>();

        var sql = @"SELECT 
                        m.Id AS SubjectId, m.Name AS SubjectName,
                        c.Id AS ContentId, c.Name AS ContentName, c.Note AS Note, c.ReviewNumber AS ReviewNumber, 
                        r.Id AS ReviewId, r.ScheduleReviewDate AS ScheduleReviewDate, r.IsReviewed AS IsReviewed
                    FROM Remembo.Reviews r
                    INNER JOIN Remembo.Contents c
                        ON (r.ContentId = c.Id)
                    INNER JOIN Remembo.Subjects m
                        ON (c.SubjectId = m.Id)
                    INNER JOIN Remembo.Users u
                        ON (m.UserId = u.Id)    
                    WHERE r.IsReviewed = FALSE AND u.Id = @UserId; ";

        var result = await connection.QueryAsync<SubjectDetailsDto, ContentDetailDto, ReviewDetailDto, SubjectDetailsDto>(
                sql,
                (subject, content, review) => {

                    if (!lookup.TryGetValue(subject.SubjectId, out SubjectDetailsDto? subjectContent)) {
                        lookup.Add(subject.SubjectId, subjectContent = subject);
                    }

                    if (subjectContent.Contents == null)
                        subjectContent.Contents = new List<ContentDetailDto>();

                    if (content != null) {
                        var currentReview = new ReviewDetailDto(review.ReviewId, review.ScheduleReviewDate, review.IsReviewed);
                        var contentDetailList = new ContentDetailDto(content.ContentId, content.ContentName, content.Note, content.ReviewNumber, currentReview);
                        subjectContent.Contents.Add(contentDetailList);
                    }

                    return subjectContent;
                },
                param: new { UserId = userId },
                splitOn: "ContentId, ReviewId"
            );

        var subjectContentList = lookup.Values.ToList();
        return subjectContentList;
    }
}
