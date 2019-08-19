using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a post by it's unique ID.
    /// </summary>
    public sealed class PostFinderById : IInteractor<int, PostView> {
        #region Fields
        private IPostRepo postRepo;

        private IMapper<Post, PostView> postMapper;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post find by ID interactor.
        /// </summary>
        /// <param name="postRepo">CRUD post repo.</param>
        /// <param name="postMapper">Mapper to convert posts to view</param>
        public PostFinderById(IPostRepo postRepo, IMapper<Post, PostView> postMapper) {
            this.postRepo = postRepo;
            this.postMapper = postMapper;
        }
        #endregion

        /// <summary>
        /// Find a post by it's unique ID.
        /// </summary>
        /// <param name="input">The ID to look for.</param>
        /// <returns>The matching post found.</returns>
        public async Task<PostView> Handle(int input) {
            Post p = await postRepo.FindById(input);

            if (p == null) {
                return null;
            }

            return postMapper.Map(p);
        }
    }
}