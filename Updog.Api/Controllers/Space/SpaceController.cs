using Microsoft.AspNetCore.Mvc;
using Updog.Application;
using System.Threading.Tasks;
using Updog.Domain;
using System;
using FluentValidation;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Updog.Api {
    /// <summary>
    /// End point for managing sub spaces of the site.
    /// </summary>
    [Authorize]
    [Route("api/space")]
    [ApiController]
    public sealed class SpaceController : ApiController {
        #region Fields
        private SpaceFinderByName spaceFinderByName;

        private SpaceCreator spaceCreator;

        private SpaceUpdater spaceUpdater;
        #endregion

        #region Constructor(s)
        public SpaceController(SpaceFinderByName spaceFinderByName, SpaceCreator spaceCreator, SpaceUpdater spaceUpdater) {
            this.spaceFinderByName = spaceFinderByName;
            this.spaceCreator = spaceCreator;
            this.spaceUpdater = spaceUpdater;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get a space via it's name.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        [HttpHead("{name}")]
        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<ActionResult> FindByName(string name) {
            SpaceView s = await this.spaceFinderByName.Handle(name);

            if (s != null) {
                return Ok(s);
            } else {
                return NotFound();
            }
        }

        /// <summary>
        /// Create a new sub space.
        /// </summary>
        /// <param name="request">The incoming reuqest</param>
        [HttpPost]
        public async Task<ActionResult> CreateSpace(SpaceCreateRequest request) {
            try {
                SpaceView s = await this.spaceCreator.Handle(new SpaceCreateParams(request.Name, request.Description, User));
                return Ok(s);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            } catch {
                return InternalServerError("An unknown error occured.");
            }
        }

        /// <summary>
        /// Edit an existing sub space.
        /// </summary>
        [HttpPatch("{name}")]
        public async Task<ActionResult> UpdateSpace(string name, SpaceUpdateRequest request) {
            try {
                SpaceView s = await this.spaceUpdater.Handle(new SpaceUpdateParams(name, request.Description, User));
                return Ok(s);
            } catch (ValidationException ex) {
                return BadRequest(ex.Message);
            } catch {
                return InternalServerError("An unknown error occured.");
            }
        }

        /// <summary>
        /// Subscribe to a sub space.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        [HttpPost("{name}/subscribe")]
        public async Task<ActionResult> SubscribeToSpace(string name) {
            throw new Exception();
        }

        /// <summary>
        /// Cancel a subscription to a space.
        /// </summary>
        /// <param name="name">The name of the space to cancel.</param>
        [HttpDelete("{name}/subscribe")]
        public async Task<ActionResult> DesubscribeFromSpace(string name) {
            throw new Exception();
        }
        #endregion
    }
}