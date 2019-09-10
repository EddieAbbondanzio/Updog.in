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
        private SpaceFinder _spaceFinder;

        private SpaceFinderByName _spaceFinderByName;

        private SpaceCreator _spaceCreator;

        private SpaceUpdater _spaceUpdater;

        private PostFinderBySpace _postFinderBySpace;
        #endregion

        #region Constructor(s)
        public SpaceController(SpaceFinder spaceFinder, SpaceFinderByName spaceFinderByName, SpaceCreator spaceCreator, SpaceUpdater spaceUpdater, PostFinderBySpace postFinderBySpace) {
            this._spaceFinder = spaceFinder;
            this._spaceFinderByName = spaceFinderByName;
            this._spaceCreator = spaceCreator;
            this._spaceUpdater = spaceUpdater;
            this._postFinderBySpace = postFinderBySpace;
        }
        #endregion

        #region Publics
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Find([FromQuery]int pageNumber, [FromQuery] int pageSize = Space.PageSize) {
            PagedResultSet<SpaceView> spaces = await this._spaceFinder.Handle(new SpaceFindParams(pageNumber, pageSize));
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
            SpaceView? s = await this._spaceFinderByName.Handle(new SpaceFindByNameParams(name, User));

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
            SpaceView s = await this._spaceCreator.Handle(new SpaceCreateParams(request.Name, request.Description, User!));
            return Ok(s);
        }

        /// <summary>
        /// Edit an existing sub space.
        /// </summary>
        [HttpPatch("{name}")]
        public async Task<ActionResult> UpdateSpace(string name, SpaceUpdateRequest request) {
            SpaceView s = await this._spaceUpdater.Handle(new SpaceUpdateParams(name, request.Description, User!));
            return Ok(s);
        }

        /// <summary>
        /// Find the posts for a specific space.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        /// <param name="pageNumber">0-index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>The posts found.</returns>
        [AllowAnonymous]
        [HttpGet("{name}/post/new")]
        public async Task<ActionResult> FindPosts(string name, [FromQuery]int pageNumber, [FromQuery] int pageSize = Post.PageSize) {
            PagedResultSet<PostView> posts = await this._postFinderBySpace.Handle(new PostFindBySpaceParams(name, pageNumber, pageSize));
            SetContentRangeHeader(posts.Pagination);
            return Ok(posts);
        }
        #endregion
    }
}