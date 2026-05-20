using ECommerce.Entities.DTOs;
using FluentValidation;

namespace ECommerce.Business.ValidationRules.FluentValidation
{
    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}