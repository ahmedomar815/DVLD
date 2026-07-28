


namespace DVLD.Contracts.ApplicationType;

using DVLD.Contracts.LicenseType;


public class LicenseTypeRequestValidator : AbstractValidator<LicenseTypeRequest>

{
    public LicenseTypeRequestValidator()
    {
        RuleFor(x => x.Name)
            
            .NotEmpty()
            .NotNull()
            .Length(15, 30);
        RuleFor(x => x.Description)
            .NotEmpty()
            .NotNull()
            .Length(30, 100);
        RuleFor(x => x.DefaultVaildityLength)
     .GreaterThan(0); 
        RuleFor(x => x.DefaultVaildityLength)
            .NotEmpty();
        RuleFor(x => x.fees)
       .GreaterThan(0);
    }


}
