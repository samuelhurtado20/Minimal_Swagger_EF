using Microsoft.EntityFrameworkCore;
using Minimal_Swagger_EF;
using Minimal_Swagger_EF.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<MinimalApiContext>();
builder.Services.AddTransient<InjectExample>();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

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

app.MapGet("/ShowUtcNow", (InjectExample ie) => ie.ShowUtcNow());

app.MapPost("/AddPerson", async (PersonRequest person, MinimalApiContext db) => 
{
    try
    {
        Person entity = new()
        {
            Name = person.Name,
            Age = person.Age,
            JobPosition = person.JobPosition
        };

        await db.People.AddAsync(entity);
        await db.SaveChangesAsync();
        return Results.Ok(entity);
    }
    catch (global::System.Exception)
    {
        return Results.BadRequest();
    }
});

app.MapGet("/GetPerson/{id}", async (int id, MinimalApiContext db) =>
{
    try
    {
        var entity = await db.People.FindAsync(id);
        return entity != null ? Results.Ok(entity) : Results.NotFound();
    }
    catch (global::System.Exception)
    {
        return Results.BadRequest();
    }
});

app.Run();
