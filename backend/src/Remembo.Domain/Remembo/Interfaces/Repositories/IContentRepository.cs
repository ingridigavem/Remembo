using Remembo.Domain.Remembo.Entities;

namespace Remembo.Domain.Remembo.Interfaces.Repositories;
public interface IContentRepository {
    Task<bool> InsertContentAndFirstReviewAsync(Content content, Review review);
    Task<IList<Content>> GetAllByMatterIdAsync(Guid matterId);
    Task<Content> SelectByIdAsync(Guid contentId);
}