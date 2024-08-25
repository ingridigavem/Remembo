using Remembo.Domain.Remembo.Entities;

namespace Remembo.Domain.Remembo.Interfaces.Repositories;
public interface IMatterRepository {
    Task<bool> InsertAsync(Matter entity);
    Task<IList<Matter>> GetAllByUserIdAsync(Guid userId);
    Task<Matter> SelectByIdAsync(Guid id);
}