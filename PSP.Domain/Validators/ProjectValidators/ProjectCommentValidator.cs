using FluentValidation;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Domain.Validators.ProjectValidators
{

    public class ProjectCommentValidator : AbstractValidator<ProjectComment>
    {

        public ProjectCommentValidator()
        {
            RuleFor(pc => pc.Text).NotNull().WithMessage("Comment text cannot be null")
                .NotEmpty().WithMessage("Comment text cannot be empty.")
                .MinimumLength(1).WithMessage("Comment text must be at lest 1 character long.")
                .MaximumLength(1000).WithMessage("Commen text can contains at most 1000 characters.");
        }
    }
}