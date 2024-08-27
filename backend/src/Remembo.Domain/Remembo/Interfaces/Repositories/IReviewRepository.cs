using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;

namespace Remembo.Domain.Remembo.Interfaces.Repositories;
public interface IReviewRepository {
    Task<bool> InsertNextReviewAsync(Review nextReview);
    Task<bool> UpdateCurrentReviewAsync(Guid currentReviewId, Guid contentId);
    Task<ReviewContentDto> GetContentIdAndReviewNumberByReviewIdAsync(Guid currentReviewId);
    Task<IList<Review>> GetAllNotReviewedByUserIdAsync(Guid userId);
}
