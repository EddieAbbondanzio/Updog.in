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

        private UserRegisterInteractor userRegistrar;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user controller.
        /// </summary>
        public UserController(
                UserFinderByUsername userFinder,
                UserRegisterInteractor userRegistrar
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
            UserView user = await userFinder.Handle(username);
            return user != null ? Ok(user) : NotFound() as ActionResult;
        }

        /// <summary>
        /// Register a new user with the website.
        /// </summary>
        /// <param name="registration">The new user registration</param>
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserRegisterRequest req) {
            UserRegisterParams registration = new UserRegisterParams(req.Username, req.Password, req.Email);

            try {
                UserLogin login = await userRegistrar.Handle(registration);
                return login != null ? Ok(login) : BadRequest("Registration failed.") as ActionResult;
            } catch (ValidationException ex) {
                return BadRequest(new ValidationError(ex));
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return BadRequest("Error, please try again later");
            }
        }
        #endregion
    }
}