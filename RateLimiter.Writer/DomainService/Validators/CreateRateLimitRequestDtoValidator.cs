using FluentValidation;
using RateLimiter.Writer.DomainService.DTOs;

namespace RateLimiter.Writer.DomainService.Validators;

public class CreateRateLimitRequestDtoValidator : AbstractValidator<RateLimitRequestDto>
{
    public CreateRateLimitRequestDtoValidator()
    {
        RuleFor(x => x.Route)
            .NotEmpty().WithMessage("Route не должен быть пустым.")
            .MaximumLength(256).WithMessage("Route не должен превышать 256 символов.");

        RuleFor(x => x.Rpm)
            .GreaterThan(0).WithMessage("Rpm должно быть больше нуля.")
            .LessThanOrEqualTo(100000).WithMessage("Rpm не должно превышать 100,000.");
    }
}