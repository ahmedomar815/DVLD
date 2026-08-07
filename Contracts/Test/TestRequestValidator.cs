namespace DVLD.Contracts.Test;

using FluentValidation;

public record TestResultRequest(string TestAppointmentId, TestResult TestResult, string Notes);

public class TestResultRequestValidator : AbstractValidator<TestResultRequest>
{
    public TestResultRequestValidator()
    {
        RuleFor(x => x.TestAppointmentId)
            .NotEmpty().WithMessage("TestAppointmentId is required.");

        RuleFor(x => x.TestResult)
            .IsInEnum().WithMessage("TestResult must be a valid value.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes must not exceed 500 characters.");
    }

   
}
