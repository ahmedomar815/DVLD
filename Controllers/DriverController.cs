using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
public class DriverController(IDriverService driverService) : ControllerBase
{
    private readonly IDriverService _driverService = driverService;

    [HttpGet("{driverId}")]
    public async Task<IActionResult> Get(string driverId, CancellationToken cancellationToken)
    {
        var result = await _driverService.GetAsync(driverId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("")]
    public async Task<IActionResult> Create(string driverId, CancellationToken cancellationToken)
    {
        var result = await _driverService.GetAsync(driverId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }


}
