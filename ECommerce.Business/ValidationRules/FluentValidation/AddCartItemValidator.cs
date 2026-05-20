using ECommerce.Entities.DTOs;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation
{
    public class AddCartItemValidator : AbstractValidator<AddCartItemDto>
    {
        public AddCartItemValidator()
        {
            RuleFor(x => x.ProductId)
                .GreaterThan(0);

            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
