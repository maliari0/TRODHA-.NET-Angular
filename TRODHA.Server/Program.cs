using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TRODHA.Application.Services;
using TRODHA.Application.Services.Interfaces;
using TRODHA.Core.Interfaces;
using TRODHA.Infrastructure.Data;
using TRODHA.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using TRODHA.Application.Validators;
using System.Reflection;
using TRODHA.Server.Middleware;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<IGoalStatusRepository, GoalStatusRepository>();
builder.Services.AddScoped<IUserNoteRepository, UserNoteRepository>();
builder.Services.AddScoped<INoteImageRepository, NoteImageRepository>();
builder.Services.AddScoped<IRecommendationRepository, RecommendationRepository>();
builder.Services.AddScoped<IImportanceLevelRepository, ImportanceLevelRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IGoalStatusService, GoalStatusService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IGoalStatusService, GoalStatusService>();
builder.Services.AddScoped<IUserNoteService, UserNoteService>();
builder.Services.AddScoped<INoteImageService, NoteImageService>();
builder.Services.AddScoped<IRecommendationService, RecommendationService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateGoalDtoValidator>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddScoped<IAuthService, AuthService>();

var jwtKey = builder.Configuration["Jwt:Secret"] ??
             Environment.GetEnvironmentVariable("JWT_SECRET");

if (string.IsNullOrEmpty(jwtKey))
    throw new InvalidOperationException("JWT Secret is not configured");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
            ValidateIssuer = builder.Environment.IsProduction(),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = builder.Environment.IsProduction(),
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TRODHA API", Version = "v1" });

    // Configure Swagger to use JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(builder => builder
    .WithOrigins("http://localhost:4200", "https://localhost:7056", "http://localhost:5173", "https://127.0.0.1:57438", "https://localhost:57438")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
