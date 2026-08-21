namespace DVLD.Contracts.User;

public record UserResponse(
    string Id,
    string FirstName,
    string SecondName,
    string ThirdName,
    string FourthName,
    string Email,
    string Phone,
    string NationalId
    ,string CountryName);
