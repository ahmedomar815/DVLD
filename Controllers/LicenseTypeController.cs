
using DVLD.Contracts.LicenseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

[Route("[controller]")]
[ApiController]
[Authorize]
public class LicenseTypeController(ILicenseTypeService licenseTypeService) : ControllerBase
{
    private readonly ILicenseTypeService _licenseTypeService = licenseTypeService;


    [HttpGet("{licenseTypeId}")]
    [HasPermission(Permissions.GetLicenseTypes)]
    public async Task<IActionResult> Get([FromRoute, Range(1, int.MaxValue)] int licenseTypeId, CancellationToken cancellationToken)
    {
        var result = await _licenseTypeService.GetAsync(licenseTypeId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    [HasPermission(Permissions.CreateLicenseTypes)]

    public async Task<IActionResult> Create([FromBody] LicenseTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _licenseTypeService.CreateAsync(request, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { licenseTypeId = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpPut("{licenseTypeId}")]
    [HasPermission(Permissions.UpdateLicenseTypes)]

    public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int licenseTypeId, [FromBody] LicenseTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _licenseTypeService.UpdateAsync(licenseTypeId, request, cancellationToken);
        return result.IsSuccess ? NoContent():result.ToProblem();

    }
    [HttpGet("")]
    [HasPermission(Permissions.GetLicenseTypes)]
    public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
    {
        var result = await _licenseTypeService.GetAllAsync( cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
