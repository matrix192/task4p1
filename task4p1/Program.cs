var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<task4p1.Repositories.IBooksRepository, task4p1.Repositories.BooksRepository>();
builder.Services.AddSingleton<task4p1.Repositories.IAuthorsRepository, task4p1.Repositories.AuthorsRepository>();

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