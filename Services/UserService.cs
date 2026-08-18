using DVLD.Contracts.User;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;

namespace DVLD.Services;

public class UserService(
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext context)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> CreateAsync( UserRequest request, CancellationToken cancellationToken)
    {
        if (await _context.Users
            .AnyAsync(x => x.NationalId == request.NationalId, cancellationToken))
        {
            return Result.Failure(UserErrors.NationalIdAlreadyExists);
        }

        if (await _userManager.FindByEmailAsync(request.Email) is not null)
        {
            return Result.Failure(UserErrors.UserAlreadyExists);
        }

        var user = request.Adapt<ApplicationUser>();

        var result = await _userManager.CreateAsync(
            user,
            request.Password);

        if (!result.Succeeded)
        {
            var error = result.Errors.First();

            return Result.Failure(
                new Error(
                    error.Code,
                    error.Description,
                    StatusCodes.Status400BadRequest));
        }

        return Result.Success();
    }
    public async Task<Result<UserResponse>> GetAsync(  string userId , CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        var response = user.Adapt<UserResponse>();

        return Result.Success(response);
    }

    public async Task<Result<UserResponse>> UpdateAsync(string userId, UserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return Result.Failure<UserResponse>(UserErrors.UserNotFound);

        if (await _context.Users.AnyAsync(
                x => x.NationalId == request.NationalId &&
                     x.Id != userId,
                cancellationToken))
        {
            return Result.Failure<UserResponse>(UserErrors.NationalIdAlreadyExists);
        }

        if (await _userManager.FindByEmailAsync(request.Email) is { } existingUser
            && existingUser.Id != userId)
        {
            return Result.Failure<UserResponse>(UserErrors.UserAlreadyExists);
        }
        user = request.Adapt<ApplicationUser>();
        var result = await _userManager.UpdateAsync(user);

        if(!result.Succeeded)
        {
            var error = result.Errors.First();
            return Result.Failure<UserResponse>(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        return Result.Success(user.Adapt<UserResponse>());


    }
}


