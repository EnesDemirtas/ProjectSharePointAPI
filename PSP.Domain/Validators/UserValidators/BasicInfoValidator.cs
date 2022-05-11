﻿using FluentValidation;
using PSP.Domain.Aggregates.UserAggregate;

namespace PSP.Domain.Validators.UserValidators {

    public class BasicInfoValidator : AbstractValidator<BasicInfo> {

        public BasicInfoValidator() {
            RuleFor(info => info.FirstName)
                .NotNull().WithMessage("First name is required. It is currently null")
                .MinimumLength(3).WithMessage("First name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("First name can contains at most 50 characters.");

            RuleFor(info => info.LastName)
                .NotNull().WithMessage("Last name is required. It is currently null")
                .MinimumLength(3).WithMessage("Last name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Last name can contains at most 50 characters.");

            RuleFor(info => info.EmailAddress).NotNull().WithMessage("Email address is required.").EmailAddress()
                .WithMessage("Provided email address is not valid.");

            RuleFor(info => info.UserName).NotNull().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Username can contains at most 50 characters.");
        }
    }
}