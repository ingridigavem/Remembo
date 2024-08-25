using Microsoft.AspNetCore.Mvc;
using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Interfaces.Services;
using System.Security.Claims;

namespace Remembo.Api.Endpoints;

public static class MatterEndpoints {
    public static RouteGroupBuilder MapMatterEndpoints(this RouteGroupBuilder routeGroup) {

        routeGroup.MapPost("/create-matter", async (ClaimsPrincipal user, [FromBody] MatterDto matter, IMatterService matterService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await matterService.CreateMatterAsync(matter, userId);
            return Results.Json(result, statusCode: 200);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Create a new Matter",
            Description = "Recieve a matter name and return the matter ID",
        });


        routeGroup.MapGet("/get-all", async (ClaimsPrincipal user, IMatterService matterService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await matterService.GetAllMattersByUserIdAsync(userId);

            return Results.Json(result, statusCode: 200);
        }).RequireAuthorization()
          .WithOpenApi(operation => new(operation) {
              Summary = "Get all Matters by user id",
              Description = "Get all matters from user",
          });

        return routeGroup;
    }
}
