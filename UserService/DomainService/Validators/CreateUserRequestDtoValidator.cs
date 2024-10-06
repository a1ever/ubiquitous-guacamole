using FluentValidation;
using UserService.DomainService.DTOs;

namespace UserService.DomainService.Validators;

public class CreateUserRequestDtoValidator : AbstractValidator<UserRequestDto>
{
    public CreateUserRequestDtoValidator()
    {
        RuleFor(x => x.Id).Null().WithMessage("Id должен быть пустым при создании пользователя.");
        
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Логин не должен быть пустым")
            .MinimumLength(3).WithMessage("Логин должен содержать минимум 3 символа");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль не должен быть пустым")
            .MinimumLength(6).WithMessage("Пароль должен содержать минимум 6 символов");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя не должно быть пустым");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Фамилия не должна быть пустой");

        RuleFor(x => x.Age)
            .NotNull().WithMessage("Возраст не должен быть пустым")
            .InclusiveBetween(0, 120).WithMessage("Возраст должен быть от 0 до 120 лет");
    }
}