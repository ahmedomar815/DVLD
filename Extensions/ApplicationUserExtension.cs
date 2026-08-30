namespace DVLD.Extensions;

public static class ApplicationUserExtension
{
    public static string GetFullName(this ApplicationUser user)
    {
        if (user == null)
            return string.Empty;

        var firstName = user.FirstName;
        var secondName = user.SecondName;
        var thirdName = user.ThirdName;
        var fourthName = user.FourthName;

        var parts = new[] { firstName, secondName, thirdName, fourthName };
            

        return string.Join(" ", parts);
    }
}
