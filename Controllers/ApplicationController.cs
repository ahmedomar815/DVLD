

using MailKit;
using DVLD.Abstractions.Consts;
using DVLD.Authentication.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("[controller]")]
[ApiController]
[Authorize]
public class ApplicationController(IApplicationService application) : ControllerBase
{

    private readonly IApplicationService _application = application;

    [HttpGet("{applicaitonId}")]
    [HasPermission(Permissions.GetApplications)]
    public async Task<IActionResult> Get([FromRoute] string applicaitonId, CancellationToken cancellationToken)
    {
     
        var result = await _application.Get(applicaitonId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("{applicationTypeId}")]
    [HasPermission(Permissions.CreateApplications)]
    public async Task<IActionResult> Create([FromRoute ] int applicationTypeId ,[FromBody] ApplicationRequest request, CancellationToken cancellationToken)
    {
        var result = await _application.Create(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("reject/{applicationId}")]
    [HasPermission(Permissions.UpdateApplications)]
    public async Task<IActionResult>Reject([FromRoute] string applicationId,CancellationToken cancellationToken)
    {
        var result=await _application.SetRejectedAsync(applicationId, cancellationToken);
        return result.IsSuccess? NoContent() : result.ToProblem();
    }
    [HttpPut("cancale/{applicationId}")]
    [HasPermission(Permissions.UpdateApplications)]
    public async Task<IActionResult> Cancalle([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await _application.SetCancelledAsync(applicationId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("approve/{applicationId}")]
    [HasPermission(Permissions.UpdateApplications)]
    public async Task<IActionResult> Approve([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await _application.SetApprovedAsync(applicationId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
