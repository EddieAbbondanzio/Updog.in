using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Updog.Application;
using Updog.Domain;
using Updog.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace Updog.Api {
    /// <summary>
    /// Authentication token handler that uses json web tokens.
    /// </summary>
    public sealed class JsonWebTokenHandler {
        #region Fields
        /// <summary>
        /// How many seconds the token is valid for.
        /// </summary>
        private uint expiresIn;

        /// <summary>
        /// The domain that issued the token.
        /// </summary>
        private string issuer;

        /// <summary>
        /// Signing key used to sign tokens with.
        /// </summary>
        private SymmetricSecurityKey secret;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new JWT handler.
        /// </summary>
        /// <param name="config">Config settings.</param>
        public JsonWebTokenHandler(IAuthenticationTokenConfig config) {
            expiresIn = config.Expires;
            issuer = config.Issuer;
            secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));
        }
        #endregion

        /// <summary>
        /// Issue a JWT for a user.
        /// </summary>
        /// <param name="login">The user to isssue it to.</param>
        /// <returns>The generated JWT.</returns>
        public string IssueToken(UserLogin login) {
            // Less is more, we only really need user Id since we'll have to hit the DB anyways.
            Claim[] claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, login.Id.ToString()),
                new Claim(ClaimTypes.Sid, login.Id.ToString())
            };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                issuer,
                claims,
                expires: DateTime.Now.AddSeconds(expiresIn),
                signingCredentials: new SigningCredentials(secret, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}