using System.Threading.Tasks;
using Updog.Domain;

namespace Updog.Application {
    /// <summary>
    /// Use case handler to find a post by it's unique ID.
    /// </summary>
    public sealed class PostFinderById : IInteractor<int, PostInfo> {
        #region Fields
        private IPostRepo postRepo;

        private IUserRepo userRepo;
        #endregion

        #region Constructor(s)
        /// <summary>
        /// Create a new post find by ID interactor.
        /// </summary>
        /// <param name="postRepo">CRUD post repo.</param>
        /// <param name="userRepo">CRUD user repo.</param>
        public PostFinderById(IPostRepo postRepo, IUserRepo userRepo) {
            this.postRepo = postRepo;
            this.userRepo = userRepo;
        }
        #endregion

        /// <summary>
        /// Find a post by it's unique ID.
        /// </summary>
        /// <param name="input">The ID to look for.</param>
        /// <returns>The matching post found.</returns>
        public async Task<PostInfo> Handle(int input) {
            Post p = await postRepo.FindById(input);
            User u = await userRepo.FindById(p.UserId);

            return new PostInfo(p.Id, p.Type, p.Title, p.Body, u.Username, p.CreationDate);
        }
    }
}