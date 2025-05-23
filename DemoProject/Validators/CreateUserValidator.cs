using DemoProject.Dto;
using FluentValidation;

namespace DemoProject.Validators;

public class CreateUserValidator : AbstractValidator<UserDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Title is required.").NotNull();
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.").NotNull();
    }
}