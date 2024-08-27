using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Shared.DTOs;

namespace Remembo.Domain.Remembo.Interfaces.Services;
public interface IMatterService {
    Task<Result<Matter>> CreateMatterAsync(MatterDto matter, Guid userId);
    Task<Result<Matter>> GetMatterByIdAsync(Guid matterId, Guid userId);
    Task<Result<IList<Matter>>> GetAllMattersByUserIdAsync(Guid userId);
}
