using Domain.Dto.Create;
using FluentValidation;

public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Product name is required.");
        RuleFor(x => x.Description).NotEmpty().NotNull().WithMessage("Description is required.");
        RuleFor(x => x.Price).InclusiveBetween(1, 10000).WithMessage("Price must be between 1 and 10000.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0.");
    }
}

public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
{
    public CreateOrderDtoValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id is required.");
        RuleFor(x => x.ProductIdQuantity).NotEmpty().WithMessage("Product Id Quantity is required.");
        RuleFor(x => x.ProductIdQuantity).Must(x => x.Count > 0).WithMessage("Product Id Quantity must contain at least one product.");
        RuleFor(x => x)
            .Must(x => x.ProductIdQuantity.Values.All(y => y > 0)).WithMessage("Quantity must be greater than 0.");
    }
}