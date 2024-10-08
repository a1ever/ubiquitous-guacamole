using FluentValidation;
using RateLimiter.Writer.DomainService.Models;

namespace RateLimiter.Writer.DomainService.Validators;

public class RateLimitValidator : AbstractValidator<RateLimit>
{
    public RateLimitValidator()
    {
        RuleFor(x => x.Route)
            .NotEmpty().WithMessage("Route cannot be empty.")
            .MaximumLength(256).WithMessage("Route cannot exceed 256 characters.");

        RuleFor(x => x.RequestsPerMinute)
            .GreaterThan(0).WithMessage("RequestsPerMinute must be greater than zero.")
            .LessThanOrEqualTo(10000).WithMessage("RequestsPerMinute cannot exceed 10,000.");
    }
}