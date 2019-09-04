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
    /// End point for managing sub spaces of the site.
    /// </summary>
    [Authorize]
    [Route("api/space")]
    [ApiController]
    public sealed class SpaceController : ApiController {
        #region Fields
        private SpaceFinder spaceFinder;

        private SpaceFinderByName spaceFinderByName;

        private SpaceCreator spaceCreator;

        private SpaceUpdater spaceUpdater;

        private PostFinderBySpace postFinderBySpace;
        #endregion

        #region Constructor(s)
        public SpaceController(SpaceFinder spaceFinder, SpaceFinderByName spaceFinderByName, SpaceCreator spaceCreator, SpaceUpdater spaceUpdater, PostFinderBySpace postFinderBySpace) {
            this.spaceFinder = spaceFinder;
            this.spaceFinderByName = spaceFinderByName;
            this.spaceCreator = spaceCreator;
            this.spaceUpdater = spaceUpdater;
            this.postFinderBySpace = postFinderBySpace;
        }
        #endregion

        #region Publics
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Find([FromQuery]int pageNumber, [FromQuery] int pageSize = Space.PageSize) {
            PagedResultSet<SpaceView> spaces = await this.spaceFinder.Handle(new SpaceFindParams(pageNumber, pageSize));
            SetContentRangeHeader(spaces.Pagination);
            return Ok(spaces);
        }


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

        /// <summary>
        /// Find the posts for a specific space.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        /// <param name="pageNumber">0-index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The posts found.</returns>
        [HttpGet("{name}/post/new")]
        public async Task<ActionResult> FindPosts(string name, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            PagedResultSet<PostView> posts = await this.postFinderBySpace.Handle(new PostFindBySpaceParams(name, pageNumber, pageSize));
            SetContentRangeHeader(posts.Pagination);
            return Ok(posts);
        }
        #endregion
    }
}