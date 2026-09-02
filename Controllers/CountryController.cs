using DVLD.Abstractions;
using DVLD.Abstractions.Consts;
using DVLD.Authentication.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CountryController(ICountryService countryService) : ControllerBase
{
    private readonly ICountryService _countryService = countryService;

    [HttpGet]
    [HasPermission(Permissions.GetCountries)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _countryService.GetAllAsync(cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
}
