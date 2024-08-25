using Remembo.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddDatabase();
builder.AddJwtAuthentication();
builder.AddRememboContext();
builder.Services.AddCors();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI(
        s => {
            s.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDoList API v1");
        });
}
app.UseHttpsRedirection();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
app.UseAuthorization();

app.UseHealthChecks("/api/health");
app.MapRememboEndpoints();

app.Run();

