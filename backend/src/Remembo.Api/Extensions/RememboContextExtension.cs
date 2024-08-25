using Remembo.Api.Endpoints;
using Remembo.Data.Repositories.Account;
using Remembo.Data.Repositories.Remembo;
using Remembo.Domain.Account.Interfaces;
using Remembo.Domain.Remembo.Interfaces.Repositories;
using Remembo.Domain.Remembo.Interfaces.Services;
using Remembo.Service.Account;
using Remembo.Service.Remembo;

namespace Remembo.Api.Extensions;

public static class RememboContextExtension {
    public static void AddRememboContext(this WebApplicationBuilder builder) {
        #region SERVICES

        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddScoped<IMatterService, MatterService>();

        #endregion

        #region REPOSITORIES

        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IMatterRepository, MatterRepository>();

        #endregion
    }

    public static void MapRememboEndpoints(this IEndpointRouteBuilder app) {
        app.MapGroup("api/account").MapAccountEndpoints().WithTags("Account");
        app.MapGroup("api/matter").MapMatterEndpoints().WithTags("Matter").RequireAuthorization();

    }


}
