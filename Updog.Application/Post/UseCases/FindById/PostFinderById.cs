using System;
using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a post by it's unique ID.
    /// </summary>
    public sealed class PostFinderById : IInteractor<int, PostView?> {
        #region Fields
        private IDatabase _database;

        private IPostViewMapper _postMapper;
        #endregion

        #region Constructor(s)
        public PostFinderById(IDatabase database, IPostViewMapper postMapper) {
            _database = database;
            _postMapper = postMapper;
        }
        #endregion

        /// <summary>
        /// Find a post by it's unique ID.
        /// </summary>
        /// <param name="input">The ID to look for.</param>
        /// <returns>The matching post found.</returns>
        public async Task<PostView?> Handle(int input) {
            throw new Exception();
            Post? p = null;
            // IPostRepo postRepo = _database.CreateRepo<IPostRepo>();
            // 
            // Post? p = await postRepo.FindById(input);

            if (p == null) {
                return null;
            }

            return _postMapper.Map(p);
        }
    }
}