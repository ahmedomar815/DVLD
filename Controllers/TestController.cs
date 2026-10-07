
using Api.Dtos.Tests.Requests;
using Application.Features.Tests.Commands.CreateTest;
using Application.Features.Tests.Commands.UpdateTest;


namespace Api.Controllers;

[ApiController]
[Route("api/tests")]
[Authorize]
public sealed class TestController(ISender sender) : ControllerBase
{
    [HttpPost]
    [HasPermission(Permissions.CreateTests)]
    public async Task<IActionResult> Create([FromBody] TestDto request, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized();

        var command = request.Adapt<CreateTestCommand>() with { UserId = userId, Notes = request.Notes ?? string.Empty };
        var result = await sender.Send(command, cancellationToken);

        return result.IsSuccess
            ? Created($"api/tests/{result.Value.Id}", result.Value.Adapt<TestDto>())
            : result.ToProblem();
    }

    [HttpPut("{testId}")]
    [HasPermission(Permissions.UpdateTests)]
    public async Task<IActionResult> Update(
        [FromRoute] string testId,
        [FromBody] TestDto request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateTestCommand>() with
        {
            TestId = testId,
            Notes = request.Notes ?? string.Empty
        };
        var result = await sender.Send(command, cancellationToken);
        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
