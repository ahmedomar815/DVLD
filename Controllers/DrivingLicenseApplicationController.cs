using DVLD.Application.Features.DrivingLicenseApplications.Commands.CreateDrivingLicenseApplication;
using DVLD.Application.Features.DrivingLicenseApplications.Queries.GetDrivingLicenseApplication;
[Route("[controller]")]
[ApiController]
[Authorize]
public class DrivingLicenseApplicationController(ISender sender) : ControllerBase
{
    [HttpGet("{drivingLicenseApplicationId}")]
    [HasPermission(Permissions.GetDrivingLicenseApplications)]
    public async Task<IActionResult> Get([FromRoute] string drivingLicenseApplicationId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetDrivingLicenseApplicationQuery(drivingLicenseApplicationId),
            cancellationToken);
        return result.IsSuccess
            ? Ok(result.Value.Adapt<DrivingLicenseApplicationDto>())
            : result.ToProblem();
    }
    [HttpPost("")]
    [HasPermission(Permissions.CreateDrivingLicenseApplications)]
    public async Task<IActionResult> Create([FromBody] CreateDrivingLicenseApplicationDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            request.Adapt<CreateDrivingLicenseApplicationCommand>(),
            cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
