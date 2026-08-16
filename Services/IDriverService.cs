using DVLD.Contracts.Driver;

namespace DVLD.Services;

public interface IDriverService
{
    Task<Result<DriverResponse>> GetAsync(string driverId, CancellationToken cancellationToken);
    Task<Result<DriverResponse>> CreateAsync(DriverRequest request, CancellationToken cancellationToken);

}
