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
    [Route("api/me")]
    [ApiController]
    public sealed class MeController : ApiController {
        #region Fields
        public UpdateUserInteractor userUpdater;

        public UserPasswordUpdater passwordUpdater;
        #endregion

        #region Constructor(s)
        public MeController(UpdateUserInteractor userUpdater, UserPasswordUpdater passwordUpdater) {
            this.userUpdater = userUpdater;
            this.passwordUpdater = passwordUpdater;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get the email of the user.
        /// </summary>
        [HttpGet("email")]
        [Authorize]
        public ActionResult GetEmail() {
            return Ok(User.Email);
        }

        /// <summary>
        /// Update the info of a user.
        /// </summary>
        /// <param name="updateRequest">The new user info.</param>
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] MeUpdateRequest updateRequest) {
            await userUpdater.Handle(new UpdateUserParams(User, updateRequest.Email));
            return Ok();
        }

        /// <summary>
        /// Update the password of the logged in user.
        /// </summary>
        /// <param name="updatePasswordRequest">The new password.</param>
        [Authorize]
        [HttpPut("password")]
        public async Task<ActionResult> UpdatePassword([FromBody] MeUpdatePasswordReqest updatePasswordRequest) {
            await passwordUpdater.Handle(new UserPasswordUpdateParams(User, updatePasswordRequest.Password));
            return Ok();
        }
        #endregion
    }
}