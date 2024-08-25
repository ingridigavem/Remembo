using Microsoft.AspNetCore.Mvc;
using Remembo.Domain.Account.DTOs;
using Remembo.Domain.Account.Interfaces;
using Remembo.Domain.Shared.DTOs;
using Remembo.Domain.Shared.Responses;

namespace Remembo.Api.Endpoints;

public static class AccountEndpoints {
    public static RouteGroupBuilder MapAccountEndpoints(this RouteGroupBuilder routeGroup) {
        routeGroup.MapPost("/create-account", async ([FromBody] UserDto user, IAccountService accountService) => {
            var result = await accountService.CreateUserAsync(user);
            return Results.Json(result, statusCode: (int)result.Status);

        }).WithOpenApi(operation => new(operation) {
            Summary = "Create a new User"
        }).Produces<Result<IdResponse>>(StatusCodes.Status201Created)
          .Produces<Result<IdResponse>>(StatusCodes.Status400BadRequest)
          .Produces<Result<IdResponse>>(StatusCodes.Status500InternalServerError);

        routeGroup.MapPost("/login", async ([FromBody] LoginDto login, IAccountService accountService) => {
            var result = await accountService.LoginAsync(login);
            return Results.Json(result, statusCode: (int)result.Status);

        }).WithOpenApi(operation => new(operation) {
            Summary = "Login",
            Description = "Retrieve a token for a user",
        }).Produces<Result<string>>(StatusCodes.Status200OK)
          .Produces<Result<string>>(StatusCodes.Status400BadRequest)
          .Produces<Result<string>>(StatusCodes.Status500InternalServerError);
        return routeGroup;
    }
}