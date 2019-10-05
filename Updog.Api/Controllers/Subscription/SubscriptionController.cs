
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
        private CommandHandler<SubscriptionCreateCommand> subscriptionCreator;
        private CommandHandler<SubscriptionDeleteCommand> subscriptionDeleter;
        #endregion

        #region Constructor(s)
        public SubscriptionController(CommandHandler<SubscriptionCreateCommand> subscriptionCreator, CommandHandler<SubscriptionDeleteCommand> subscriptionDeleter) {
            this.subscriptionCreator = subscriptionCreator;
            this.subscriptionDeleter = subscriptionDeleter;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Subscribe to a sub space.
        /// </summary>
        /// <param name="spaceName">The name of the space.</param>
        [HttpPost("{spaceName}")]
        public async Task<ActionResult> SubscribeToSpace(string spaceName) {
            await subscriptionCreator.Execute(new SubscriptionCreateCommand() {
                Space = spaceName,
                User = User!
            }, ActionResultBuilder);
            return ActionResultBuilder.Build();
        }

        /// <summary>
        /// Cancel a subscription to a space.
        /// </summary>
        /// <param name="spaceName">The name of the space to cancel.</param>
        [HttpDelete("{spaceName}")]
        public async Task<ActionResult> DesubscribeFromSpace(string spaceName) {
            await subscriptionDeleter.Execute(new SubscriptionDeleteCommand() {
                Space = spaceName,
                User = User!
            }, ActionResultBuilder);
            return ActionResultBuilder.Build();
        }
        #endregion
    }
}