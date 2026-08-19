using DVLD.Abstractions.Consts;
using DVLD.Authentication.Filters;
using DVLD.Contracts.TestAppointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class TestAppointmentController(ITestAppointmentService testAppointmentService) : ControllerBase
{
    private readonly ITestAppointmentService _testAppointmentService = testAppointmentService;

    [HttpGet("{testAppointmentId}")]
    [HasPermission(Permissions.GetTestAppointments)]
    public async Task <IActionResult> Get(string testAppointmentId, CancellationToken cancellationToken)
    {
        var result = await _testAppointmentService.GetAsync(testAppointmentId, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPost("")]
    [HasPermission(Permissions.CreateTestAppointments)]
    public async Task<IActionResult> Create(TestAppointmentRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var result = await _testAppointmentService.CreateAsync(userId!, request, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    [HttpPut("{testAppointmentId}")]
    [HasPermission(Permissions.UpdateTestAppointments)]
    public async Task<IActionResult> Update([FromRoute]string testAppointmentId, TestAppointmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _testAppointmentService.UpdateAsync(testAppointmentId, request, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
