using FluentValidation;
using LearnCraft.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnCraft.API.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            _logger.LogWarning("Validation failed: {Errors}", exception.Errors);

            var errors = exception.Errors.Select(e => e.ErrorMessage).ToList();
            
            var response = ResponseDto<List<string>>.Failure(
                "Validation failed.", 
                StatusCodes.Status400BadRequest);
            response.Data = errors;

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception, "Exception occurred: {Message}", exception.Message);

            var response = ResponseDto<object>.Failure(
                "An unexpected error occurred.", 
                StatusCodes.Status500InternalServerError);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
