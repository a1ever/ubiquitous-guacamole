using FluentValidation;
using UserService.DomainService.DTOs;

namespace UserService.DomainService.Validators;

public class UpdateUserRequestDtoValidator : AbstractValidator<UserRequestDto>
{
    public UpdateUserRequestDtoValidator()
    {
        RuleFor(x => x.Id)
            .Null().WithMessage("Id не должен быть указан при обновлении пользователя.");

        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Логин не должен быть пустым");

        RuleFor(x => x.Password)
            .Must(password => password == null || !string.IsNullOrEmpty(password))
            .WithMessage("Пароль не должен быть пустым, если он указан");

        // Поля Name, Surname и Age могут быть null, проверяем только если они заданы
        RuleFor(x => x.Name)
            .Must(name => name == null || !string.IsNullOrEmpty(name))
            .WithMessage("Имя не должно быть пустым, если оно указано");

        RuleFor(x => x.Surname)
            .Must(surname => surname == null || !string.IsNullOrEmpty(surname))
            .WithMessage("Фамилия не должна быть пустой, если она указана");

        RuleFor(x => x.Age)
            .Must(age => !age.HasValue || (age >= 0 && age <= 120))
            .WithMessage("Возраст должен быть от 0 до 120 лет, если указан");
    }
}