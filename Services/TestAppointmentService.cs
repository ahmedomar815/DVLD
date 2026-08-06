using DVLD.Contracts.DrivingLicenseApplication;
using DVLD.Contracts.LicenseType;
using DVLD.Contracts.TestAppointment;
using Mapster;

namespace DVLD.Services;

public class TestAppointmentService(ApplicationDbContext context):ITestAppointmentService
{
    private readonly ApplicationDbContext _context = context;


    public async Task<Result<TestAppointmentResponse>> GetAsync(string testAppointmentId, CancellationToken cancellationToken)
    {
        throw new Exception("Not Implemented"); 
        var testAppointment = await _context.TestAppointments.Include(x=>x.TestType).FirstOrDefaultAsync(x=>x.Id==testAppointmentId, cancellationToken);
        if(testAppointment is null)
        {
            return Result.Failure<TestAppointmentResponse>(TestAppointmentErrors.NotFound);
        }
        return Result.Success<TestAppointmentResponse>(testAppointment.Adapt<TestAppointmentResponse>());
    }
    public async Task<Result<TestAppointmentResponse>>CreateAsync(string userId,TestAppointmentRequest  request,CancellationToken cancellationToken)
    {
        var IsTestTypeExist = await _context.TestTypes.AnyAsync(x => x.Id == request.TestTypeId);
        if (!IsTestTypeExist) return Result.Failure<TestAppointmentResponse>(TestTypeErrors.NotFound);

        var IsDrivingLicenseApplicationsExist = await _context.DrivingLicenseApplications.AnyAsync(x => x.Id == request.DrivingLicenseApplicationId);
        if (!IsDrivingLicenseApplicationsExist) return Result.Failure<TestAppointmentResponse >(DrivingLicenseApplicationErros.NotFound);
        var testAppointment = request.Adapt<TestAppointment>();
        testAppointment.CreatedByUserId = userId;
        await  _context.TestAppointments.AddAsync(testAppointment, cancellationToken);
        await _context.SaveChangesAsync();
        var response = await _context.TestAppointments
       .Where(x => x.Id == testAppointment.Id)
       .Select(x => new TestAppointmentResponse(
         x.Id,
         x.AppointmentDate,
         x.PaidFees,
         new TestTypeResponse(
             x.TestType.Id,
             x.TestType.Title,
             x.TestType.Description,
             x.TestType.Fees
         )
       ))
       .SingleAsync(cancellationToken);

        return Result.Success(response);
    }
}
