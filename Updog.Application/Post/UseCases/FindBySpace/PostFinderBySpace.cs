using System.Collections.Generic;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find posts by a space.
    /// </summary>
    public sealed class PostFinderBySpace : IInteractor<PostFindBySpaceParams, PagedResultSet<PostView>> {
        #region Fields
        private IPostRepo repo;

        private IMapper<Post, PostView> mapper;
        #endregion

        #region Constructor(s)
        public PostFinderBySpace(IPostRepo repo, IMapper<Post, PostView> mapper) {
            this.repo = repo;
            this.mapper = mapper;
        }
        #endregion

        public async Task<PagedResultSet<PostView>> Handle(PostFindBySpaceParams input) {
            PagedResultSet<Post> posts = await repo.FindBySpace(input.Space, input.PageNumber, input.PageSize);

            List<PostView> views = new List<PostView>();

            foreach (Post p in posts) {
                views.Add(mapper.Map(p));
            }

            return new PagedResultSet<PostView>(views, posts.Pagination);
        }
    }
}