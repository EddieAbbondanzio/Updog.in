using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Blurtle.Application;
using Blurtle.Domain;
using Blurtle.Infrastructure;
using Microsoft.IdentityModel.Tokens;

namespace Blurtle.Api {
    public sealed class JsonWebTokenHandler : IAuthenticationTokenHandler {
        #region Fields
        private uint expiresIn;

        private string issuer;

        private SymmetricSecurityKey secret;
        #endregion

        #region Constructor(s)
        public JsonWebTokenHandler(IAuthenticationTokenConfig config) {
            expiresIn = config.Expires;
            issuer = config.Issuer;
            secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));
        }
        #endregion

        public string IssueToken(User user) {
            Claim[] claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
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