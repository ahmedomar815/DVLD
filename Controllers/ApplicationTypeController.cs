using DVLD.Abstractions;
using DVLD.Abstractions.Consts;
using DVLD.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ApplicationTypeController(IApplicationTypeService applicationTypeService) : ControllerBase
{
    private readonly IApplicationTypeService _applicationTypeService = applicationTypeService;

    [HttpGet("{applicationTypeId:int}")]
    [HasPermission(Permissions.GetApplicationTypes)]
    public async Task<IActionResult> Get([FromRoute] int applicationTypeId, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.GetAsync(applicationTypeId, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet]
    [HasPermission(Permissions.GetApplicationTypes)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.GetAllAsync(cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.CreateApplicationTypes)]
    public async Task<IActionResult> Create([FromBody] ApplicationTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.CreateAsync(request, cancellationToken);

        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { applicationTypeId = result.Value.Id }, result.Value)
            : result.ToProblem();
    }
    [HttpPut("{applicationTypeId:int}")]
    [HasPermission(Permissions.UpdateApplicationTypes)]
    public async Task<IActionResult> Update([FromRoute] int applicationTypeId, [FromBody] ApplicationTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.UpdateAsync(applicationTypeId, request, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("{applicationTypeId:int}")]
    [HasPermission(Permissions.DeleteApplicationTypes)]
    public async Task<IActionResult> Delete([FromRoute] int applicationTypeId, CancellationToken cancellationToken)
    {
        var result = await _applicationTypeService.DeleteAsync(applicationTypeId, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    

}
