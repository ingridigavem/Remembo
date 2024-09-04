using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Shared.DTOs;

namespace Remembo.Domain.Remembo.Interfaces.Services;
public interface IContentService {
    Task<Result<DetailedContentDto?>> CreateContentAndFirstReviewAsync(ContentDto content);
    Task<Result<Content>> GetContentByIdAsync(Guid contentId);
    Task<Result<IList<Content>>> GetAllContentsBySubjectIdAsync(Guid subjectId);
}
