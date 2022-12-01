using FluentValidation;
using TwitterAnalysis.Application.Messages.Request.Auth;

namespace TwitterAnalysis.Application.Validations
{
    public class AuthValidator : AbstractValidator<LoginAuthRequest>
    {
        public AuthValidator()
        {
            RuleFor(x => x.Name).NotEmpty()
                .NotNull()
                .WithMessage("this field cannot be empty or null");

            RuleFor(x => x.Secret).NotEmpty()
                .NotNull()
                .WithMessage("this field cannot be emtpy or null");
        }
    }
}
