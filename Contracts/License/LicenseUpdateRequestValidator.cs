namespace DVLD.Contracts.License;

public class LicenseUpdateRequestValidator:AbstractValidator<LicenseUpdateRequest>

{
    public LicenseUpdateRequestValidator()
    {
        RuleFor(x => x.LicenseTypeId)
            .GreaterThan(0)
            .WithMessage("LicenseTypeId must be greater than 0.");

        RuleFor(x => x.DriverId)
            .NotEmpty()
            .WithMessage("DriverId is required.");

        RuleFor(x => x.ApplicationId)
            .NotEmpty()
            .WithMessage("ApplicationId is required.");

        RuleFor(x => x.Notes)
            .MaximumLength(500)
            .WithMessage("Notes cannot exceed 500 characters.");

        RuleFor(x => x.PaidFees)
            .GreaterThanOrEqualTo(0)
            .WithMessage("PaidFees cannot be negative.");

        RuleFor(x => x.IssueReason)
            .IsInEnum()
            .WithMessage("Invalid IssueReason.");
    }
}
