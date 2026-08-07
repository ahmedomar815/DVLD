namespace DVLD.Contracts.TestAppointment;

public class TestAppointmentRequestValidator
    : AbstractValidator<TestAppointmentRequest>
{
    public TestAppointmentRequestValidator()
    {
        RuleFor(x => x.AppointmentDate)
            .NotEmpty()
            .Must(date => date > DateTime.Now)
            .WithMessage("Appointment date must be in the future");

        RuleFor(x => x.PaidFees)
            .GreaterThan(0)
            .WithMessage("Paid fees must be greater than zero");

        RuleFor(x => x.TestTypeId)
            .GreaterThan(0)
            .WithMessage("Test type is required");

        RuleFor(x => x.DrivingLicenseApplicationId)
            .NotEmpty()
            .WithMessage("Driving license application id is required");
    }
}
