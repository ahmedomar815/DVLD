public static class ResultExtensions
{
    public static ObjectResult ToProblem(this Result result)
    {
        if (result.IsSuccess)
            throw new InvalidOperationException(
                "Cannot convert success result to a problem");

        var problemDetails = new ProblemDetails
        {
            Status = result.Error.ToStatusCode(),
            Title = "An error occurred",
            Detail = result.Error.Description
        };

        problemDetails.Extensions["errors"] = new[]
        {
            new
            {
                code = result.Error.Code,
                description = result.Error.Description
            }
        };

        return new ObjectResult(problemDetails)
        {
            StatusCode = problemDetails.Status
        };
    }

    private static int ToStatusCode(this Error error) => error.Code switch
    {
        "User.InvalidCredentials"
            or "User.UserDisabled"
            or "User.InvalidRefreshToken"
            => StatusCodes.Status401Unauthorized,

        "User.UserLockedout"
            => StatusCodes.Status423Locked,

        _ when error.Code.EndsWith(
            ".NotFound",
            StringComparison.Ordinal)
            => StatusCodes.Status404NotFound,

        _ when error.Code.Contains(
            "Duplicate",
            StringComparison.OrdinalIgnoreCase)
            || error.Code.Contains(
                "Already",
                StringComparison.OrdinalIgnoreCase)
            || error.Code.Contains(
                "Used",
                StringComparison.OrdinalIgnoreCase)
            => StatusCodes.Status409Conflict,

        _ => StatusCodes.Status400BadRequest
    };
}