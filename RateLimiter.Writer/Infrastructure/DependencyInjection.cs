using FluentValidation;
using RateLimiter.Writer.DomainService.DTOs;
using RateLimiter.Writer.DomainService.Mappers;
using RateLimiter.Writer.DomainService.Repositories;
using RateLimiter.Writer.DomainService.Services.Abstractions;
using RateLimiter.Writer.DomainService.Services.Implementations;
using RateLimiter.Writer.DomainService.Validators;
using RateLimiter.Writer.Infrastructure.Repositories;

namespace RateLimiter.Writer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IRateLimitRepository, RateLimitRepository>();

        services.AddSingleton<RateLimitMapper>();

        services.AddTransient<IValidator<RateLimitRequestDto>, CreateRateLimitRequestDtoValidator>();
        services.AddTransient<IValidator<RateLimitRequestDto>, UpdateRateLimitRequestDtoValidator>();

        services.AddScoped<IWriterDomainService, WriterDomainService>();

        return services;
    }
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IRateLimitRepository, RateLimitRepository>();

        return services;
    }
}