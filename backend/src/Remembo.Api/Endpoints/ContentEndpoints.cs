using Microsoft.AspNetCore.Mvc;
using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.DTOs;
using Remembo.Domain.Shared.Responses;

namespace Remembo.Api.Endpoints;

public static class ContentEndpoints {
    public static RouteGroupBuilder MapContentEndpoints(this RouteGroupBuilder routeGroup) {

        routeGroup.MapPost("/", async ([FromBody] ContentDto content, IContentService contentService) => {

            var result = await contentService.CreateContentAndFirstReviewAsync(content);
            return Results.Json(result, statusCode: (int)result.Status);

        }).WithOpenApi(operation => new(operation) {
            Summary = "Create a new Content and schedule the first review",
            Description = "Recieves a Content name and a MatterId and return the content ID. \n Create a content and the first scheduled review",
        }).Produces<Result<IdResponse>>(StatusCodes.Status201Created)
          .Produces<Result<IdResponse>>(StatusCodes.Status400BadRequest)
          .Produces<Result<IdResponse>>(StatusCodes.Status401Unauthorized)
          .Produces<Result<IdResponse>>(StatusCodes.Status500InternalServerError);


        routeGroup.MapGet("/{id}", async ([FromRoute] Guid id, IContentService contentService) => {
            var result = await contentService.GetContentByIdAsync(id);

            return Results.Json(result, statusCode: (int)result.Status);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Get a Content by content id",
            Description = "Get a content filter by content id",
        }).Produces<Result<Content>>(StatusCodes.Status200OK)
          .Produces<Result<Content>>(StatusCodes.Status400BadRequest)
          .Produces<Result<Content>>(StatusCodes.Status401Unauthorized)
          .Produces<Result<Content>>(StatusCodes.Status404NotFound)
          .Produces<Result<Content>>(StatusCodes.Status500InternalServerError);


        return routeGroup;
    }
}
