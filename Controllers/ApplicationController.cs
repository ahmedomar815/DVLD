

using MailKit;
using DVLD.Authentication.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DVLD.Abstractions;
using DVLD.Abstractions.Consts;


[Route("[controller]")]
[ApiController]
[Authorize]
public class ApplicationController(IApplicationService application) : ControllerBase
{

    private readonly IApplicationService _application = application;

    [HttpGet("{applicationId}")]
    [HasPermission(Permissions.GetApplications)]
    public async Task<IActionResult> Get([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
     
        var result = await _application.GetAsync(applicationId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost]
    [HasPermission(Permissions.CreateApplications)]
    public async Task<IActionResult> Create([FromBody] ApplicationRequest request, CancellationToken cancellationToken)
    {
        var result = await _application.CreateAsync(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("{applicationId}/reject")]
    [HasPermission(Permissions.UpdateApplications)]
    public async Task<IActionResult>Reject([FromRoute] string applicationId,CancellationToken cancellationToken)
    {
        var result=await _application.SetRejectedAsync(applicationId, cancellationToken);
        return result.IsSuccess? NoContent() : result.ToProblem();
    }
    [HttpPut("{applicationId}/cancel")]
    [HasPermission(Permissions.UpdateApplications)]
    public async Task<IActionResult> Cancalle([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await _application.SetCancelledAsync(applicationId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("{applicationId}/approve")]
    [HasPermission(Permissions.UpdateApplications)]
    public async Task<IActionResult> Approve([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await _application.SetApprovedAsync(applicationId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
