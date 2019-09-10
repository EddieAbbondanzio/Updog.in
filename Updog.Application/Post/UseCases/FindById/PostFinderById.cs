using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a post by it's unique ID.
    /// </summary>
    public sealed class PostFinderById : IInteractor<PostFindByIdParams, PostView?> {
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
        public async Task<PostView?> Handle(PostFindByIdParams input) {
            using (var connection = database.GetConnection()) {
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);
                IVoteRepo voteRepo = database.GetRepo<IVoteRepo>(connection);

                Post? post = await postRepo.FindById(input.PostId);

                if (post == null) {
                    return null;
                }

                //Pull in the vote if needed.
                if (input.User != null) {
                    post.Vote = await voteRepo.FindByUserAndPost(input.User.Username, input.PostId);
                }

                return postMapper.Map(post);
            }
        }
    }
}