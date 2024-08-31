using Remembo.Domain.Remembo.DTOs;

namespace Remembo.Domain.Remembo.Interfaces.Repositories;
public interface IDashboardRepository {
    Task<IList<MatterDetailsDto>> GetAllNotReviewedByUserIdAsync(Guid userId);
    Task<StatisticsDto> GetStatisticsAsync(Guid userId);
}
