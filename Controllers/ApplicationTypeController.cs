
using DVLD.Application.Features.ApplicationTypes.Commands.DeleteApplicationType;
using DVLD.Application.Features.ApplicationTypes.Queries.GetApplicationType;
[Route("[controller]")]
[ApiController]
[Authorize]
public class ApplicationTypeController(ISender sender) : ControllerBase
{
    [HttpGet("{applicationTypeId:int}")]
    [HasPermission(Permissions.GetApplicationTypes)]
    public async Task<IActionResult> Get([FromRoute] int applicationTypeId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetApplicationTypeQuery(applicationTypeId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<ApplicationTypeDto>()) : result.ToProblem();
    }
    [HttpGet]
    [HasPermission(Permissions.GetApplicationTypes)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetApplicationTypesQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<IEnumerable<ApplicationTypeDto>>()) : result.ToProblem();
    }
    [HttpPost]
    [HasPermission(Permissions.CreateApplicationTypes)]
    public async Task<IActionResult> Create([FromBody] CreateApplicationTypeDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(request.Adapt<CreateApplicationTypeCommand>(), cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { applicationTypeId = result.Value.Id }, result.Value.Adapt<ApplicationTypeDto>())
            : result.ToProblem();
    }
    [HttpPut("{applicationTypeId:int}")]
    [HasPermission(Permissions.UpdateApplicationTypes)]
    public async Task<IActionResult> Update(
        [FromRoute] int applicationTypeId,
        [FromBody] UpdateApplicationTypeDto request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateApplicationTypeCommand>() with { ApplicationTypeId = applicationTypeId };
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("{applicationTypeId:int}")]
    [HasPermission(Permissions.DeleteApplicationTypes)]
    public async Task<IActionResult> Delete([FromRoute] int applicationTypeId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new DeleteApplicationTypeCommand(applicationTypeId), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
