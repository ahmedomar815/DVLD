using FluentValidation;

namespace DVLD.Contracts.ApplicationType;

public class ApplicationTypeRequestValidator : AbstractValidator<ApplicationTypeRequest>

{
    public ApplicationTypeRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Fees).GreaterThan(0).WithMessage("Fees must be greater than zero.");
    }
}
