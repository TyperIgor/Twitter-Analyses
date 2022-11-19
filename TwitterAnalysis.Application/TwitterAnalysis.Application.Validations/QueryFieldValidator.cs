using FluentValidation;
using TwitterAnalysis.Application.Messages.Request;

namespace TwitterAnalysis.Application.Validations
{
    public class QueryFieldValidator : AbstractValidator<QueryRequest>
    {
        public QueryFieldValidator()
        {
            RuleFor(q => q.Query)
                .NotEmpty()
                    .WithMessage("Field query must not be null or empty")
                .MaximumLength(100)
                    .WithMessage("Field query must be less than 50 ")
                .MinimumLength(5)
                    .WithMessage("Field query must have 5 legth at minimum");
        }

    }
}
