using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using DVLD.Contracts.User;
using DVLD.Extensions;
using DVLD.Services;
using DVLD.Abstractions;

namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class MyInfoController(IUserInfoService userInfoService) : ControllerBase
{
    private readonly IUserInfoService _userInfoService = userInfoService;

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var result = await _userInfoService.GetInfoAsync(userId!, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _userInfoService.ChangePasswordAsync(
            request.CurrentPassword,
            request.NewPassword,
            cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
