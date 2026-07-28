

using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ApplicationController(IApplicationService application) : ControllerBase
{

    private readonly IApplicationService _application = application;

    [HttpGet("{applicaitonId}")]
    public async Task<IActionResult> Get([FromRoute] string applicaitonId, CancellationToken cancellationToken)
    {
     
        var result = await _application.Get(applicaitonId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("{applicationTypeId}")]
    public async Task<IActionResult> Create([FromRoute ] int applicationTypeId ,[FromBody] ApplicationRequest request, CancellationToken cancellationToken)
    {
        var result = await _application.Create(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
    [HttpPut("reject/{applicationId}")]
    public async Task<IActionResult>Reject([FromRoute] string applicationId,CancellationToken cancellationToken)
    {
        var result=await _application.SetRejectedAsync(applicationId, cancellationToken);
        return result.IsSuccess? NoContent() : result.ToProblem();
    }
    [HttpPut("cancale/{applicationId}")]
    public async Task<IActionResult> Cancalle([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await _application.SetCancelledAsync(applicationId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("approve/{applicationId}")]
    public async Task<IActionResult> Approve([FromRoute] string applicationId, CancellationToken cancellationToken)
    {
        var result = await _application.SetApprovedAsync(applicationId, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
