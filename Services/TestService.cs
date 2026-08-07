using DVLD.Contracts.Test;
using DVLD.Contracts.TestAppointment;
using Mapster;
using System.Reflection.Metadata.Ecma335;

namespace DVLD.Services;

public class TestService(ApplicationDbContext context ): ITestService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<TestResponse>> CreateAsync(string userId, TestRequest request,CancellationToken cancellationToken)
    {
        var testAppointment = await _context.TestAppointments
     .FirstOrDefaultAsync(x => x.Id == request.TestAppointmentId, cancellationToken);

        if (testAppointment is null)
            return Result.Failure<TestResponse>(TestAppointmentErrors.NotFound);

        var test = request.Adapt<Test>();
        test.CreatedByUserId= userId;
        await _context.Tests.AddAsync(test, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = test.Adapt<TestResponse>() with
        {
            TestAppointmentResponse = testAppointment.Adapt<TestAppointmentResponse>()
        };

        return Result.Success(response);

    }
}
