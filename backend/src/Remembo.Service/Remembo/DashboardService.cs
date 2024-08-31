using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Interfaces.Repositories;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.Constants;
using Remembo.Domain.Shared.DTOs;
using System.Net;

namespace Remembo.Service.Remembo;
public class DashboardService(IDashboardRepository repository) : IDashboardService {
    public async Task<Result<DashboardDto>> GetDashboardDetailsAsync(Guid userId) {
        if (userId == Guid.Empty) return new Result<DashboardDto>(error: ErrorsMessages.NULL_USER_ID_ERROR, status: HttpStatusCode.BadRequest);

        #region Retrieve Data
        DashboardDto dashboardDto = new();
        try {
            dashboardDto.SetStatistics(await repository.GetStatisticsAsync(userId));
            dashboardDto.SetMatterDetailsList(await repository.GetAllNotReviewedByUserIdAsync(userId));
        } catch (Exception ex) {
            return new Result<DashboardDto>(error: ErrorsMessages.FAILED_TO_RETRIEVE_DATA_ERROR, exceptionMessage: ex.Message, status: HttpStatusCode.InternalServerError);
        }
        #endregion

        return new Result<DashboardDto>(data: dashboardDto, status: HttpStatusCode.OK);
    }
}
