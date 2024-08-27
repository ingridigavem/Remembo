using Microsoft.AspNetCore.Mvc;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.DTOs;
using Remembo.Domain.Shared.Responses;

namespace Remembo.Api.Endpoints;

public static class ReviewEndpoints {
    public static RouteGroupBuilder MapReviewEndpoints(this RouteGroupBuilder routeGroup) {

        routeGroup.MapPost("/{id}", async ([FromRoute] Guid id, IReviewService reviewService) => {
            var result = await reviewService.ScheduleNextReviewAsync(id);
            return Results.Json(result, statusCode: (int)result.Status);

        }).WithOpenApi(operation => new(operation) {
            Summary = "CHECKA A REVISAO ATUAL",
            Description = "",
        }).Produces<Result<IdResponse>>(StatusCodes.Status201Created)
          .Produces<Result<IdResponse>>(StatusCodes.Status400BadRequest)
          .Produces<Result<IdResponse>>(StatusCodes.Status401Unauthorized)
          .Produces<Result<IdResponse>>(StatusCodes.Status500InternalServerError);


        return routeGroup;
    }
}
