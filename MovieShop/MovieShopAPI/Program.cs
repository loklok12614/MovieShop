using System.Text;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieShopAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<ICastService, CastService>();

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddScoped<IAccountService, AccountService>();

//inject connection string from appsettings.json in to MovieShopDbContext class
builder.Services.AddDbContext<MovieShopDbContext>( options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieShopDbConnection"));
});

// API is gonna use JWT authentication, so that it can look at the incoming request and look for Token
// If token is valid, it will get the claims in the HttpContext
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["secretKey"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMovieShopExceptionMiddleware();
app.UseHttpsRedirection();

// Authentication middleware !!!
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();