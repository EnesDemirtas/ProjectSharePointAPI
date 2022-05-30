using FluentValidation;
using PSP.Domain.Aggregates.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Domain.Validators.CategoryValidators
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(category => category.CategoryName)
                .NotNull().WithMessage("Category name is required. It is currently null")
                .MinimumLength(2).WithMessage("Category name must be at least 2 characters long.")
                .MaximumLength(50).WithMessage("Category name can contains at most 50 characters.");
            
            RuleFor(category => category.CategoryDescription)
                .NotNull().WithMessage("Category description is required. It is currently null")
                .MinimumLength(2).WithMessage("Category description must be at least 2 characters long.")
                .MaximumLength(500).WithMessage("Category description can contains at most 500 characters.");

        }
    }
}
