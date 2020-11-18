using System.Security.Claims;
using coursework_itransition.Models;

namespace coursework_itransition
{
    public static class Utils
    {
        public static bool UserIsAuthor(ClaimsPrincipal User, Composition comp)
        {
            if (User == null)
                return false;

            var c = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (c == null)
                return false;

            return c.Value == comp.AuthorID;
        }
        
        public static bool UserIsAuthor(ClaimsPrincipal User, string AuthorID)
        {
            if (User == null)
                return false;

            var c = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (c == null)
                return false;

            return c.Value == AuthorID;
        }

        public static string GetUserID(ClaimsPrincipal User)
        {
            if(User == null)
                return null;

            var c = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if(c == null)
                return null;

            return c.Value;
        }
    }
}