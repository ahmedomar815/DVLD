namespace DVLD.Contracts.ApplicationRole;

public class RoleRequestValidator : AbstractValidator<RoleRequest>
{
    public RoleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Permissions)
            .NotNull()
            .Must(x => x.Any())
            .WithMessage("At least one claim is required.");

        RuleForEach(x => x.Permissions)
            .NotEmpty()
            .MaximumLength(100);
    }
}