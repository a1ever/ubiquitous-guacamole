namespace RateLimiter.Writer.DomainService.DTOs;

public class RateLimitResponseDto
{
    public string Route { get; set; } = string.Empty;
    public int Rpm { get; set; }
}