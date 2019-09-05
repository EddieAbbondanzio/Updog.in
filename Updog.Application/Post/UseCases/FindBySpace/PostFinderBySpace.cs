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
        private IPostRepo _repo;

        private IPostViewMapper _mapper;
        #endregion

        #region Constructor(s)
        public PostFinderBySpace(IPostRepo repo, IPostViewMapper mapper) {
            _repo = repo;
            _mapper = mapper;
        }
        #endregion

        public async Task<PagedResultSet<PostView>> Handle(PostFindBySpaceParams input) {
            PagedResultSet<Post> posts = await _repo.FindBySpace(input.Space, input.PageNumber, input.PageSize);

            List<PostView> views = new List<PostView>();

            foreach (Post p in posts) {
                views.Add(_mapper.Map(p));
            }

            return new PagedResultSet<PostView>(views, posts.Pagination);
        }
    }
}