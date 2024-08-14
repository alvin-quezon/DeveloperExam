using Asp.Versioning;
using DeveloperExam.Api.Endpoints.UserProfile.Commands;
using DeveloperExam.Api.Endpoints.UserProfile.Queries;
using DeveloperExam.Api.Middleware;
using DeveloperExam.Application.Behaviors;
using DeveloperExam.Application.UserProfile.Queries.GetUserProfileById;
using DeveloperExam.Domain.Abstractions;
using DeveloperExam.Infrastructure.Context;
using DeveloperExam.Infrastructure.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);
var applicationProjectAssembly = typeof(GetUserProfileByIdResponse).Assembly;

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Core Application
builder.Services.AddDbContext<DeveloperExamContext>(context => context.UseSqlServer(builder.Configuration.GetConnectionString("DeveloperExam")));
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(applicationProjectAssembly));
builder.Services.AddTransient<IUnitOfWork>(factory => factory.GetRequiredService<DeveloperExamContext>());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddValidatorsFromAssembly(applicationProjectAssembly);
builder.Services.AddCors();

// Core Services
builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();

// Api versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("AllowedAddress").Get<string[]>())
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithHeaders(HeaderNames.ContentType);
    });
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

// Add endpoints
app.UserProfileQueryEndpoints();
app.UserProfileCommandEndpoints();

app.Run();
