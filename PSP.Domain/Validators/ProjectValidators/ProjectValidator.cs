using FluentValidation;
using PSP.Domain.Aggregates.ProjectAggregate;

namespace PSP.Domain.Validators.ProjectValidators
{

    public class ProjectValidator : AbstractValidator<Project>
    {

        public ProjectValidator()
        {
            RuleFor(p => p.ProjectContent).NotNull().WithMessage("Project content cannot be null")
                .NotEmpty().WithMessage("Project content cannot be empty.");
            RuleFor(p => p.ProjectName).NotNull().WithMessage("Project Name cannot be null")
                .NotEmpty().WithMessage("Project name cannot be empty.");

        }
    }
}