using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Shared.DTOs;

namespace Remembo.Domain.Remembo.Interfaces.Services;
public interface IDashboardService {
    Task<Result<DashboardDto>> GetDashboardDetailsAsync(Guid userId);
}
