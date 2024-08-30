using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Shared.DTOs;

namespace Remembo.Domain.Remembo.Interfaces.Services;
public interface IReviewService {
    Task<Result<Review>> ScheduleNextReviewAsync(Guid id);
    Task<Result<IList<Review>>> GetAllNotReviewedAsync(Guid userId);
}
