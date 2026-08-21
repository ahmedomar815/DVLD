using DVLD.Contracts.User;
using Mapster;

namespace DVLD.Services;

public class UserInfo(UserManager<ApplicationUser> userManager,ApplicationDbContext context)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<UserResponse>> GetInfo( string id, CancellationToken cancellationToken)
    {
        var response = await _context.Users.Where(x=>x.Id == id).Select
            (x=>new UserResponse(x.Id,x.FirstName,x.SecondName,x.ThirdName,x.FourthName,x.Email!,x.PhoneNumber!,x.NationalId,x.Country.Name))
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        return Result.Success(response);
    }


}
