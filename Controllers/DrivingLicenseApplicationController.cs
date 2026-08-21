using DVLD.Contracts.DrivingLicenseApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("[controller]")]
[ApiController]
[Authorize]
public class DrivingLicenseApplicationController(IDrivingLicenseApplicationService drivingLicenseApplicationService) : ControllerBase
{
    private readonly IDrivingLicenseApplicationService _drivingLicenseApplicationService = drivingLicenseApplicationService;

    [HttpGet("{drivingLicenseApplicationId}")]
    [HasPermission(Permissions.GetDrivingLicenseApplications)]
    public async Task<IActionResult> Get([FromRoute] string drivingLicenseApplicationId, CancellationToken cancellationToken)
    {
        var result = await _drivingLicenseApplicationService.GetAsync(drivingLicenseApplicationId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("")]
    [HasPermission(Permissions.CreateDrivingLicenseApplications)]
    public async Task<IActionResult> Create([FromBody]DrivingLicenseApplicaitonRequest request,CancellationToken cancellationToken)
    {
        var result = await _drivingLicenseApplicationService.CreateAsync(request,cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
