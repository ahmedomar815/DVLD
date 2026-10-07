using RequestDto = Api.Dtos.TestAppointments.Requests.TestAppointmentDto;
using ResponseDto = Api.Dtos.TestAppointments.Responses.TestAppointmentDto;
using Application.Features.TestAppointments.Commands.CreateTestAppointment;
using Application.Features.TestAppointments.Commands.UpdateTestAppointment;
using Application.Features.TestAppointments.Queries.GetTestAppointment;

namespace Api.Controllers;

[ApiController]
[Route("api/test-appointments")]
[Authorize]
public sealed class TestAppointmentController(ISender sender) : ControllerBase
{
    [HttpGet("{testAppointmentId}")]
    [HasPermission(Permissions.GetTestAppointments)]
    public async Task<IActionResult> Get(string testAppointmentId, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetTestAppointmentQuery(testAppointmentId), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<ResponseDto>()) : result.ToProblem();
    }

    [HttpPost]
    [HasPermission(Permissions.CreateTestAppointments)]
    public async Task<IActionResult> Create(
        [FromBody] RequestDto request,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized();

        var command = request.Adapt<CreateTestAppointmentCommand>() with { UserId = userId };

        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Get), new { testAppointmentId = result.Value.Id }, result.Value.Adapt<ResponseDto>())
            : result.ToProblem();
    }

    [HttpPut("{testAppointmentId}")]
    [HasPermission(Permissions.UpdateTestAppointments)]
    public async Task<IActionResult> Update(
        string testAppointmentId,
        [FromBody] RequestDto request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateTestAppointmentCommand>() with
        {
            TestAppointmentId = testAppointmentId
        };

        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
