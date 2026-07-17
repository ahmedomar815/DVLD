namespace DVLD.Contracts.Authentication;

public record LoginResponse(string FirstName, string LastName, string ThirdName
    , string FourthName, string Email, string Id, string Token,int ExpireIn,string RefreshToken);

