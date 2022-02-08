using Microsoft.EntityFrameworkCore;
using Minimal_Swagger_EF;
using Minimal_Swagger_EF.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MinimalApiContext>();
builder.Services.AddTransient<InjectExample>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/beers", () =>
{
    using var db = new MinimalApiContext();
    return db.People.ToList();
});

app.MapGet("/beerInject", async (MinimalApiContext db) =>
{
    return await db.People.ToListAsync();
});

app.MapGet("/InjectExample", (InjectExample ie) => ie.ShowUtcNow());

app.Run();
