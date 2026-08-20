using DVLD.Abstractions.Consts;

namespace DVLD.Contracts.User;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.SecondName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.ThirdName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.FourthName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(100);

        RuleFor(x => x.Phone)
            .NotEmpty()
            .Matches(@"^01[0125][0-9]{8}$")
            .WithMessage("Invalid Egyptian phone number.");

        RuleFor(x => x.NationalId)
            .NotEmpty()
            .Length(14)
            .Matches(@"^\d{14}$")
            .WithMessage("National ID must contain exactly 14 digits.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(100)
            .Matches(RegexPatterns.Password)
            .WithMessage("Password must be at least 8 characters and contain uppercase, lowercase, number, and special character.");
    }
}
