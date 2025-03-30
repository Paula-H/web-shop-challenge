using Domain.Entity;
using Domain.Enum;
using FluentValidation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
        RuleFor(x => x.Price).InclusiveBetween(1, 10000).WithMessage("Price must be between 1 and 10000.");
        RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock must be greater than or equal to 0.");
    }
}

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required.");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}

public class CouponValidator : AbstractValidator<Coupon>
{
    public CouponValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Coupon title is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Coupon description is required.");
        RuleFor(x => x.Code).NotEmpty().WithMessage("Coupon code is required.");
        RuleFor(x => x)
            .Must(model =>
                (model.Type == CouponType.Rate && model.DiscountRate != null) ||
                (model.Type == CouponType.Amount && model.DiscountAmount != null)
            );
    }
}

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User id is required.");
        RuleFor(x => x.TotalPrice).InclusiveBetween(1, 10000).WithMessage("Total price must be between 1 and 10000.");
        RuleFor(x => x.Status).IsInEnum().WithMessage("Status is not valid.");
        RuleFor(x => x.Deduction).GreaterThanOrEqualTo(0).WithMessage("Deduction must be greater than or equal to 0.");
    }
}