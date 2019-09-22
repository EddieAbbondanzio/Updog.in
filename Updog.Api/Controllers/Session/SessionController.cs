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
        private UserLoginInteractor loginUserInteractor;

        private IUserViewMapper userViewMapper;
        #endregion

        #region Constructor(s)
        public SessionController(UserLoginInteractor loginUserInteractor, IUserViewMapper userViewMapper) {
            this.loginUserInteractor = loginUserInteractor;
            this.userViewMapper = userViewMapper;
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
                login => login != null ? Ok(login) : Unauthorized("Invalid username and/or password") as ActionResult,
                fail => BadRequest(fail)
            );
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
            UserView user = userViewMapper.Map(User!);

            return Ok(new UserLogin(user, authorization.Split(" ")[1]));
        }
        #endregion
    }
}