using DVLD.Contracts.LicenseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class LicenseController(ILicenseService licenseService) : ControllerBase
{
    private readonly ILicenseService _licenseService = licenseService;

    [HttpGet("{licenceId}")]
    public async Task<IActionResult>Get(string licenceId)
    {
        var result = await _licenseService.GetAyncId(licenceId);
         return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    public async Task<IActionResult> Create([FromBody] LicenseRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        
        var result = await _licenseService.CreateAsync(userId!, request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { LicenceId = result.Value.LicenseNumber }, result.Value) : result.ToProblem();
    }
}
