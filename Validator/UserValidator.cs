using FluentValidation;
using NextEcommerceWebApi.Models;

namespace NextEcommerceWebApi.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
      public UserValidator() 
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email address is required").EmailAddress().WithMessage("Your email address is not valid");
            RuleFor(e => e.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
