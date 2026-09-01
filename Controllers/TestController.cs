using DVLD.Contracts.Test;
using DVLD.Properties.Abstractions;
using DVLD.Properties.Abstractions.Consts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class TestController(ITestService testService) : ControllerBase
{
    private readonly ITestService _testService = testService;

    [HttpPost]
    [HasPermission(Permissions.CreateTests)]
    public async Task<IActionResult> Create([FromBody]TestRequest request,CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var result=await _testService.CreateAsync(userId!, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }

    [HttpPut("{testId}")]
    [HasPermission(Permissions.UpdateTests)]
    public async Task<IActionResult> Update(
        [FromRoute] string testId,
        [FromBody] TestRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _testService.UpdateAsync(testId, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
 
