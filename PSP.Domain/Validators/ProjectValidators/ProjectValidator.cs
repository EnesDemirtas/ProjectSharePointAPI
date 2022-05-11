using FluentValidation;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Domain.Validators.ProjectValidators {

    public class ProjectValidator : AbstractValidator<Project> {

        public ProjectValidator() {
            RuleFor(p => p.ProjectContent).NotNull().WithMessage("Project content cannot be null")
                .NotEmpty().WithMessage("Project contnet cannot be empty.");
        }
    }
}