using Updog.Application;
using Updog.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Updog.Api {
    /// <summary>
    /// End point for managing the logged in user.
    /// </summary>
    [Authorize]
    [Route("api/me")]
    [ApiController]
    public sealed class MeController : ApiController {
        #region Fields
        public UserUpdater _userUpdater;

        public UserPasswordUpdater _passwordUpdater;
        #endregion

        #region Constructor(s)
        public MeController(UserUpdater userUpdater, UserPasswordUpdater passwordUpdater) {
            this._userUpdater = userUpdater;
            this._passwordUpdater = passwordUpdater;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get the email of the user.
        /// </summary>
        [HttpGet("email")]
        public ActionResult GetEmail() {
            return Ok(User!.Email);
        }

        /// <summary>
        /// Update the info of a user.
        /// </summary>
        /// <param name="updateRequest">The new user info.</param>
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] MeUpdateRequest updateRequest) {
            await _userUpdater.Handle(new UpdateUserParams(User!, updateRequest.Email));
            return Ok();
        }

        /// <summary>
        /// Update the password of the logged in user.
        /// </summary>
        /// <param name="updatePasswordRequest">The new password.</param>
        [HttpPut("password")]
        public async Task<ActionResult> UpdatePassword([FromBody] MeUpdatePasswordRequest updatePasswordRequest) {
            await _passwordUpdater.Handle(new UserPasswordUpdateParams(User!, updatePasswordRequest.Password));
            return Ok();
        }
        #endregion
    }
}