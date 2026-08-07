

namespace DVLD.Contracts.DrivingLicenseApplication;

public class DrivingLicenseApplicationRequestValidator:AbstractValidator<DrivingLicenseApplicaitonRequest>

{
    public DrivingLicenseApplicationRequestValidator()
    {
         RuleFor(x=>x.LicenseTypeId).NotNull();
         RuleFor(x=>x.ApplicationId).NotEmpty().NotNull();
    }


}
