namespace DVLD.Contracts.Driver;

public class DriverRequestValidator: AbstractValidator<DriverRequest>
{
    public DriverRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}
