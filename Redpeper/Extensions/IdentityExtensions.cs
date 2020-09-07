using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Redpeper.Extensions
{
    public static class IdentityExtensions
    {

        private static string GetValueOrDefault(IIdentity identity, string claimType)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst(claimType);

            return (claim == null) ? string.Empty : claim.Value;
        }

        public static string GetFirstName(this IIdentity identity)
        {
            return GetValueOrDefault(identity, ClaimTypes.GivenName);
        }

        public static string GetId(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirst("UserId").Value;
        }

        public static string GetEmployeeId(this IIdentity identity)
        {
            return (identity as ClaimsIdentity).FindFirst("EmployeeId").Value;
        }

        public static IEnumerator<Claim> GetUserClaims(this IIdentity identity)
        {
            //var ra = (ClaimsIdentity)User.Identity;
            return (identity as ClaimsIdentity).Claims.GetEnumerator();
        }
    }

}
