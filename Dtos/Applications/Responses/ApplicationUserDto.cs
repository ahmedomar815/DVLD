namespace Api.Dtos.Applications.Responses;

public sealed record ApplicationUserDto(
    string Id,
    string FirstName,
    string SecondName,
    string ThirdName,
    string FourthName,
    string Email,
    string PhoneNumber,
    string Address,
    string NationalId,
    string CountryName);
