using DVLD.Contracts.Country;

namespace DVLD.Services;

public class CountryService(ApplicationDbContext context) : ICountryService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Result<IEnumerable<CountryResponse>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var countries = await _context.Countries
            .AsNoTracking()
            .OrderBy(country => country.Name)
            .Select(country => new CountryResponse(country.Id, country.Name))
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<CountryResponse>>(countries);
    }
}
