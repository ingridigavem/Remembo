using Microsoft.AspNetCore.Mvc;
using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.DTOs;
using System.Security.Claims;

namespace Remembo.Api.Endpoints;

public static class MatterEndpoints {
    public static RouteGroupBuilder MapMatterEndpoints(this RouteGroupBuilder routeGroup) {

        routeGroup.MapPost("/", async (ClaimsPrincipal user, [FromBody] MatterDto matter, IMatterService matterService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await matterService.CreateMatterAsync(matter, userId);
            return Results.Json(result, statusCode: (int)result.Status);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Create a new Matter",
            Description = "Recieves a matter name and return the matter ID",
        }).Produces<Result<Matter>>(StatusCodes.Status201Created)
          .Produces<Result<Matter>>(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status401Unauthorized)
          .Produces<Result<Matter>>(StatusCodes.Status500InternalServerError);


        routeGroup.MapGet("/", async (ClaimsPrincipal user, IMatterService matterService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await matterService.GetAllMattersByUserIdAsync(userId);

            return Results.Json(result, statusCode: (int)result.Status);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Get all Matters",
            Description = "Get all matters from user",
        }).Produces<Result<IList<Matter>>>(StatusCodes.Status200OK)
          .Produces<Result<IList<Matter>>>(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status401Unauthorized)
          .Produces<Result<IList<Matter>>>(StatusCodes.Status404NotFound)
          .Produces<Result<IList<Matter>>>(StatusCodes.Status500InternalServerError);


        routeGroup.MapGet("/{id}", async ([FromRoute] Guid id, ClaimsPrincipal user, IMatterService matterService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await matterService.GetMatterByIdAsync(id, userId);

            return Results.Json(result, statusCode: (int)result.Status);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Get a Matter by matter id",
            Description = "Get a matter filter by matter id from user",
        }).Produces<Result<Matter>>(StatusCodes.Status200OK)
          .Produces<Result<Matter>>(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status401Unauthorized)
          .Produces<Result<Matter>>(StatusCodes.Status404NotFound)
          .Produces<Result<Matter>>(StatusCodes.Status500InternalServerError);


        routeGroup.MapGet("/{matterId}/contents", async ([FromRoute] Guid matterId, IContentService contentService) => {
            var result = await contentService.GetAllContentsByMatterIdAsync(matterId);

            return Results.Json(result, statusCode: (int)result.Status);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Get all Content by matter",
            Description = "Get all contents from a matter",
        }).Produces<Result<IList<Content>>>(StatusCodes.Status200OK)
          .Produces<Result<IList<Content>>>(StatusCodes.Status400BadRequest)
          .Produces<Result<IList<Content>>>(StatusCodes.Status401Unauthorized)
          .Produces<Result<IList<Content>>>(StatusCodes.Status404NotFound)
          .Produces<Result<IList<Content>>>(StatusCodes.Status500InternalServerError);

        return routeGroup;
    }
}
