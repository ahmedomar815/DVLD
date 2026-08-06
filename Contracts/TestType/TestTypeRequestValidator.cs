using DVLD.Contracts.TestType;

namespace DVLD.Contracts.LicenseType;

public class TestTypeRequestValidator : AbstractValidator<TestTypeRequest>
{
    public TestTypeRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.Fees)
            .GreaterThan(0).WithMessage("Fees must be greater than 0.");
    }
}
