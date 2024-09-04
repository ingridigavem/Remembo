using Microsoft.AspNetCore.Mvc;
using Remembo.Domain.Remembo.DTOs;
using Remembo.Domain.Remembo.Entities;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Domain.Shared.DTOs;
using System.Security.Claims;

namespace Remembo.Api.Endpoints;

public static class SubjectEndpoints {
    public static RouteGroupBuilder MapSubjectEndpoints(this RouteGroupBuilder routeGroup) {

        routeGroup.MapPost("/", async (ClaimsPrincipal user, [FromBody] SubjectDto subject, ISubjectService subjectService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await subjectService.CreateSubjectAsync(subject, userId);
            return Results.Json(result, statusCode: (int)result.Status);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Create a new Subject",
            Description = "Recieves a subject name and return the subject ID",
        }).Produces<Result<Subject>>(StatusCodes.Status201Created)
          .Produces<Result<Subject>>(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status401Unauthorized)
          .Produces<Result<Subject>>(StatusCodes.Status500InternalServerError);


        routeGroup.MapGet("/", async (ClaimsPrincipal user, ISubjectService subjectService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await subjectService.GetAllSubjectsByUserIdAsync(userId);

            return Results.Json(result, statusCode: (int)result.Status);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Get all Subjects",
            Description = "Get all subjects from user",
        }).Produces<Result<IList<Subject>>>(StatusCodes.Status200OK)
          .Produces<Result<IList<Subject>>>(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status401Unauthorized)
          .Produces<Result<IList<Subject>>>(StatusCodes.Status404NotFound)
          .Produces<Result<IList<Subject>>>(StatusCodes.Status500InternalServerError);


        routeGroup.MapGet("/{id}", async ([FromRoute] Guid id, ClaimsPrincipal user, ISubjectService subjectService) => {
            var tokenUserId = user.FindFirstValue("userId");
            if (string.IsNullOrEmpty(tokenUserId)) return Results.Json("Invalid token", statusCode: 405);

            _ = Guid.TryParse(tokenUserId, out var userId);
            var result = await subjectService.GetSubjectByIdAsync(id, userId);

            return Results.Json(result, statusCode: (int)result.Status);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Get a Subject by subject id",
            Description = "Get a subject filter by subject id from user",
        }).Produces<Result<Subject>>(StatusCodes.Status200OK)
          .Produces<Result<Subject>>(StatusCodes.Status400BadRequest)
          .Produces(StatusCodes.Status401Unauthorized)
          .Produces<Result<Subject>>(StatusCodes.Status404NotFound)
          .Produces<Result<Subject>>(StatusCodes.Status500InternalServerError);


        routeGroup.MapGet("/{subjectId}/contents", async ([FromRoute] Guid subjectId, IContentService contentService) => {
            var result = await contentService.GetAllContentsBySubjectIdAsync(subjectId);

            return Results.Json(result, statusCode: (int)result.Status);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Get all Content by subject",
            Description = "Get all contents from a subject",
        }).Produces<Result<IList<Content>>>(StatusCodes.Status200OK)
          .Produces<Result<IList<Content>>>(StatusCodes.Status400BadRequest)
          .Produces<Result<IList<Content>>>(StatusCodes.Status401Unauthorized)
          .Produces<Result<IList<Content>>>(StatusCodes.Status404NotFound)
          .Produces<Result<IList<Content>>>(StatusCodes.Status500InternalServerError);

        return routeGroup;
    }
}
