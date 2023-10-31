using Booker.Controllers;
using Booker.Data;
using Booker.Repositories.Implementations;
using Booker.Repositories.Models;
using Booker.Services.Implementations;
using Booker.Services.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;

service.AddControllers();
service.AddEndpointsApiExplorer();
service.AddSwaggerGen();

// SERVICES
service.AddScoped<IAuthorService, AuthorService>();
service.AddScoped<IBookService, BookService>();
service.AddScoped<IPublisherService, PublisherService>();
service.AddScoped<IGenreService, GenreService>();

// REPOSITORIES
service.AddScoped<IBookRepository, BookRepository>();
service.AddScoped<IAuthorRepository, AuthorRepository>();
service.AddScoped<IPublisherRepository, PublisherRepository>();
service.AddScoped<IGenreRepository, GenreRepository>();

service.AddDbContext<BookerDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
}, ServiceLifetime.Scoped);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
