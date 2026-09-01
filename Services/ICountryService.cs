using DVLD.Contracts.Country;

namespace DVLD.Services;

public interface ICountryService
{
    Task<Result<IEnumerable<CountryResponse>>> GetAllAsync(CancellationToken cancellationToken);
}
