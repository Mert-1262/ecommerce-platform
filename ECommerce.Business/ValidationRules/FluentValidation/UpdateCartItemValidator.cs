using ECommerce.Entities.DTOs;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation
{
    public class UpdateCartItemValidator : AbstractValidator<UpdateCartItemDto>
    {
        public UpdateCartItemValidator()
        {
            RuleFor(x => x.Quantity)
                .GreaterThan(0);
        }
    }
}
