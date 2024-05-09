using Microsoft.AspNetCore.Http.HttpResults;
using TestExercise.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("api/groups/{id:int}", async (int id, IConfiguration configuration, IDbService db) =>
{
    var result = await db.GetGroupDetailsByIdAsync(id);
    return result is null
        ? Results.NotFound($"Group with id:{id} does not exits")
        : Results.Ok(result);
});

app.MapDelete("api/students/{id:int}", async (int id, IConfiguration configuration, IDbService db) =>
{
    var result = await db.RemoveStudentByIdAsync(id);
    return (result) 
        ? Results.NoContent()
        : Results.NotFound($"Student with id:{id} does not exist"); //Results.Created for app.MapPost
});
app.Run();
