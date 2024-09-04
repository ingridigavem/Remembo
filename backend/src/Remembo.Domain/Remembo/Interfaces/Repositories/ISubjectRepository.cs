using Remembo.Domain.Remembo.Entities;

namespace Remembo.Domain.Remembo.Interfaces.Repositories;
public interface ISubjectRepository {
    Task<bool> InsertAsync(Subject entity);
    Task<IList<Subject>> GetAllByUserIdAsync(Guid userId);
    Task<Subject> SelectByIdAsync(Guid id, Guid userId);
}