using System;
using System.Linq;
using System.Security.Claims;

namespace Updog.Api {
    /// <summary>
    /// Extension methods for ClaimsPrincipal.
    /// </summary>
    public static class ClaimsPrincipalExts {
        /// <summary>
        /// Get the user Id from the NameIdentifier claim on the user principal.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <returns>The unique Id of the user the principal is for.</returns>
        public static int GetUserId(this ClaimsPrincipal principal) {
            Claim subjectClaim = principal.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier);
            return Convert.ToInt32(subjectClaim);
        }
    }
}