using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;

namespace Remembo.Domain.Remembo.Interfaces.Repositories;
public interface IContentRepository {
    Task<bool> InsertContentAndFirstReviewAsync(Content content, Review review);
    Task<IList<Content>> GetAllBySubjectIdAsync(Guid subjectId);
    Task<Content> SelectByIdAsync(Guid contentId);
    Task<DetailedContentDto?> GetContentDetailsAsync(Guid contentId, Guid reviewId);
}