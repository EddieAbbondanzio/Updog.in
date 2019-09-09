using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find posts by a space.
    /// </summary>
    public sealed class PostFinderBySpace : IInteractor<PostFindBySpaceParams, PagedResultSet<PostView>> {
        #region Fields
        private IDatabase database;
        private IPostViewMapper mapper;
        #endregion

        #region Constructor(s)
        public PostFinderBySpace(IDatabase database, IPostViewMapper mapper) {
            this.database = database;
            this.mapper = mapper;
        }
        #endregion

        public async Task<PagedResultSet<PostView>> Handle(PostFindBySpaceParams input) {
            using (var connection = database.GetConnection()) {
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);

                PagedResultSet<Post> posts = await postRepo.FindBySpace(input.Space, input.PageNumber, input.PageSize);

                return new PagedResultSet<PostView>(posts.Items.Select(p => mapper.Map(p)), posts.Pagination);
            }
        }
    }
}