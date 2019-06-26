using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Handler for issuing and validating bearer tokens.
    /// </summary>
    public interface IAuthenticationTokenHandler {
        #region Publics
        /// <summary>
        /// Issue a bearer token for a user.
        /// </summary>
        /// <param name="user">The user to issue it to.</param>
        /// <returns>The generated bearer token.</returns>
        string IssueToken(User user);

        /// <summary>
        /// Validate a token to check that it is legit.
        /// </summary>
        /// <param name="token">The bearer token to validate.</param>
        /// <returns>If the token was valid or not.</returns>
        AuthenticationTokenValidationResult ValidateToken(string token);
        #endregion
    }
}