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
        private IMediator mediator;
        #endregion

        #region Constructor(s)
        public MeController(IMediator mediator) {
            this.mediator = mediator;
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
        public async Task<IActionResult> Update([FromBody] MeUpdateRequest updateRequest) =>
            (await mediator.Command(new UserUpdateCommand(User!.Username, new UserUpdate(updateRequest.Email), User!)))
            .Match(
                (result) => Ok() as IActionResult,
                (error) => BadRequest(error.Message)
            );

        /// <summary>
        /// Update the password of the logged in user.
        /// </summary>
        /// <param name="updatePasswordRequest">The new password.</param>
        [HttpPut("password")]
        public async Task<IActionResult> UpdatePassword([FromBody] MeUpdatePasswordRequest updatePasswordRequest) =>
            (await mediator.Command(new UserUpdatePasswordCommand(
                User!.Username,
                new UserUpdatePassword(updatePasswordRequest.CurrentPassword, updatePasswordRequest.NewPassword),
                User!))
            ).Match(
                (result) => Ok() as IActionResult,
                (error) => BadRequest(error.Message)
            );
        #endregion
    }
}