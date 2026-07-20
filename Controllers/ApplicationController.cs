

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ApplicationController(IApplicationService application) : ControllerBase
{

    private readonly IApplicationService _application = application;

    [HttpGet("get/{applicaitonId}")]
    public async Task<IActionResult> Get([FromRoute] string applicaitonId, CancellationToken cancellationToken)
    {
     
        var result = await _application.Get(applicaitonId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("create/{applicationTypeId}")]
    public async Task<IActionResult> Create([FromRoute ] int applicationTypeId ,[FromBody] ApplicationRequest request, CancellationToken cancellationToken)
    {
        var result = await _application.Create(request, cancellationToken);

        return result.IsSuccess ? Ok() : result.ToProblem();
    }
}
