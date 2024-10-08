using RateLimiter.Writer.DomainService.DTOs;
using RateLimiter.Writer.DomainService.Models;

namespace RateLimiter.Writer.DomainService.Services.Abstractions;

public interface IWriterDomainService
{
    Task<RateLimitResponseDto> CreateLimitAsync(RateLimitRequestDto dto);
    Task<RateLimitResponseDto> GetLimitAsync(string route);
    Task<bool> UpdateLimitAsync(RateLimitRequestDto dto);
    Task<bool> DeleteLimitAsync(string route);
}