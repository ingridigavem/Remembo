using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Shared.DTOs;

namespace Remembo.Domain.Remembo.Interfaces.Services;
public interface ISubjectService {
    Task<Result<Subject>> CreateSubjectAsync(SubjectDto subject, Guid userId);
    Task<Result<Subject>> GetSubjectByIdAsync(Guid subjectId, Guid userId);
    Task<Result<IList<Subject>>> GetAllSubjectsByUserIdAsync(Guid userId);
}
