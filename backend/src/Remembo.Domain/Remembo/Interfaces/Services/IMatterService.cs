using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Shared.DTOs;
using Remembo.Domain.Shared.Responses;

namespace Remembo.Domain.Remembo.Interfaces.Services;
public interface IMatterService {
    Task<Result<IdResponse>> CreateMatterAsync(MatterDto matter, Guid userId);
    Task<Result<Matter>> GetMatterByIdAsync(Guid matterId);
    Task<Result<IList<Matter>>> GetAllMattersByUserIdAsync(Guid userId);
}
