
using Api.Dtos.Authentication.Requests;
using Api.Dtos.Authentication.Responses;
using Application.Features.Authenticaiton.Commands.ForgotPasswordCommand;
using Application.Features.Authenticaiton.Commands.LoginCommand;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthenticationController(ISender sender) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.Adapt<LoginCommand>(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<AuthDto>()) : result.ToProblem();
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.Adapt<ForgotPasswordCommand>(), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
