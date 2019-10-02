using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;
using System;
using FluentValidation;
using System.Linq;

namespace Updog.Api {
    /// <summary>
    /// End point for managing users of the site.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public sealed class UserController : ApiController {
        #region Fields
        private UserFinderByUsername userFinder;
        private UserRegistrar userRegistrar;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user controller.
        /// </summary>
        public UserController(
                UserFinderByUsername userFinder,
                UserRegistrar userRegistrar
            ) {
            this.userFinder = userFinder;
            this.userRegistrar = userRegistrar;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Retrieve a user from the backend via their username.
        /// </summary>
        /// <param name="username">The username of the user to look for.</param>
        [HttpGet("{username}")]
        [HttpHead("{username}")]
        public async Task<ActionResult> FindByUsername(string username) {
            // When checking for username availability, see if the username is banned first.
            if (Request.Method.Equals("HEAD") && User.IsUsernameBanned(username)) {
                return BadRequest("Username is unavailable");
            }

            var result = await userFinder.Handle(new FindByValueParams<string>(username, user: User));

            return result.Match<ActionResult>(
                user => user != null ? Ok(user) : NotFound() as ActionResult,
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Register a new user with the website.
        /// </summary>
        /// <param name="registration">The new user registration</param>
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegisterRequest req) {
            var result = await userRegistrar.Handle(new UserRegisterParams(req.Username, req.Password, req.Email));

            return result.Match<ActionResult>(
                login => Ok(login),
                fail => BadRequest(fail)
            );
        }
        #endregion
    }
}