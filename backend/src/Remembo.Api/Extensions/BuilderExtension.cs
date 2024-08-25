using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MySqlConnector;
using Remembo.Domain;
using System.Text;

namespace Remembo.Api.Extensions;

public static class BuilderExtension {
    public static void AddConfiguration(this WebApplicationBuilder builder) {
        Configuration.Database.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        Configuration.Secrets.JwtPrivateKey = builder.Configuration.GetSection("Secrets").GetValue<string>("JwtPrivateKey") ?? string.Empty;
    }

    public static void AddDatabase(this WebApplicationBuilder builder) {
        builder.Services.AddTransient(x =>
            new MySqlConnection(builder.Configuration.GetConnectionString("Default")));
    }

    public static void AddJwtAuthentication(this WebApplicationBuilder builder) {
        builder.Services
            .AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.Secrets.JwtPrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        builder.Services.AddAuthorization();
    }
}

