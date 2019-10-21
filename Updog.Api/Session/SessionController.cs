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
        private IMediator mediator;
        private IEventBus bus;

        private IUserLoginFactory loginFactory;
        #endregion

        #region Constructor(s)
        public SessionController(IMediator mediator, IEventBus bus, IUserLoginFactory loginFactory) {
            this.mediator = mediator;
            this.bus = bus;
            this.loginFactory = loginFactory;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Login a user and issue them a bearer token.
        /// </summary>
        /// <param name="loginRequest">The credentials to authenticate under.</param>
        [HttpPost]
        public async Task<IActionResult> Login([FromBody]SessionLoginRequest loginRequest) {
            UserLogin? login = null;

            bus.Listen<UserLoginEvent>((IDomainEvent e) => {
                UserLoginEvent loginEvent = (UserLoginEvent)e;
                login = loginEvent.Login;
            });

            var result = await mediator.Command(new LoginUserCommand() {
                Credentials = new UserCredentials(loginRequest.Username, loginRequest.Password)
            });

            return login != null ? Ok(login) : Unauthorized("Invalid username and/or password.") as IActionResult;
        }

        /// <summary>
        /// Relogin using an auth token issued at an earlier date.
        /// </summary>
        [HttpPatch]
        [Authorize]
        public ActionResult ReLogin() {
            /*
            * Dirty work is done by the auth filter.
            * Down the road this can be tweaked to support rolling tokens...
            */
            return Ok(loginFactory.Create(User!));
        }
        #endregion
    }
}