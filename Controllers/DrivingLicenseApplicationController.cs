using DVLD.Contracts.DrivingLicenseApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class DrivingLicenseApplicationController(IDrivingLicenseApplicationService drivingLicenseApplicationService) : ControllerBase
{
    private readonly IDrivingLicenseApplicationService _drivingLicenseApplicationService = drivingLicenseApplicationService;

    [HttpGet("{drivingLicenseApplicationId}")]
    public async Task<IActionResult> Get([FromRoute] string drivingLicenseApplicationId, CancellationToken cancellationToken)
    {
        var result = await _drivingLicenseApplicationService.GetAsync(drivingLicenseApplicationId, cancellationToken);
        return result.IsSuccess ? Ok(result) : result.ToProblem();
    }
    [HttpPost("")]
    public async Task<IActionResult> Create([FromBody]DrivingLicenseApplicaitonRequest request,CancellationToken cancellationToken)
    {
        var result = await _drivingLicenseApplicationService.CreateAsync(request,cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
