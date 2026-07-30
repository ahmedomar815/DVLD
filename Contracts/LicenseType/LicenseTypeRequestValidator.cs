namespace DVLD.Contracts.LicenseType;

public class LicenseTypeRequestValidator : AbstractValidator<LicenseTypeRequest>
{
    public LicenseTypeRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(15, 30);

        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(30, 100);

        RuleFor(x => x.DefaultValidityLength)
            .GreaterThan(0);

        RuleFor(x => x.MinimumAllowedAge)
            .GreaterThanOrEqualTo(18);

        RuleFor(x => x.Fees)
            .GreaterThan(0);
    }
}
