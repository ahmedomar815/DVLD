using DVLD.Contracts.Test;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class TestController(ITestService testService) : ControllerBase
{
    private readonly ITestService _testService = testService;

    public async Task<IActionResult> Create([FromBody]TestRequest request,CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var result=await _testService.CreateAsync(userId!, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
 
