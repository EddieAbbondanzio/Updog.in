
using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Updog.Api {
    /// <summary>
    /// End points for managing subcriptions to spaces.
    /// </summary>
    [Authorize]
    [Route("api/subscription")]
    [ApiController]
    public sealed class SubscriptionController : ApiController {
        #region Fields
        private IMediator mediator;
        #endregion

        #region Constructor(s)
        public SubscriptionController(IMediator mediator) {
            this.mediator = mediator;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Subscribe to a sub space.
        /// </summary>
        /// <param name="spaceName">The name of the space.</param>
        [HttpPost("{spaceName}")]
        public async Task<IActionResult> SubscribeToSpace(string spaceName) =>
            (await mediator.Command(new SubscriptionCreateCommand(new Domain.SubscriptionCreate(spaceName), User!)))
            .Match(
                r => Ok() as IActionResult,
                e => BadRequest(e.Message)
            );

        /// <summary>
        /// Cancel a subscription to a space.
        /// </summary>
        /// <param name="spaceName">The name of the space to cancel.</param>
        [HttpDelete("{spaceName}")]
        public async Task<IActionResult> DesubscribeFromSpace(string spaceName) =>
            (await mediator.Command(new SubscriptionDeleteCommand(spaceName, User!)))
            .Match(
                r => Ok() as IActionResult,
                e => BadRequest(e.Message)
            );
        #endregion
    }
}