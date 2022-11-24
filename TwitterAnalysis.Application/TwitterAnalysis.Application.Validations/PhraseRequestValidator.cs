using System;
using FluentValidation;
using TwitterAnalysis.Application.Messages.Request;

namespace TwitterAnalysis.Application.Validations
{
    public class PhraseRequestValidator : AbstractValidator<RacistPhraseRequest>
    {
        public PhraseRequestValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().NotNull()
                .WithMessage("This field must not be empty or null");

            RuleFor(x => x.ActiveRacist)
                .NotEmpty().NotNull()
                .WithMessage("this field must not be empty or null");
        }
    }
}
