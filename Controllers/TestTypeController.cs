using DVLD.Contracts.TestType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class TestTypeController(ITestTypeService testTypeService) : ControllerBase
{
    private readonly ITestTypeService _testTypeService = testTypeService;

    [HttpGet("{testTypeId}")]
    [HasPermission(Permissions.GetTestTypes)]
    public async Task<IActionResult> Get([FromRoute] int  testTypeId, CancellationToken cancellationToken)
    {
        var result = await _testTypeService.GetAsync(testTypeId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpGet("")]
    [HasPermission(Permissions.GetTestTypes)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _testTypeService.GetAllAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("")]
    [HasPermission(Permissions.CreateTestTypes)]
    public async Task<IActionResult> Create([FromBody] TestTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _testTypeService.CreateAsync(request, cancellationToken);

        return result.IsSuccess
        ? CreatedAtAction(nameof(Get), new { testTypeId = result.Value.Id }, result.Value)
        : result.ToProblem(); 
    }
    [HttpPut("{testTypeId}")]
    [HasPermission(Permissions.UpdateTestTypes)]
    public async Task<IActionResult> Update([FromRoute] int  testTypeId , [FromBody] TestTypeRequest request, CancellationToken cancellationToken)
    {
        var result = await _testTypeService.UpdateAsync(testTypeId, request, cancellationToken);

        return result.IsSuccess  ? NoContent():result.ToProblem();
    }

    [HttpDelete("{testTypeId}")]
    [HasPermission(Permissions.DeleteTestTypes)]
    public async Task<IActionResult> Delete([FromRoute] int testTypeId, CancellationToken cancellationToken)
    {
        var result = await _testTypeService.DeleteAsync(testTypeId,cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
