
using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;
using System;
using FluentValidation;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Updog.Application.Paging;

namespace Updog.Api {
    /// <summary>
    /// End points for managing subcriptions to spaces.
    /// </summary>
    [Authorize]
    [Route("api/subscription")]
    [ApiController]
    public sealed class SubscriptionController : ApiController {
        /// <summary>
        /// Get the subscriptions of the user, or defaults
        /// if no user is logged in.
        /// </summary>
        [AllowAnonymous]
        public async Task<ActionResult> GetSubscriptions() {
            // If logged in, get their subs, else get defaults.
            throw new Exception();
        }

        /// <summary>
        /// Subscribe to a sub space.
        /// </summary>
        /// <param name="spaceName">The name of the space.</param>
        [HttpPost("{spaceName}")]
        public async Task<ActionResult> SubscribeToSpace(string spaceName) {
            throw new Exception();
        }

        /// <summary>
        /// Cancel a subscription to a space.
        /// </summary>
        /// <param name="spaceName">The name of the space to cancel.</param>
        [HttpDelete("{spaceName}")]
        public async Task<ActionResult> DesubscribeFromSpace(string spaceName) {
            throw new Exception();
        }
    }
}