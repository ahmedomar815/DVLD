
using DVLD.Contracts.User;

namespace DVLD.Contracts.Application;
public record ApplicationResponse( string Status, decimal PaidFees, string ApplicationTypeName, UserResponse UserResponse);

