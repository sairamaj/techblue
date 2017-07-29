using System.Security.Claims;
using System.Linq;

namespace web.Extensions
{
    public static class ClaimsIdentityExtension
    {
        public const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        public static string GetId(this ClaimsIdentity claimsIdentity)
        {
            return claimsIdentity.GetSpecificClaim(ObjectIdentifier);
        }

        public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}