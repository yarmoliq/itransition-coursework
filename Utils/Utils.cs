using System.Security.Claims;

using Identity.Models;
using coursework_itransition.Data;

namespace coursework_itransition
{
    public static class Utils
    {
        public static string GetUserID(ClaimsPrincipal userClaims)
        {
            if(userClaims == null)
                return null;

            var nameIdentifier = userClaims.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if(nameIdentifier == null)
                return null;

            return nameIdentifier.Value;
        }
    }
}