
using DVLD.Contracts.Authentication;
using DVLD.Services;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IAuthServices authServices) : ControllerBase
{
    private readonly IAuthServices _authServices = authServices;

    [HttpPost("login")]
    public  async Task <IActionResult> Login ([FromBody] LoginRequest request , CancellationToken cancellationToken)
    {
        var result =  await _authServices.GetTokenAsync(request.Email, request.Password, cancellationToken);
        return result.IsSuccess? Ok(result.Value):result.ToProblem();
    }
    [HttpPost("get-refresh-token")]
    public async Task<IActionResult> GetRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var Result = await _authServices.GetRefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken);
        return Result.IsSuccess ? Ok(Result.Value) : Result.ToProblem();
    }
    [HttpPut("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var Result = await _authServices.RevokeRefreshTokensync(request.Token, request.RefreshToken, cancellationToken);
        return Result.IsSuccess ? Ok() : Result.ToProblem();
    }
}
