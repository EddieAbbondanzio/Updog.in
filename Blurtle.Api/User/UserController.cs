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
        #region Constructor(s)
        /// <summary>
        /// Create a new user controller.
        /// </summary>
        public UserController() {
        }
        #endregion

        #region Publics
        // /// <summary>
        // /// Retrieve a user from the backend via their username.
        // /// </summary>
        // /// <param name="username">The username of the user to look for.</param>
        // [HttpGet("{username}")]
        // public async Task<ActionResult> FindUserByUsername(string username) {
        //     // User user = await userService.FindUserByUsername(username);
        //     return user != null ? Ok(user) : NotFound() as ActionResult;
        // }

        // /// <summary>
        // /// Log into the backend and recieve a JWT if success.
        // /// </summary>
        // /// <param name="credentials">The user credentials.</param>
        // [HttpPost("login")]
        // public async Task<ActionResult> LoginUser([FromBody] UserCredentials credentials) {
        //     User user = await userService.LoginUser(credentials);
        //     return user != null ? Ok(userService.IssueAuthToken(user)) : Unauthorized() as ActionResult;
        // }

        // public async Task RegisterUser() {

        // }

        // public async Task LoginUser() {

        // }

        // public async Task IsUsernameAvailable() {

        // }

        // public async Task UpdateUser() {

        // }

        // public async Task UpdateUserPassword() {

        // }
        #endregion
    }
}