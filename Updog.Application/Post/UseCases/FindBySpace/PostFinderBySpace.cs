using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Interactor to find posts by a space.
    /// </summary>
    public sealed class PostFinderBySpace : IInteractor<FindByValueParams<string>, PagedResultSet<PostView>> {
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

        public async Task<PagedResultSet<PostView>> Handle(FindByValueParams<string> input) {
            using (var connection = database.GetConnection()) {
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);
                IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);

                PagedResultSet<Post> posts = await postRepo.FindBySpace(input.Value, input.Pagination.PageNumber, input.Pagination.PageSize);

                if (input.User != null) {

                    foreach (Post p in posts) {
                        p.Vote = await voteRepo.FindByUserAndPost(input.User.Username, p.Id);
                    }
                }

                return new PagedResultSet<PostView>(posts.Items.Select(p => mapper.Map(p)), posts.Pagination);
            }
        }
    }
}