using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace web.Extensions
{
    public static class ControllerExtension
    {
        public const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        public static string GetId(this Controller controller)
        {
            return controller.GetSpecificClaim(ObjectIdentifier);
        }

        public static string GetSpecificClaim(this Controller controller, string claimType)
        {
            var claim = controller.User.Claims.FirstOrDefault(x => x.Type == claimType);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}