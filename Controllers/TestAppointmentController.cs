using DVLD.Contracts.TestAppointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class TestAppointmentController(ITestAppointmentService testAppointmentService) : ControllerBase
{
    private readonly ITestAppointmentService _testAppointmentService = testAppointmentService;

    [HttpGet("{testAppointmentId}")]
    public async Task <IActionResult> Get(string testAppointmentId, CancellationToken cancellationToken)
    {
        var result = await _testAppointmentService.GetAsync(testAppointmentId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("")]
    public async Task<IActionResult> Create(TestAppointmentRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var result = await _testAppointmentService.CreateAsync(userId!, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPut("{testAppointmentId}")]
    public async Task<IActionResult> Update([FromRoute]string testAppointmentId, TestAppointmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _testAppointmentService.UpdateAsync(testAppointmentId, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
