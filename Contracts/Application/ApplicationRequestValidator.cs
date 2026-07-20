using DVLD.Contracts.Application;
using FluentValidation;

namespace DVLD.Contracts.ApplicationType;

public class ApplicationRequestValidator : AbstractValidator<ApplicationRequest>

{
    public ApplicationRequestValidator()
    {
        RuleFor(x => x.ApplicationTypeId)
            .GreaterThan(0)
            .WithMessage("ApplicationTypeId must be greater than zero.")
            .NotEmpty();
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserID is required.");
        

    }
}
