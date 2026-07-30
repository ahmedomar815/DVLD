
using DVLD.Contracts.LicenseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class LicenseTypeController(ILicenseTypeService licenseTypeService) : ControllerBase
{
    private readonly ILicenseTypeService _licenseTypeService = licenseTypeService;


    [HttpGet("{licenseTypeId}")]
    public async Task<IActionResult> Get([FromRoute, Range(1, int.MaxValue)] int licenseTypeId, CancellationToken cancellationToken)
    {
        var result = await _licenseTypeService.GetAsync(licenseTypeId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]

    public async Task<IActionResult> Create([FromBody] LicenseTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _licenseTypeService.CreateAsync(request, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { licenseTypeId = result.Value.Id }, result.Value)
            : result.ToProblem();
    }

    [HttpPut("{licenseTypeId}")]

    public async Task<IActionResult> Update([FromRoute, Range(1, int.MaxValue)] int licenseTypeId, [FromBody] LicenseTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _licenseTypeService.UpdateAsync(licenseTypeId, request, cancellationToken);
        return result.IsSuccess ? NoContent():result.ToProblem();

    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll( CancellationToken cancellationToken)
    {
        var result = await _licenseTypeService.GetAllAsync( cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
