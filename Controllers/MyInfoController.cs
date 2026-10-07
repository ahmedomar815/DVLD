using Api.Dtos.Authentication.Requests;
using Api.Dtos.Authentication.Responses;
using Application.Features.Authenticaiton.Commands.RefreshTokenCommand;
using Application.Features.Authenticaiton.Commands.ResetPasswordCommand;
using Application.Features.Authenticaiton.Commands.RevokeRefreshTokenCommand;

namespace Api.Controllers;

[ApiController]
[Route("api/my-info")]
[Authorize]
public sealed class MyInfoController(ISender sender) : ControllerBase
{
    
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.Adapt<RefreshTokenCommand>(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<AuthDto>()) : result.ToProblem();
    }

    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke([FromBody] RefreshTokenDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.Adapt<RevokeRefreshTokenCommand>(), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.Adapt<ResetPasswordCommand>(), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
