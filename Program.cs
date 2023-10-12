using Booker.Data;
using Booker.Repositories.Implementations;
using Booker.Repositories.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;

service.AddControllers();
service.AddEndpointsApiExplorer();
service.AddSwaggerGen();

service.AddSingleton<IBookRepository, BookRepository>();
service.AddSingleton<IAuthorRepository, AuthorRepository>();
service.AddSingleton<IPublisherRepository, PublisherRepository>();
service.AddSingleton<IGenreRepository, GenreRepository>();

service.AddDbContext<BookerDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
}, ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
