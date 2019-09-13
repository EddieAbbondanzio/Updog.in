
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
        private SubscriptionFinderByUser _subsriptionFinderByUser;

        private SpaceFinderDefault _spaceFinderDefault;

        private SubscriptionCreator _subscriptionCreator;

        private SubscriptionDeleter _subscriptionDeleter;
        #endregion

        #region Constructor(s)
        public SubscriptionController(SubscriptionFinderByUser subscriptionFinderByUser, SpaceFinderDefault spaceFinderDefault, SubscriptionCreator subscriptionCreator, SubscriptionDeleter subscriptionDeleter) {
            _subsriptionFinderByUser = subscriptionFinderByUser;
            _spaceFinderDefault = spaceFinderDefault;
            _subscriptionCreator = subscriptionCreator;
            _subscriptionDeleter = subscriptionDeleter;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get the subscriptions of the user, or defaults
        /// if no user is logged in.
        /// </summary>
        [AllowAnonymous]
        public async Task<ActionResult> GetSubscriptions() {
            if (User != null) {
                IEnumerable<SubscriptionView> subs = await _subsriptionFinderByUser.Handle(new SubscriptionFindByUserParams(User));
                return Ok(subs.Select(s => s.Space));
            } else {
                IEnumerable<SpaceView> spaces = await _spaceFinderDefault.Handle(new SpaceFindByDefaultParams(User));
                return Ok(spaces);
            }
        }

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