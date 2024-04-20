using api.Data;
using api.Seeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();

// Register the TestDBContext with dependency injection
builder.Services.AddDbContext<TestDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.MapControllers(); // Map controllers

app.UseHttpsRedirection();

app.MapPost("/api/seed", async (TestDBContext dbContext, HttpContext httpContext) =>
{
    try
    {
        // Retrieve the file path from the form data
        var formCollection = await httpContext.Request.ReadFormAsync();
        string? filePath = formCollection["filePath"];

        // Validate if filePath is not empty or null

        if (string.IsNullOrEmpty(filePath))
        {
            return Results.BadRequest("File path is missing in the request.");
        }

        // Proceed with seeding the records
        var seeder = new DataSeeder(dbContext);
        seeder.SeedRecordsFromCsv(filePath);

        return Results.Ok("Records seeded successfully.");
    }
    catch (Exception ex)
    {
        return Results.BadRequest($"An error occurred while seeding records: {ex.Message}");
    }
});


app.Run();
