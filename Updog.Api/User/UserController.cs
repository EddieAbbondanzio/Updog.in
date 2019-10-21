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
        private JsonWebTokenHandler tokenHandler;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user controller.
        /// </summary>
        public UserController(IMediator mediator, IEventBus bus, JsonWebTokenHandler tokenHandler) {
            this.mediator = mediator;
            this.bus = bus;
            this.tokenHandler = tokenHandler;
        }
        #endregion

        #region Publics
        [HttpHead("{username}")]
        public async Task<IActionResult> IsUsernameAvailable(string username) =>
            (await mediator.Query<IsUsernameAvailableQuery, bool>(new IsUsernameAvailableQuery(username, User))).Match<IActionResult>(
                isFree => isFree ? NotFound() : Ok() as IActionResult,
                error => BadRequest(error.Message)
            );

        /// <summary>
        /// Retrieve a user from the backend via their username.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        [HttpGet("{username}")]
        public async Task<IActionResult> FindByUsername(string username) =>
            (await mediator.Query<FindUserByUsernameQuery, UserReadView?>(new FindUserByUsernameQuery(username, User)))
            .Match<IActionResult>(
                user => user != null ? Ok(user) : NotFound() as IActionResult,
                error => BadRequest(error.Message)
            );

        [HttpGet("{username}/moderator")]
        public async Task<IActionResult> GetModeratorSpaces(string username) =>
            (await mediator.Query<FindSpacesUserModeratesQuery, IEnumerable<SpaceReadView>>(new FindSpacesUserModeratesQuery(username, User)))
            .Match<IActionResult>(
                spaces => Ok(spaces),
                error => BadRequest(error.Message)
            );

        /// <summary>
        /// Register a new user with the website.
        /// </summary>
        /// <param name="registration">The new user registration</param>
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest req) {
            // Yuck
            UserLogin? login = null;

            bus.Listen<UserRegisterEvent>(e => login = ((UserRegisterEvent)e).Login);

            var result = await mediator.Command(new RegisterUserCommand() {
                Registration = new UserRegistration(req.Username, req.Password, req.Email)
            });


            result.Match<IActionResult>(
                result => login != null ? Ok(new { Id = result.InsertId, AuthToken = tokenHandler.IssueToken(login) }) : BadRequest() as IActionResult,
                error => BadRequest(error.Message)
            );

            if (login != null) {
                return Ok(login);
            } else {
                return BadRequest(result.Right().Message);
            }
        }
        #endregion
    }
}