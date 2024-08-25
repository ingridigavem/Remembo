using Microsoft.OpenApi.Models;
using Remembo.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.AddRememboContext();
builder.Services.AddCors();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Remembo API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insert the JWT token JWT bellow"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(
        s => {
            s.SwaggerEndpoint("/swagger/v1/swagger.json", "Remembo API v1");
        });
}
app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/api/health");
app.MapRememboEndpoints();

app.Run();

