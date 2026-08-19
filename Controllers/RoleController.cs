using DVLD.Contracts.ApplicationRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class RoleController(IRoleService roleService) : ControllerBase
{
    private readonly IRoleService _roleService = roleService;

    [HttpGet("")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var roles = await _roleService.GetAll(cancellationToken);
        return Ok(roles);
    }

    [HttpGet("{roleId}")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> Get([FromRoute] string roleId, CancellationToken cancellationToken)
    {
        var result = await _roleService.GetRole(roleId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    [HasPermission(Permissions.CreateRoles)]
    public async Task<IActionResult> Create([FromBody] RoleRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.CreateAsync(request, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { roleId = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpPut("{roleId}")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> Update([FromRoute] string roleId, [FromBody] RoleRequest request)
    {
        var result = await _roleService.UpdateAsync(roleId, request);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{roleId}/toggle-status")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> ToggleStatus([FromRoute] string roleId)
    {
        var result = await _roleService.ToggleStatusAsync(roleId);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
