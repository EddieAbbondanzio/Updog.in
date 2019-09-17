
using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;
using System;
using FluentValidation;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Updog.Application.Paging;
using System.Collections.Generic;

namespace Updog.Api {
    /// <summary>
    /// End points for managing subcriptions to spaces.
    /// </summary>
    [Authorize]
    [Route("api/subscription")]
    [ApiController]
    public sealed class SubscriptionController : ApiController {
        #region Fields
        private SubscriptionCreator subscriptionCreator;
        private SubscriptionDeleter subscriptionDeleter;
        #endregion

        #region Constructor(s)
        public SubscriptionController(SubscriptionCreator subscriptionCreator, SubscriptionDeleter subscriptionDeleter) {
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
            var result = await subscriptionCreator.Handle(new SubscriptionCreateParams(spaceName, User!));

            return result.Match<ActionResult>(
                sub => Ok(sub),
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Cancel a subscription to a space.
        /// </summary>
        /// <param name="spaceName">The name of the space to cancel.</param>
        [HttpDelete("{spaceName}")]
        public async Task<ActionResult> DesubscribeFromSpace(string spaceName) {
            var failable = await subscriptionDeleter.Handle(new SubscriptionDeleteParams(spaceName, User!));

            return failable.Match<ActionResult>(
                fail => BadRequest(fail),
                () => Ok()
            );
        }
        #endregion
    }
}