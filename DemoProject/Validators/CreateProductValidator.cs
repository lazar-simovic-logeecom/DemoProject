using DemoProject.Dto;
using FluentValidation;

namespace DemoProject.Validators;

public class CreateProductValidator : AbstractValidator<ProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.").NotNull()
            .MaximumLength(20).WithMessage("Title can't be longer than 20 characters");
        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("Sku is required.").NotNull()
            .MaximumLength(10).WithMessage("Sku can't be longer than 10 characters.");
        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Brand is required.").NotNull()
            .MaximumLength(20).WithMessage("Brand can't be longer than 20 characters.");
        RuleFor(x => x.Price)
            .NotEmpty().WithMessage("Price is required.").NotNull();
    }
}