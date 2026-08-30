using DVLD.Contracts.User;
using Hangfire;
using Mapster;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;

namespace DVLD.Services;

public class UserInfo(UserManager<ApplicationUser> userManager
    ,ApplicationDbContext context
    ,IHttpContextAccessor httpContextAccessor
    ) : IUserInfoService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    

    public async Task<Result<UserResponse>> GetInfoAsync(string userId, CancellationToken cancellationToken)
    {
        var response = await _context.Users.Where(x=>x.Id == userId).Select
            (x=>new UserResponse(x.Id,x.FirstName,x.SecondName,x.ThirdName,x.FourthName,x.Email!,x.PhoneNumber!,x.Address,x.NationalId,x.Country.Name)).AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        return Result.Success(response);
    }
    public async Task<Result> ChangePasswordAsync(string currentPassword, string newPassword, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext!.User.GetUserId();
        var user = await _userManager.FindByIdAsync(userId!);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        bool checkPassword = await _userManager.CheckPasswordAsync(user, currentPassword);
        if (!checkPassword) return Result.Failure(UserErrors.InvalidCurrentPassword);
       var result= await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        if(result.Succeeded)
        {
            return Result.Success();
        }
        var Error = result.Errors.FirstOrDefault();
        return Result.Failure(new Error(Error!.Code, Error.Description, StatusCodes.Status400BadRequest));
    }
   


}
