using DVLD.Properties.Abstractions;
using DVLD.Properties.Abstractions.Consts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
public class DriverController(IDriverService driverService) : ControllerBase
{
    private readonly IDriverService _driverService = driverService;

    [HttpGet("{driverId}")]
    [HasPermission(Permissions.GetDrivers)]
    public async Task<IActionResult> Get(string driverId, CancellationToken cancellationToken)
    {
        var result = await _driverService.GetAsync(driverId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("")]
    [HasPermission(Permissions.CreateDrivers)]
    public async Task<IActionResult> Create([FromBody] DVLD.Contracts.Driver.DriverRequest request, CancellationToken cancellationToken)
    {
        var result = await _driverService.CreateAsync(request, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { driverId = result.Value.Id }, result.Value)
            : result.ToProblem();
    }


}
