using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Updog.Application.Paging;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a number of posts via the user who created them..
    /// </summary>
    public sealed class PostFinderByUser : Interactor<FindByValueParams<string>, PagedResultSet<PostView>> {
        #region Fields
        private IDatabase database;
        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostFinderByUser(IDatabase database, IPostViewMapper postMapper) {
            this.database = database;
            this.postMapper = postMapper;
        }
        #endregion


        #region Publics
        [Validate(typeof(FindByUserValidator))]
        protected override async Task<PagedResultSet<PostView>> HandleInput(FindByValueParams<string> input) {
            using (var connection = database.GetConnection()) {
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);
                IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);

                PagedResultSet<Post> posts = await postRepo.FindByUser(input.Value, input.Pagination?.PageNumber ?? 0, input.Pagination?.PageSize ?? Post.PageSize);

                if (input.User != null) {
                    foreach (Post p in posts) {
                        p.Vote = await voteRepo.FindByUserAndPost(input.User.Username, p.Id);
                    }
                }

                return new PagedResultSet<PostView>(posts.Items.Select(p => postMapper.Map(p)), posts.Pagination);
            }
        }
    }
    #endregion
}