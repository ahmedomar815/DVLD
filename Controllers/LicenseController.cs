using DVLD.Contracts.License;
using DVLD.Contracts.LicenseService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

[Route("[controller]")]
[ApiController]
[Authorize]
public class LicenseController(ILicenseService licenseService) : ControllerBase
{
    private readonly ILicenseService _licenseService = licenseService;

    [HttpGet("{licenceId}")]
    [HasPermission(Permissions.GetLicenses)]
    public async Task<IActionResult>Get(string licenceId)
    {
        var result = await _licenseService.GetAyncId(licenceId);
         return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("")]
    [HasPermission(Permissions.CreateLicenses)]
    public async Task<IActionResult> Create([FromBody] LicenseRequest request, CancellationToken cancellationToken)
    {
       
        
        var result = await _licenseService.CreateAsync(request, cancellationToken);
        return result.IsSuccess ? CreatedAtAction(nameof(Get), new { LicenceId = result.Value.LicenseNumber }, result.Value) : result.ToProblem();
    }

    [HttpPut("{licenseNumber}")]
    [HasPermission(Permissions.UpdateLicenses)]
    public async Task<IActionResult> Update([FromRoute] string licenseNumber, [FromBody] LicenseUpdateRequest request, CancellationToken cancellationToken)
    {
       

        var result = await _licenseService.UpdateAsync(licenseNumber, request, cancellationToken);
        return result.IsSuccess ?NoContent() : result.ToProblem();
    }

    [HttpPut("disable/{licenseNumber}")]
    [HasPermission(Permissions.UpdateLicenses)]
    public async Task<IActionResult> ToggleStatus([FromRoute] string licenseNumber, CancellationToken cancellationToken)
    {
        var result = await _licenseService.Disable(licenseNumber, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
