using DVLD.Abstractions;

namespace DVLD.Errors;

public  record UserErrors
{
    public static Error InvalidCredentials => new Error("User.InvalidCredentials", "Invalid email or password", StatusCodes.Status401Unauthorized);
    public static Error UserNotFound => new Error("User.UserNotFound", "UserNotFound", StatusCodes.Status400BadRequest);

}

