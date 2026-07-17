using DVLD.Services;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
public class ApplicationTypeController(IApplicationTypeService applicationTypeService) : ControllerBase
{
    private readonly IApplicationTypeService _applicationTypeService = applicationTypeService;

    [HttpGet("get/{applicationTypeId}")]
    public async Task<IActionResult> Get([FromRoute] int applicationTypeId, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.Get(applicationTypeId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.GetAll(cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost("add")]
    public async Task<IActionResult> Create([FromBody] ApplicationTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.CreateApplicationType(request, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpPut("update/{applicationTypeId}")]
    public async Task<IActionResult> Update([FromRoute] int applicationTypeId, [FromBody] ApplicationTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.Update(applicationTypeId, request, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("delete/{applicationTypeId}")]
    public async Task<IActionResult> Delete([FromRoute] int applicationTypeId, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.Delete(applicationTypeId, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    

}
