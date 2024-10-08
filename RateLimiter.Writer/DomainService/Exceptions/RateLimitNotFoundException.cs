namespace RateLimiter.Writer.DomainService.Exceptions;

public class RateLimitNotFoundException : Exception
{
    public RateLimitNotFoundException(string route)
        : base($"Rate limit for route '{route}' not found.")
    {
    }
}