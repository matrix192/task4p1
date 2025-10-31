using Microsoft.EntityFrameworkCore;
using task4p1.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var conn = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=library.db";
builder.Services.AddDbContext<LibraryContext>(options => options.UseSqlite(conn));

builder.Services.AddScoped<task4p1.Repositories.IBooksRepository, task4p1.Repositories.BooksRepository>();
builder.Services.AddScoped<task4p1.Repositories.IAuthorsRepository, task4p1.Repositories.AuthorsRepository>();

builder.Services.AddScoped<task4p1.Services.AuthorService>();
builder.Services.AddScoped<task4p1.Services.BookService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();