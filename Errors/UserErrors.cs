using DVLD.Abstractions;

namespace DVLD.Errors;

public  record UserErrors
{
    public static Error InvalidCredentials => new Error("User.InvalidCredentials", "Invalid email or password", StatusCodes.Status401Unauthorized);
    public static Error UserLockedout => new Error("User.UserLockedout", "the user UserLockedout plz contact with admin", StatusCodes.Status423Locked);
    public static Error UserDisabled => new Error("User.UserDisabled", "the user UserDisabled plz contact with admin", StatusCodes.Status401Unauthorized);
    public static Error UserNotFound => new Error("User.UserNotFound", "UserNotFound", StatusCodes.Status400BadRequest);
    public static readonly Error InvalidRefreshToken = new Error("User.InvalidRefreshToken", "Invalid access or refresh token", StatusCodes.Status401Unauthorized);
}

