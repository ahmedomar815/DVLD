namespace DVLD.Extensions;

public static class EnumHelper
{
    public static string GetName<T>(T value) where T : Enum
    {
        return value.ToString();
    }
}