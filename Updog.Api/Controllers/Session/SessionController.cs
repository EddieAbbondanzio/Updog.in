using Updog.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Api {
    /// <summary>
    /// End point for managing user sessions.
    /// </summary>
    [Route("api/session")]
    [ApiController]
    public sealed class SessionController : ApiController {
        #region Fields
        private UserLoginInteractor loginUserInteractor;
        #endregion

        #region Constructor(s)
        public SessionController(UserLoginInteractor loginUserInteractor) {
            this.loginUserInteractor = loginUserInteractor;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Login a user and issue them a bearer token.
        /// </summary>
        /// <param name="loginRequest">The credentials to authenticate under.</param>
        [HttpPost]
        public async Task<ActionResult> Login([FromBody]SessionLoginRequest loginRequest) {
            var result = await loginUserInteractor.Handle(new UserCredentials(loginRequest.Username, loginRequest.Password));

            return result.Match<ActionResult>(
                login => Ok(login),
                fail => BadRequest(fail)
            );
        }
        #endregion
    }
}