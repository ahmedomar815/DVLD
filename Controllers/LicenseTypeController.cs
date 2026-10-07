using Api.Dtos.LicenseTypes.Requests;
using DVLD.Application.Features.LicenseTypes.Commands.CreateLicenseType;
using DVLD.Application.Features.LicenseTypes.Commands.UpdateLicenseType;
using DVLD.Application.Features.LicenseTypes.Queries.GetLicenseType;
using DVLD.Application.Features.LicenseTypes.Queries.GetLicenseTypes;
[Route("[controller]")]
[ApiController]
[Authorize]
public class LicenseTypeController(ISender sender) : ControllerBase
{
    [HttpGet("{licenseTypeId}")]
    [HasPermission(Permissions.GetLicenseTypes)]
    public async Task<IActionResult> Get([FromRoute, Range(1, int.MaxValue)] int licenseTypeId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetLicenseTypeQuery(licenseTypeId), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value.Adapt<Api.Dtos.LicenseTypes.Responses.LicenseTypeDto>())
            : result.ToProblem();
    }
    [HttpPost("")]
    [HasPermission(Permissions.CreateLicenseTypes)]
    public async Task<IActionResult> Create([FromBody] LicenseTypeDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            request.Adapt<CreateLicenseTypeCommand>(),
            cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(
                nameof(Get),
                new { licenseTypeId = result.Value.Id },
                result.Value.Adapt<Api.Dtos.LicenseTypes.Responses.LicenseTypeDto>())
            : result.ToProblem();
    }
    [HttpPut("{licenseTypeId}")]
    [HasPermission(Permissions.UpdateLicenseTypes)]
    public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int licenseTypeId, [FromBody] LicenseTypeDto request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateLicenseTypeCommand>() with { LicenseTypeId = licenseTypeId };
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent():result.ToProblem();
    }
    [HttpGet("")]
    [HasPermission(Permissions.GetLicenseTypes)]
    public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetLicenseTypesQuery(), cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value.Adapt<IEnumerable<Api.Dtos.LicenseTypes.Responses.LicenseTypeDto>>())
            : result.ToProblem();
    }
}
