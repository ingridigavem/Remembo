using Microsoft.AspNetCore.Mvc;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.DTOs;
using System.Security.Claims;

namespace Remembo.Api.Endpoints;

public static class ReviewEndpoints {
    public static RouteGroupBuilder MapReviewEndpoints(this RouteGroupBuilder routeGroup) {

        routeGroup.MapPost("/{id}", async ([FromRoute] Guid id, IReviewService reviewService) => {
            var result = await reviewService.ScheduleNextReviewAsync(id);
            return Results.Json(result, statusCode: (int)result.Status);

        }).WithOpenApi(operation => new(operation) {
            Summary = "Check current review",
            Description = "Check current review and create and schedule next review",
        }).Produces<Result<Review>>(StatusCodes.Status200OK)
          .Produces<Result<Review>>(StatusCodes.Status400BadRequest)
          .Produces<Result<Review>>(StatusCodes.Status401Unauthorized)
          .Produces<Result<Review>>(StatusCodes.Status500InternalServerError);


        routeGroup.MapGet("/", async (ClaimsPrincipal user, IReviewService reviewService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await reviewService.GetAllNotReviewedAsync(userId);
            return Results.Json(result, statusCode: (int)result.Status);

        }).WithOpenApi(operation => new(operation) {
            Summary = "Get all reviews not reviewed",
            Description = "Get all reviews not reviewed from user",
        }).Produces<Result<IList<Review>>>(StatusCodes.Status200OK)
          .Produces<Result<IList<Review>>>(StatusCodes.Status400BadRequest)
          .Produces<Result<IList<Review>>>(StatusCodes.Status401Unauthorized)
          .Produces<Result<IList<Review>>>(StatusCodes.Status500InternalServerError);


        return routeGroup;
    }
}
