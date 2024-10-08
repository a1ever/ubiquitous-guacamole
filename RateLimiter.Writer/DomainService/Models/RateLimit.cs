namespace RateLimiter.Writer.DomainService.Models;

public class RateLimit
{
    public string Route { get; set; }
    public int RequestsPerMinute { get; set; }
}