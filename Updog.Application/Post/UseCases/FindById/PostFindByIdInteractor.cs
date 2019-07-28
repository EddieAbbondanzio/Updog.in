using System.Threading.Tasks;
using Blurtle.Domain;

namespace Blurtle.Application {
    /// <summary>
    /// Use case handler to find a post by it's unique ID.
    /// </summary>
    public sealed class PostFindByIdInteractor : IInteractor<int, Post> {
        #region Fields
        private IPostRepo postRepo;
        #endregion

        #region Constructor(s)
        public PostFindByIdInteractor(IPostRepo postRepo) { this.postRepo = postRepo; }
        #endregion

        public async Task<Post> Handle(int input) => await postRepo.FindById(input);
    }
}