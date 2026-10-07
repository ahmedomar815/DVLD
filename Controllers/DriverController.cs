
using DVLD.Application.Features.Drivers.Queries.GetDriver;
[Route("[controller]")]
[ApiController]
[Authorize]
public class DriverController(ISender sender) : ControllerBase
{
    [HttpGet("{driverId}")]
    [HasPermission(Permissions.GetDrivers)]
    public async Task<IActionResult> Get(string driverId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetDriverQuery(driverId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<DriverDto>()) : result.ToProblem();
    }
    [HttpPost]
    [HasPermission(Permissions.CreateDrivers)]
    public async Task<IActionResult> Create([FromBody] CreateDriverDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.Adapt<CreateDriverCommand>(), cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { driverId = result.Value.Id }, result.Value.Adapt<DriverDto>())
            : result.ToProblem();
    }
}
