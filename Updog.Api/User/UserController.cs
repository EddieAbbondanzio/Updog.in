using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;
using System;
using FluentValidation;
using System.Linq;
using System.Collections.Generic;

namespace Updog.Api {
    /// <summary>
    /// End point for managing users of the site.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public sealed class UserController : ApiController {
        #region Fields
        private IMediator mediator;
        private IEventBus bus;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user controller.
        /// </summary>
        public UserController(IMediator mediator, IEventBus bus) {
            this.mediator = mediator;
            this.bus = bus;
        }
        #endregion

        #region Publics
        [HttpHead("{username}")]
        public async Task<IActionResult> IsUsernameAvailable(string username) =>
            (await mediator.Query<IsUsernameAvailableQuery, bool>(new IsUsernameAvailableQuery(username, User))).Match(
                isFree => isFree ? NotFound() : Ok() as IActionResult,
                error => BadRequest(error.Message) as IActionResult
            );

        /// <summary>
        /// Retrieve a user from the backend via their username.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        [HttpGet("{username}")]
        public async Task<IActionResult> FindByUsername(string username) =>
            (await mediator.Query<FindUserByUsernameQuery, UserReadView?>(new FindUserByUsernameQuery(username, User)))
            .Match(
                user => user != null ? Ok(user) : NotFound() as IActionResult,
                error => BadRequest(error.Message) as IActionResult
            );

        [HttpGet("{username}/moderator")]
        public async Task<IActionResult> GetModeratorSpaces(string username) =>
            (await mediator.Query<FindSpacesUserModeratesQuery, IEnumerable<SpaceReadView>>(new FindSpacesUserModeratesQuery(username, User)))
            .Match(
                spaces => Ok(spaces) as IActionResult,
                error => BadRequest(error.Message) as IActionResult
            );

        /// <summary>
        /// Register a new user with the website.
        /// </summary>
        /// <param name="registration">The new user registration</param>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest req) {
            UserLogin? login = null;

            bus.Listen<UserRegisterEvent>((IDomainEvent e) => {
                UserRegisterEvent registerEvent = (UserRegisterEvent)e;
                login = registerEvent.Login;
            });

            var result = await mediator.Command(new RegisterUserCommand() {
                Registration = new UserRegistration(req.Username, req.Password, req.Email)
            });

            if (login != null) {
                return Ok(login);
            } else {
                return BadRequest(result.Right().Message);
            }
        }
        #endregion
    }
}