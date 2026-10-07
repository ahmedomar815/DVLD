
using Application.Features.License.Commands.Renew;
using Application.Features.License.Commands.ToggleLicenseStatus;
using DVLD.Features.Licenses.Queries;
[Route("[controller]")]
[ApiController]
[Authorize]
public class LicenseController(ISender sender) : ControllerBase
{
    [HttpGet("{licenceId}")]
    [HasPermission(Permissions.GetLicenses)]
    public async Task<IActionResult> Get(string licenceId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetLicenseQuery(licenceId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<LicenseDto>()) : result.ToProblem();
    }
    [HttpPost("")]
    [HasPermission(Permissions.CreateLicenses)]
    public async Task<IActionResult> Create([FromBody] CreateLicenseDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.Adapt<CreateLicenseCommand>(), cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { licenceId = result.Value.LicenseNumber }, result.Value.Adapt<LicenseDto>())
            : result.ToProblem();
    }
    [HttpPut("{licenseNumber}")]
    [HasPermission(Permissions.UpdateLicenses)]
    public async Task<IActionResult> Update([FromRoute] string licenseNumber, [FromBody] UpdateLicenseDto request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateLicenseCommand>() with { LicenseNumber = licenseNumber };
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{licenseNumber}/disable")]
    [HasPermission(Permissions.UpdateLicenses)]
    public async Task<IActionResult> ToggleStatus([FromRoute] string licenseNumber, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ToggleLicenseStatusCommand(licenseNumber), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{licenseNumber}/renew")]
    [HasPermission(Permissions.UpdateLicenses)]
    public async Task<IActionResult> Renew([FromRoute] string licenseNumber, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RenewLicenseCommand(licenseNumber), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<LicenseDto>()) : result.ToProblem();
    }
}
