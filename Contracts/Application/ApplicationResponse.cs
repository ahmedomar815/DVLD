using DVLD.Contracts.ApplicationType;

namespace DVLD.Contracts.Application;

public record ApplicationResponse( string Status
    , decimal PaidFees, string ApplicationTypeName, UserResponse UserResponse);

