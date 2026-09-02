using DVLD.Abstractions;
using DVLD.Abstractions.Consts;
using DVLD.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet("{userId}")]
    [HasPermission(Permissions.GetUsers)]
    public async Task<IActionResult> Get([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var result = await _userService.GetAsync(userId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.CreateUsers)]
    public async Task<IActionResult> Create([FromBody] UserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.CreateAsync(request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{userId}")]
    [HasPermission(Permissions.UpdateUsers)]
    public async Task<IActionResult> Update([FromRoute] string userId, [FromBody] UserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateAsync(userId, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{userId}/unlock")]
    [HasPermission(Permissions.UpdateUsers)]
    public async Task<IActionResult> Unlock([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var result = await _userService.UnlockUserAsync(userId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{userId}/toggle-status")]
    [HasPermission(Permissions.UpdateUsers)]
    public async Task<IActionResult> ToggleStatus([FromRoute] string userId, CancellationToken cancellationToken)
    {
        var result = await _userService.ToggleStatusAsync(userId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
