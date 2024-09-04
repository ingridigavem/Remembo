using Remembo.Domain.Remembo.DTOs;

namespace Remembo.Domain.Remembo.Interfaces.Repositories;
public interface IDashboardRepository {
    Task<IList<SubjectDetailsDto>> GetAllNotReviewedByUserIdAsync(Guid userId);
    Task<StatisticsDto> GetStatisticsAsync(Guid userId);
}
