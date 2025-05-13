using FluentValidation;
using DemoProject.Dto;

namespace DemoProject.Validators;

public class CreateCategoryValidator : AbstractValidator<CategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.").NotNull()
            .MaximumLength(20).WithMessage("Title can't be longer than 20 characters");
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.").NotNull()
            .MaximumLength(10).WithMessage("Code can't be longer than 10 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.").NotNull()
            .MaximumLength(100).WithMessage("Description can't be longer than 100 characters.");
    }
}