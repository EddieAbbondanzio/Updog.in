using System;
using System.Collections.Generic;
using Blurtle.Application;
using Blurtle.Domain;
using JWT;
using JWT.Algorithms;
using JWT.Builder;

namespace Blurtle.Infrastructure {
    /// <summary>
    /// Authentication token handler that uses JWTs to authenticate users.
    /// </summary>
    public sealed class JsonWebTokenHandler : IAuthenticationTokenHandler {
        #region Fields
        /// <summary>
        /// How long the tokens are valid for (in seconds)
        /// </summary>
        private int lifeSpan;

        /// <summary>
        /// The secret key used to sign tokens.
        /// </summary>
        private string secret;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new json web token handler.
        /// </summary>
        /// <param name="lifeSpan">How long tokens are valid for.</param>
        /// <param name="secret">The secret key to sign with.</param>
        public JsonWebTokenHandler(int lifeSpan, string secret) {
            this.lifeSpan = lifeSpan;
            this.secret = secret;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Issue a JWT for a user.
        /// </summary>
        /// <param name="user">The user to issue it to.</param>
        /// <returns>The generated JWT.</returns>
        public string IssueToken(User user) => new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(secret)
            .AddClaim("exp", DateTimeOffset.UtcNow.AddSeconds(lifeSpan).ToUnixTimeSeconds())
            .AddClaim("user", user.Id)
            .Build();

        /// <summary>
        /// Validate a token and check it's status.
        /// </summary>
        /// <param name="token">The token to validate.</param>
        /// <returns>The token's status.</returns>
        public AuthenticationTokenValidationResult ValidateToken(string token) {
            try {
                IDictionary<string, object> payload = new JwtBuilder()
                .WithSecret(secret)
                .MustVerifySignature()
                .Decode<IDictionary<string, object>>(token);

                int userId = Convert.ToInt32(payload["user"]);

                return new AuthenticationTokenValidationResult(AuthenticationTokenStatus.Valid, userId);
            } catch (TokenExpiredException) {
                return new AuthenticationTokenValidationResult(AuthenticationTokenStatus.Expired);
            } catch (SignatureVerificationException) {
                return new AuthenticationTokenValidationResult(AuthenticationTokenStatus.Invalid);
            } catch {
                return new AuthenticationTokenValidationResult(AuthenticationTokenStatus.Invalid);
            }
        }
        #endregion
    }
}