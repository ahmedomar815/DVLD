using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DVLD.Abstractions;

public static class ResultExtesions
{
    public static ObjectResult ToProblem (this Result result)
    {
        if(result.IsSuccess)
            throw new InvalidOperationException("Cannot convert success result to a problem");
        var error = result.Error;

        var problemDetails = new ProblemDetails
        {
            Status = error.StatusCode,
            Title = error.Code,
            Detail = error.Description
            ,Extensions= new Dictionary<string, object>
            {
                 {
                    "errors",new[]
                    {
                        result.Error.Code,
                        result.Error.Description
                    }
                }
            }!
        };

        return new ObjectResult(problemDetails);
    }
}
