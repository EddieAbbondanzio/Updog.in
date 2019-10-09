using Updog.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Updog.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Updog.Api {
    /// <summary>
    /// End point for managing user sessions.
    /// </summary>
    [Route("api/session")]
    [ApiController]
    public sealed class SessionController : ApiController {
        #region Fields
        private CommandHandler<LoginUserCommand> loginCommand;

        #endregion

        #region Constructor(s)
        public SessionController(CommandHandler<LoginUserCommand> loginCommand) {
            this.loginCommand = loginCommand;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Login a user and issue them a bearer token.
        /// </summary>
        /// <param name="loginRequest">The credentials to authenticate under.</param>
        [HttpPost]
        public async Task<ActionResult> Login([FromBody]SessionLoginRequest loginRequest) {
            var result = await loginCommand.Execute(new LoginUserCommand() {
                Credentials = new UserCredentials(loginRequest.Username, loginRequest.Password)
            });

            return Ok(result);
        }

        /// <summary>
        /// Relogin using an auth token issued at an earlier date.
        /// </summary>
        [HttpPatch]
        [Authorize]
        public ActionResult ReLogin([FromHeader] string authorization) {
            /*
            * Dirty work is done by the auth filter.
            * Down the road this can be tweaked to support rolling tokens...
            */
            return Ok(new UserLogin(User!.Id, authorization.Split(" ")[1]));
        }
        #endregion
    }
}