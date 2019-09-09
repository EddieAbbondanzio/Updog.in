using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a post by it's unique ID.
    /// </summary>
    public sealed class PostFinderById : IInteractor<int, PostView?> {
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

        /// <summary>
        /// Find a post by it's unique ID.
        /// </summary>
        /// <param name="postId">The ID to look for.</param>
        /// <returns>The matching post found.</returns>
        public async Task<PostView?> Handle(int postId) {
            using (var connection = database.GetConnection()) {
                IPostRepo postRepo = database.GetRepo<IPostRepo>(connection);

                Post? post = await postRepo.FindById(postId);
                return post != null ? postMapper.Map(post) : null;
            }
        }
    }
}