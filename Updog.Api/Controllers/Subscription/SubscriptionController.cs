
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
        private SubscriptionCreator _subscriptionCreator;

        private SubscriptionDeleter _subscriptionDeleter;
        #endregion

        #region Constructor(s)
        public SubscriptionController(SubscriptionCreator subscriptionCreator, SubscriptionDeleter subscriptionDeleter) {
            _subscriptionCreator = subscriptionCreator;
            _subscriptionDeleter = subscriptionDeleter;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Subscribe to a sub space.
        /// </summary>
        /// <param name="spaceName">The name of the space.</param>
        [HttpPost("{spaceName}")]
        public async Task<ActionResult> SubscribeToSpace(string spaceName) {
            SubscriptionView s = await _subscriptionCreator.Handle(new SubscriptionCreateParams(spaceName, User!));
            return Ok(s);
        }

        /// <summary>
        /// Cancel a subscription to a space.
        /// </summary>
        /// <param name="spaceName">The name of the space to cancel.</param>
        [HttpDelete("{spaceName}")]
        public async Task<ActionResult> DesubscribeFromSpace(string spaceName) {
            await _subscriptionDeleter.Handle(new SubscriptionDeleteParams(spaceName, User!));
            return Ok();
        }
        #endregion
    }
}