using GrpcService.Helpers;
using GrpcService.Mapper;
using GrpcService.Services;
using Infrastructure;
using Infrastructure.RepositoryPattern;
using Mapster;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
MapsterConfig.RegisterMapping();

builder.Services.AddDbContext<DbContext, GrpcProjectDbContext>();
builder.Services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    GrpcProjectDbContext db = serviceScope.ServiceProvider.GetRequiredService<GrpcProjectDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGrpcService<UserServiceInApp>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
