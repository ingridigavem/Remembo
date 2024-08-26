using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Shared.DTOs;
using Remembo.Domain.Shared.Responses;

namespace Remembo.Domain.Remembo.Interfaces.Services;
public interface IContentService {
    Task<Result<IdResponse>> CreateContentAndFirstReviewAsync(ContentDto content);
    Task<Result<Content>> GetContentByIdAsync(Guid contentId);
    Task<Result<IList<Content>>> GetAllContentsByMatterIdAsync(Guid matterId);
}
