namespace RateLimiter.Writer.DomainService.Exceptions;

public class RateLimitAlreadyExistsException : Exception
{
    public RateLimitAlreadyExistsException(string route)
        : base($"Rate limit for route '{route}' already exists.")
    {
    }
}