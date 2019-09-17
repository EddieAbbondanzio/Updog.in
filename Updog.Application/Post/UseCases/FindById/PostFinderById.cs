using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a post by it's unique ID.
    /// </summary>
    public sealed class PostFinderById : Interactor<FindByValueParams<int>, PostView?> {
        #region Fields
        private IDatabase database;

        private IPostViewMapper postMapper;
        #endregion

        #region Constructor(s)
        public PostFinderById(IDatabase database, IPostViewMapper postMapper) {
            this.database = database;
            this.postMapper = postMapper;
        }
        #endregion
        [Validate(typeof(FindByIdValidator))]
        protected async override Task<PostView?> HandleInput(FindByValueParams<int> input) {
            using (var connection = database.GetConnection()) {
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);
                IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);

                Post? post = await postRepo.FindById(input.Value);

                if (post == null) {
                    return null;
                }

                //Pull in the vote if needed.
                if (input.User != null) {
                    post.Vote = await voteRepo.FindByUserAndPost(input.User.Username, input.Value);
                }

                return postMapper.Map(post);
            }
        }
    }
}