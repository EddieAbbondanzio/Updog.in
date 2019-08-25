using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a number of posts based on their creation date.
    /// </summary>
    public sealed class PostFinderByNew : IInteractor<PostFinderByNewParams, PagedResultSet<PostView>> {
        #region Fields
        private IPostRepo postRepo;

        private IMapper<Post, PostView> postMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post find by new interactor.
        /// </summary>
        /// <param name="postRepo">CRUD post repo.</param>
        /// <param name="postMapper">Mapper to convert post to DTO..</param>
        public PostFinderByNew(IPostRepo postRepo, IMapper<Post, PostView> postMapper) {
            this.postRepo = postRepo;
            this.postMapper = postMapper;
        }
        #endregion


        #region Publics
        public async Task<PagedResultSet<PostView>> Handle(PostFinderByNewParams input) {
            PagedResultSet<Post> posts = await postRepo.FindNewest(input.PageNumber, input.PageSize);
            List<PostView> views = new List<PostView>();

            foreach (Post p in posts) {
                views.Add(postMapper.Map(p));
            }

            return new PagedResultSet<PostView>(views, posts.Pagination);
        }
    }
    #endregion
}