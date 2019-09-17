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
        public UserUpdater userUpdater;

        public UserPasswordUpdater passwordUpdater;
        #endregion

        #region Constructor(s)
        public MeController(UserUpdater userUpdater, UserPasswordUpdater passwordUpdater) {
            this.userUpdater = userUpdater;
            this.passwordUpdater = passwordUpdater;
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
            var result = await userUpdater.Handle(new UpdateUserParams(User!, updateRequest.Email));

            return result.Match<ActionResult>(
                fail => BadRequest(fail),
                Ok
            );
        }

        /// <summary>
        /// Update the password of the logged in user.
        /// </summary>
        /// <param name="updatePasswordRequest">The new password.</param>
        [HttpPut("password")]
        public async Task<ActionResult> UpdatePassword([FromBody] MeUpdatePasswordRequest updatePasswordRequest) {
            var result = await passwordUpdater.Handle(new UserPasswordUpdateParams(User!, updatePasswordRequest.CurrentPassword, updatePasswordRequest.NewPassword));

            return result.Match<ActionResult>(
                fail => BadRequest(fail),
                Ok
            );
        }
        #endregion
    }
}