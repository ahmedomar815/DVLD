using DVLD.Application.Features.Applications.Commands.ApproveApplication;
using DVLD.Application.Features.Applications.Commands.CancelApplication;
using DVLD.Application.Features.Applications.Commands.RejectApplication;
using DVLD.Application.Features.Applications.Queries.GetApplication;
[Route("[controller]")]
[ApiController]
[Authorize]
public class ApplicationController(ISender sender) : ControllerBase
{
    [HttpGet("{applicationId}")]
    [HasPermission(Permissions.GetApplications)]
    public async Task<IActionResult> Get([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetApplicationQuery(applicationId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<ApplicationDto>()) : result.ToProblem();
    }
    [HttpPost]
    [HasPermission(Permissions.CreateApplications)]
    public async Task<IActionResult> Create([FromBody] CreateApplicationDto request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            request.Adapt<CreateApplicationCommand>(),
            cancellationToken);
        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("{applicationId}/reject")]
    [HasPermission(Permissions.UpdateApplications)]
    public async Task<IActionResult> Reject([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new RejectApplicationCommand(applicationId), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{applicationId}/cancel")]
    [HasPermission(Permissions.UpdateApplications)]
    public async Task<IActionResult> Cancalle([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CancelApplicationCommand(applicationId), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{applicationId}/approve")]
    [HasPermission(Permissions.UpdateApplications)]
    public async Task<IActionResult> Approve([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new ApproveApplicationCommand(applicationId), cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
