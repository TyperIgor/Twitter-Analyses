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
                    .WithMessage("Field Name must not be null or empty")
                .MaximumLength(100)
                    .WithMessage("Field Name must be less than 50 ")
                .MinimumLength(5)
                    .WithMessage("Field Name must have 5 legth at minimum");
        }

    }
}
