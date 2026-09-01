using DVLD.Properties.Abstractions.Consts;

namespace DVLD.Contracts.User;

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(RegexPatterns.Password)
            .WithMessage("Password must be at least 8 characters and contain uppercase, lowercase, number, and special character.");
    }
}
