using System.Security.Claims;

namespace DVLD.Extensions
{
    public static class UserExtension
    {
        public static string?GetUserId (this  ClaimsPrincipal user)
        {
            return  user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
