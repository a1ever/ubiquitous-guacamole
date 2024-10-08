namespace RateLimiter.Writer.DomainService.DTOs;

public class RateLimitRequestDto
{
    public string Route { get; set; } = string.Empty;
    public int Rpm { get; set; }
}