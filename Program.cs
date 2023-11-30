using Booker.Data;
using Booker.Repositories.Implementations;
using Booker.Repositories.Models;
using Booker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Movies.Api.Identity;

var builder = WebApplication.CreateBuilder(args);
var service = builder.Services;
var config = builder.Configuration;

service.AddControllers();
service.AddEndpointsApiExplorer();
service.AddSwaggerGen();

// SERVICES
service.AddScoped<AuthorService>();
service.AddScoped<BookService>();
service.AddScoped<PublisherService>();
service.AddScoped<GenreService>();

// REPOSITORIES
service.AddScoped<IBookRepository, BookRepository>();
service.AddScoped<IAuthorRepository, AuthorRepository>();
service.AddScoped<IPublisherRepository, PublisherRepository>();
service.AddScoped<IGenreRepository, GenreRepository>();

// AUTHENTICATION
service.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(config["JwtSettings:Secret"]!
        )),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
service.AddAuthorization(options =>
{
    options.AddPolicy(IdentityData.AdminUserPolicyName, p =>
    p.RequireClaim(IdentityData.AdminUserClaimName, "true"));
});
Environment.SetEnvironmentVariable("Key", config["JwtSettings:Audience"]);

// DB CONTEXT
service.AddDbContext<BookerDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
