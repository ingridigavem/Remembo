using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.DTOs;
using System.Security.Claims;

namespace Remembo.Api.Endpoints;

public static class DashboardEndpoints {
    public static RouteGroupBuilder MapDashboardEndpoints(this RouteGroupBuilder routeGroup) {
        routeGroup.MapGet("/", async (ClaimsPrincipal user, IDashboardService dashboardService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await dashboardService.GetDashboardDetailsAsync(userId);
            return Results.Json(result, statusCode: (int)result.Status);

        }).WithOpenApi(operation => new(operation) {
            Summary = "Get dashboard information",
            Description = "Get all reviews not reviewed from user and statitics",
        }).Produces<Result<DashboardDto>>(StatusCodes.Status200OK)
      .Produces<Result<DashboardDto>>(StatusCodes.Status400BadRequest)
      .Produces<Result<DashboardDto>>(StatusCodes.Status401Unauthorized)
      .Produces<Result<DashboardDto>>(StatusCodes.Status500InternalServerError);

        return routeGroup;
    }
}
