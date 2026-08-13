namespace DVLD.Contracts.LicenseService;

public class LicenseRequestValidator : AbstractValidator<LicenseRequest>
{
    
  
    
        public LicenseRequestValidator()
        {
            RuleFor(x => x.LicenseNumber).NotEmpty().WithMessage("LicenseNumber is required.");
            RuleFor(x => x.ApplicationId).NotEmpty().WithMessage("ApplicationId is required.");
            RuleFor(x => x.LicenseTypeId).GreaterThan(0).WithMessage("LicenseTypeId must be greater than 0.");
     
            RuleFor(x => x.Notes).MaximumLength(50).WithMessage("Notes cannot exceed 50 characters.");
            RuleFor(x => x.DriverId).NotEmpty().WithMessage("DriverId is required.");
            RuleFor(x => x.PaidFees).GreaterThanOrEqualTo(0).WithMessage("PaidFees must be non-negative");
            RuleFor(x => x.IssueReason).IsInEnum().WithMessage("IssueReason must be a valid enum value.");
        }
    

}
