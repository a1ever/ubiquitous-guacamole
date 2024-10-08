using RateLimiter.Writer.DomainService.Repositories;
using RateLimiter.Writer.DomainService.Services.Abstractions;
using FluentValidation;
using RateLimiter.Writer.DomainService.DTOs;
using RateLimiter.Writer.DomainService.Exceptions;
using RateLimiter.Writer.DomainService.Mappers;
using ValidationException = FluentValidation.ValidationException;

namespace RateLimiter.Writer.DomainService.Services.Implementations;

public class WriterDomainService : IWriterDomainService
    {
        private readonly IRateLimitRepository _rateLimitRepository;
        private readonly RateLimitMapper _rateLimitMapper;
        private readonly IValidator<RateLimitRequestDto> _createValidator;
        private readonly IValidator<RateLimitRequestDto> _updateValidator;

        public WriterDomainService(
            IRateLimitRepository rateLimitRepository,
            RateLimitMapper rateLimitMapper,
            IValidator<RateLimitRequestDto> createValidator,
            IValidator<RateLimitRequestDto> updateValidator)
        {
            _rateLimitRepository = rateLimitRepository;
            _rateLimitMapper = rateLimitMapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<RateLimitResponseDto> CreateLimitAsync(RateLimitRequestDto dto)
        {
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var rateLimit = _rateLimitMapper.ToDomain(dto);

            var existingLimit = await _rateLimitRepository.GetByRouteAsync(rateLimit.Route);
            if (existingLimit != null)
            {
                throw new RateLimitAlreadyExistsException(rateLimit.Route);
            }

            var createdLimit = await _rateLimitRepository.CreateAsync(rateLimit);

            return _rateLimitMapper.ToDto(createdLimit);
        }

        public async Task<RateLimitResponseDto> GetLimitAsync(string route)
        {
            var rateLimit = await _rateLimitRepository.GetByRouteAsync(route);
            if (rateLimit == null)
            {
                throw new RateLimitNotFoundException(route);
            }

            return _rateLimitMapper.ToDto(rateLimit);
        }

        public async Task<bool> UpdateLimitAsync(RateLimitRequestDto dto)
        {
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingLimit = await _rateLimitRepository.GetByRouteAsync(dto.Route);
            if (existingLimit == null)
            {
                throw new RateLimitNotFoundException(dto.Route);
            }

            var rateLimitToUpdate = _rateLimitMapper.ToDomain(dto);

            return await _rateLimitRepository.UpdateAsync(rateLimitToUpdate);
        }

        public async Task<bool> DeleteLimitAsync(string route)
        {
            var existingLimit = await _rateLimitRepository.GetByRouteAsync(route);
            if (existingLimit == null)
            {
                throw new RateLimitNotFoundException(route);
            }

            return await _rateLimitRepository.DeleteAsync(route);
        }
    }