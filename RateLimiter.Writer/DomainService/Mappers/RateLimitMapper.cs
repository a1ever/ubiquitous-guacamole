using RateLimiter.Writer.DomainService.DTOs;
using RateLimiter.Writer.DomainService.Models;
using Riok.Mapperly.Abstractions;

namespace RateLimiter.Writer.DomainService.Mappers;

[Mapper]
public partial class RateLimitMapper
{
    public partial RateLimit ToDomain(RateLimitRequestDto dto);
    public partial RateLimitResponseDto ToDto(RateLimit domainModel);
}