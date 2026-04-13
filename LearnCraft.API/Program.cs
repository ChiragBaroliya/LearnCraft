using FluentValidation;
using LearnCraft.API.Middleware;
using LearnCraft.Application.Behaviors;
using LearnCraft.Application.Common.Models;
using LearnCraft.Application.Data;
using LearnCraft.Application.Interfaces.Authentication;
using LearnCraft.Application.Interfaces.Data;
using LearnCraft.Application.Interfaces.Repositories;
using LearnCraft.Infrastructure.Authentication;
using LearnCraft.Infrastructure.Data;
using LearnCraft.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
// using Microsoft.OpenApi.Models; // Removed for Swagger removal
using Serilog;
using System.Text;
using LearnCraft.API.GraphQL.Courses;
using LearnCraft.API.GraphQL.Enrollments;
using LearnCraft.API.GraphQL.Lessons;
using LearnCraft.API.GraphQL.Progress;
using LearnCraft.API.GraphQL.Users;

var builder = WebApplication.CreateBuilder(args);

// Serilog Setup
builder.Host.UseSerilog((context, loggerConfiguration) => 
    loggerConfiguration.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllers();
// Swagger and EndpointsApiExplorer removed

builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query"))
    .AddTypeExtension<CourseQueries>()
    .AddTypeExtension<EnrollmentQueries>()
    .AddTypeExtension<LessonQueries>()
    .AddTypeExtension<ProgressQueries>()
    .AddTypeExtension<UserQueries>()
    .AddMutationType(d => d.Name("Mutation"))
    .AddTypeExtension<CourseMutations>()
    .AddTypeExtension<EnrollmentMutations>()
    .AddTypeExtension<LessonMutations>()
    .AddTypeExtension<ProgressMutations>()
    .AddTypeExtension<UserMutations>()
    .AddAuthorization();

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddScoped<IApplicationDbContext>(sp => 
    sp.GetRequiredService<ApplicationDbContext>());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IProgressRepository, ProgressRepository>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// MediatR & Behaviors
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(LearnCraft.Application.Data.IApplicationDbContext).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

// Validation
builder.Services.AddValidatorsFromAssembly(typeof(LearnCraft.Application.Data.IApplicationDbContext).Assembly);

// JWT Authentication
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                
                var response = ResponseDto<object>.Failure(
                    "You are not authorized to access this resource.", 
                    StatusCodes.Status401Unauthorized);
                
                await context.Response.WriteAsJsonAsync(response);
            },
            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = ResponseDto<object>.Failure(
                    "You do not have permission to perform this action.", 
                    StatusCodes.Status403Forbidden);

                await context.Response.WriteAsJsonAsync(response);
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Apply Migrations automatically on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.MigrateAsync();
    await DbSeeder.SeedAsync(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger middleware removed
}

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/graphql"));

app.MapControllers();
app.MapGraphQL();

app.Run();
