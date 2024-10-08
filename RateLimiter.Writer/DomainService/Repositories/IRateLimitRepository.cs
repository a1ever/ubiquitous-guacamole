using RateLimiter.Writer.DomainService.Models;

namespace RateLimiter.Writer.DomainService.Repositories;

public interface IRateLimitRepository
{
    Task<RateLimit> CreateAsync(RateLimit rateLimit);
    Task<RateLimit> GetByRouteAsync(string route);
    Task<bool> UpdateAsync(RateLimit rateLimit);
    Task<bool> DeleteAsync(string route);
}