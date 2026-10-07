using Api.Dtos.Roles.Requests;
using Api.Dtos.Roles.Responses;
using Application.Features.Roles.Commands.CreateRole;
using Application.Features.Roles.Commands.ToggleRoleStatus;
using Application.Features.Roles.Commands.UpdateRole;
using Application.Features.Roles.Queries.GetRole;
using Application.Features.Roles.Queries.GetRoles;


namespace Api.Controllers;

[ApiController]
[Route("api/roles")]
[Authorize]
public sealed class RoleController(ISender sender) : ControllerBase
{
    [HttpGet]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var roles = await sender.Send(new GetRolesQuery(), cancellationToken);
        return Ok(roles.Adapt<IEnumerable<RoleResponseDto>>());
    }

    [HttpGet("{roleId}")]
    [HasPermission(Permissions.GetRoles)]
    public async Task<IActionResult> Get([FromRoute] string roleId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetRoleQuery(roleId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<RoleDetailsDto>()) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.CreateRoles)]
    public async Task<IActionResult> Create([FromBody] RoleDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.Adapt<CreateRoleCommand>(), cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { roleId = result.Value.Id }, result.Value.Adapt<RoleDetailsDto>())
            : result.ToProblem();
    }

    [HttpPut("{roleId}")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> Update(
        [FromRoute] string roleId,
        [FromBody] RoleDto request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateRoleCommand>() with { RoleId = roleId };
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("{roleId}/toggle-status")]
    [HasPermission(Permissions.UpdateRoles)]
    public async Task<IActionResult> ToggleStatus([FromRoute] string roleId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ToggleRoleStatusCommand(roleId), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
