using DVLD.Contracts.Driver;
using DVLD.Contracts.License;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MimeKit.Encodings;

namespace DVLD.Services;

public class DriverService(UserManager<ApplicationUser> userManager, ApplicationDbContext context) :IDriverService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<DriverResponse>>GetAsync(string driverId,CancellationToken cancellationToken)
    {
        if(await GetDriverResponses().FirstOrDefaultAsync(x=>x.Id == driverId, cancellationToken) is not { }  response)
         return Result.Failure<DriverResponse>(DriverErrors.NotFound);

        return Result.Success(response);
    }
    public async Task<Result<DriverResponse>>CreateAsync(DriverRequest request,CancellationToken cancellationToken)
    {
         var IsExist=await _context.Drivers.AnyAsync(x=>x.Id == request.UserId, cancellationToken);
         if(IsExist)
          return Result.Failure<DriverResponse>(DriverErrors.ExistAlready);
          var userIsExist = await _userManager.FindByIdAsync(request.UserId);
         if(userIsExist is null) 
            return Result.Failure<DriverResponse>(UserErrors.UserNotFound);
         var driver=request.Adapt<Driver>();
        await _context.Drivers.AddAsync(driver, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        var response = await GetDriverResponses().FirstAsync(x => x.Id == driver.Id, cancellationToken);
        return Result.Success<DriverResponse>(response);
    }

    private IQueryable<DriverResponse> GetDriverResponses()
    {
        return _context.Drivers.Select(x => new DriverResponse(
            x.Id,
            new ApplicaitonUserResponse(
                x.ApplicationUser.Id,
                x.ApplicationUser.FirstName,
                x.ApplicationUser.SecondName,
                x.ApplicationUser.ThirdName,
                x.ApplicationUser.FourthName,
                x.ApplicationUser.Email!,
                x.ApplicationUser.NationalId
            ),
            x.Licenses.Select(l => new LicneseResponse(
              l.LicenseNumber,
              l.IssueDate,
              l.ExpiryDate,
              l.LicenseType.Name,
              l.IsActive ? "Active" : "Disabled"
            ))
        ));
    }
}
