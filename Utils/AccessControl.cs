using System.Security.Claims;
using coursework_itransition.Models;

namespace coursework_itransition
{
    public static class AccessControl
    {
        public static bool UserHasAccess(ClaimsPrincipal user, Composition comp)
        {
            if(user == null || (System.Object)comp == null)
                return false;

            var c = user.FindFirst(ClaimTypes.NameIdentifier);

            if (c == null)
                return false;

            return c.Value == comp.AuthorID || user.IsInRole("Administrator");
        }

        public static bool UserHasAccess(ClaimsPrincipal user, Chapter chapter)
        {
            if(user == null || (System.Object)chapter == null)
                return false;

            if((System.Object)chapter.Composition == null)
                return false;

            var c = user.FindFirst(ClaimTypes.NameIdentifier);

            if (c == null)
                return false;

            return c.Value == chapter.Composition.AuthorID || user.IsInRole("Administrator");
        }
    }
}