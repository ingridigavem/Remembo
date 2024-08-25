using Microsoft.AspNetCore.Mvc;
using Remembo.Domain.Account.DTOs;
using Remembo.Domain.Account.Interfaces;

namespace Remembo.Api.Endpoints;

public static class AccountEndpoints {
    public static RouteGroupBuilder MapAccountEndpoints(this RouteGroupBuilder routeGroup) {
        routeGroup.MapPost("/create-account", async ([FromBody] UserDto user, IAccountService accountService) => {
            var result = await accountService.CreateUserAsync(user);
            return Results.Json(result, statusCode: 200);

        }).WithOpenApi(operation => new(operation) {
            Summary = "Create a new User"
        });

        routeGroup.MapPost("/login", async ([FromBody] LoginDto login, IAccountService accountService) => {
            var result = await accountService.LoginAsync(login);
            return Results.Json(result, statusCode: 200);
        }).WithOpenApi(operation => new(operation) {
            Summary = "Login",
            Description = "Retrieve a token for a user",
        });
        return routeGroup;
    }
}