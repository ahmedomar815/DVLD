
[Route("[controller]")]
[ApiController]
[Authorize]
public class CountryController(ISender sender) : ControllerBase
{
    [HttpGet]
    [HasPermission(Permissions.GetCountries)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetCountriesQuery(), cancellationToken);
        return result.IsSuccess ? Ok(result.Value.Adapt<IEnumerable<CountryDto>>()) : result.ToProblem();
    }
}
