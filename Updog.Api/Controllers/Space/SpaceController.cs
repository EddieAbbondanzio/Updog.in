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
    /// End point for managing sub spaces of the site.
    /// </summary>
    [Authorize]
    [Route("api/space")]
    [ApiController]
    public sealed class SpaceController : ApiController {
        #region Fields
        private SpaceFinder spaceFinder;
        private SpaceFinderByName spaceFinderByName;
        private SubscriptionFinderByUser subsriptionFinderByUser;
        private SpaceFinderDefault spaceFinderDefault;
        private SpaceCreator spaceCreator;
        private SpaceUpdater spaceUpdater;
        private PostFinderBySpace postFinderBySpace;
        #endregion

        #region Constructor(s)
        public SpaceController(SpaceFinder spaceFinder, SpaceFinderByName spaceFinderByName, SubscriptionFinderByUser subscriptionFinder, SpaceFinderDefault spaceFinderDefault, SpaceCreator spaceCreator, SpaceUpdater spaceUpdater, PostFinderBySpace postFinderBySpace) {
            this.spaceFinder = spaceFinder;
            this.spaceFinderByName = spaceFinderByName;
            this.subsriptionFinderByUser = subscriptionFinder;
            this.spaceFinderDefault = spaceFinderDefault;
            this.spaceCreator = spaceCreator;
            this.spaceUpdater = spaceUpdater;
            this.postFinderBySpace = postFinderBySpace;
        }
        #endregion

        #region Publics
        /// <summary>
        /// Get the default spaces.
        /// </summary>
        [HttpGet("default")]
        [AllowAnonymous]
        public async Task<ActionResult> GetDefaultSpaces() {
            var result = await spaceFinderDefault.Handle(new FindParams(User));

            return result.Match<ActionResult>(
                spaces => Ok(spaces),
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Get the subscribed spaces of the user.
        /// </summary>
        [HttpGet("subscribed")]
        public async Task<ActionResult> GetSubscribedSpaces() {
            var result = await subsriptionFinderByUser.Handle(new FindByValueParams<string>(User!.Username));

            return result.Match<ActionResult>(
                spaces => Ok(spaces),
                fail => BadRequest(fail)
            );
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Find([FromQuery]int pageNumber, [FromQuery] int pageSize = Space.PageSize) {
            var result = await this.spaceFinder.Handle(new FindParams(pagination: new PaginationInfo(pageNumber, pageSize)));

            return result.Match<ActionResult>(
                spaces => {
                    SetContentRangeHeader(spaces.Pagination);
                    return Ok(spaces);
                },
                fail => BadRequest(fail)
            );
        }


        /// <summary>
        /// Get a space via it's name.
        /// </summary>
        /// <param name="name">The name of the space.</param>
        [HttpHead("{name}")]
        [HttpGet("{name}")]
        [AllowAnonymous]
        public async Task<ActionResult> FindByName(string name) {
            var result = await this.spaceFinderByName.Handle(new FindByValueParams<string>(name, User));

            return result.Match<ActionResult>(
                space => space != null ? Ok(space) : NotFound() as ActionResult,
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Create a new sub space.
        /// </summary>
        /// <param name="request">The incoming reuqest</param>
        [HttpPost]
        public async Task<ActionResult> CreateSpace(SpaceCreateRequest request) {
            var result = await this.spaceCreator.Handle(new SpaceCreateParams(request.Name, request.Description, User!));

            return result.Match<ActionResult>(
                space => Ok(space),
                fail => BadRequest(fail)
            );
        }

        /// <summary>
        /// Edit an existing sub space.
        /// </summary>
        [HttpPatch("{name}")]
        public async Task<ActionResult> UpdateSpace(string name, SpaceUpdateRequest request) {
            var result = await this.spaceUpdater.Handle(new SpaceUpdateParams(name, request.Description, User!));

            return result.Match<ActionResult>(
                space => Ok(space),
                fail => BadRequest(fail)
            );
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
            var result = await this.postFinderBySpace.Handle(new FindByValueParams<string>(name, User, new PaginationInfo(pageNumber, pageSize)));

            return result.Match<ActionResult>(
                posts => {
                    SetContentRangeHeader(posts.Pagination);
                    return Ok(posts);
                },
                fail => BadRequest(fail)
            );
        }
        #endregion
    }
}