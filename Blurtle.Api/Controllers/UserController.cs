using Microsoft.AspNetCore.Mvc;
using Blurtle.Application;
using System.Threading.Tasks;
using Blurtle.Domain;
using System;

namespace Blurtle.Api {
    /// <summary>
    /// End point for managing users of the site.
    /// </summary>
    [Route("api/user")]
    [ApiController]
    public sealed class UserController : ControllerBase {
        #region Fields
        private IUserService userService;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new user controller.
        /// </summary>
        /// <param name="userService">The user service to retrieve users with.</param>
        public UserController(IUserService userService) {
            this.userService = userService;
        }
        #endregion

        #region Publics
        [HttpGet("{username}")]
        public async Task<ActionResult> FindUserByUsername(string username) {
            User user = await userService.FindUserByUsername(username);
            Console.WriteLine(user.Email);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] UserCredentials credentials) {
            User user = await userService.LoginUser(credentials);

            if (user != null) {
                return Ok(userService.IssueAuthToken(user));
            } else {
                return Unauthorized();
            }
        }

        // public async Task RegisterUser() {

        // }

        // public async Task LoginUser() {

        // }

        // public async Task IsUsernameAvailable() {

        // }
        #endregion
    }
}