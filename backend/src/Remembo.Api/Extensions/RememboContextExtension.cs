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
        builder.Services.AddScoped<IContentService, ContentService>();
        builder.Services.AddScoped<IReviewService, ReviewService>();
        builder.Services.AddScoped<IDashboardService, DashboardService>();

        #endregion

        #region REPOSITORIES

        builder.Services.AddScoped<IAccountRepository, AccountRepository>();
        builder.Services.AddScoped<IMatterRepository, MatterRepository>();
        builder.Services.AddScoped<IContentRepository, ContentRepository>();
        builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();

        #endregion
    }

    public static void MapRememboEndpoints(this IEndpointRouteBuilder app) {
        app.MapGroup("api/account").MapAccountEndpoints().WithTags("Account");
        app.MapGroup("api/matter").MapMatterEndpoints().WithTags("Matter").RequireAuthorization();
        app.MapGroup("api/content").MapContentEndpoints().WithTags("Content").RequireAuthorization();
        app.MapGroup("api/review").MapReviewEndpoints().WithTags("Review").RequireAuthorization();
        app.MapGroup("api/dashboard").MapDashboardEndpoints().WithTags("Dashboard").RequireAuthorization();
    }
}
