using DVLD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ApplicationTypeController(IApplicationTypeService applicationTypeService) : ControllerBase
{
    private readonly IApplicationTypeService _applicationTypeService = applicationTypeService;

    [HttpGet("get/{applicationTypeId}")]
    [HasPermission(Permissions.GetApplicationTypes)]
    public async Task<IActionResult> Get([FromRoute] int applicationTypeId, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.Get(applicationTypeId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("get-all")]
    [HasPermission(Permissions.GetApplicationTypes)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.GetAll(cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("")]
    [HasPermission(Permissions.CreateApplicationTypes)]
    public async Task<IActionResult> Create([FromBody] ApplicationTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.CreateApplicationType(request, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { applicationTypeId = result.Value.Id }, result.Value)
            : result.ToProblem();
    }
    [HttpPut("update/{applicationTypeId}")]
    [HasPermission(Permissions.UpdateApplicationTypes)]
    public async Task<IActionResult> Update([FromRoute] int applicationTypeId, [FromBody] ApplicationTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.Update(applicationTypeId, request, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("delete/{applicationTypeId}")]
    [HasPermission(Permissions.DeleteApplicationTypes)]
    public async Task<IActionResult> Delete([FromRoute] int applicationTypeId, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.Delete(applicationTypeId, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    

}
