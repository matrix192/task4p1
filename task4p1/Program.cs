var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<task4p1.Repositories.IBooksRepository, task4p1.Repositories.BooksRepository>();
builder.Services.AddSingleton<task4p1.Repositories.IAuthorsRepository, task4p1.Repositories.AuthorsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();